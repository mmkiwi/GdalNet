// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet.InteropSourceGen;

[Generator]
public class HandleGenerator : IIncrementalGenerator
{
    const string AttributeFull =
        $"{nameof(MMKiwi)}.{nameof(GdalNet)}.{nameof(InteropAttributes)}.{nameof(GdalGenerateHandleAttribute)}";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Do a simple filter for methods
        IncrementalValuesProvider<GenerationInfo> methodDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                AttributeFull,
                predicate: (node, _) => node is ClassDeclarationSyntax, // select methods with attributes
                transform: GetClasses); // sect the methods with the [GdalWrapperMethod] attribute

        // Combine the selected methods with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<GenerationInfo>)> compilationAndMethods
            = context.CompilationProvider.Combine(methodDeclarations.Collect());

        // Generate the source using the compilation and methods
        context.RegisterSourceOutput(compilationAndMethods,
            static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static GenerationInfo GetClasses(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        // we know the node is a MethodDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)context.TargetNode;
        INamedTypeSymbol classSymbol = (INamedTypeSymbol)context.TargetSymbol;

        AttributeData attribute = context.Attributes[0];

        bool needsConstructMethod = false;
        bool hasConstructor;
        bool hasConstructMethod = false;
        bool generateOwns = true;
        bool generateDoesntOwn = true;
        bool needsOwns = false;
        bool needsDoesntOwn = false;
        bool hasOwns = false;
        bool hasDoesntOwn = false;

        bool isPartial = classSyntax.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword));

        if (!isPartial)
        {
            //If the method is not partial, skip everything else and just report an error. 
            return new GenerationInfo.ErrorNotPartial { ClassSymbol = classSyntax, };
        }

        MemberVisibility constructorVisibility = MemberVisibility.Protected;

        foreach (KeyValuePair<string, TypedConstant> namedArgument in attribute.NamedArguments)
        {
            switch (namedArgument)
            {
                case { Key: nameof(GdalGenerateHandleAttribute.ConstructorVisibility), Value.Value: int cv }:
                    constructorVisibility = (MemberVisibility)cv;
                    break;
                case { Key: nameof(GdalGenerateHandleAttribute.GenerateOwns), Value.Value: bool bv }:
                    generateOwns = bv;
                    break;
                case { Key: nameof(GdalGenerateHandleAttribute.GenerateDoesntOwn), Value.Value: bool bv }:
                    generateDoesntOwn = bv;
                    break;
            }
        }

        string? baseHandle = null;

        List<string> parentClasses = [];

        var parentClass = classSymbol.BaseType;
        while (parentClass != null)
        {
            string ds = parentClass.ToDisplayString();
            parentClasses.Add(ds);
            if (ds == "MMKiwi.GdalNet.Handles.GdalInternalHandleNeverOwns")
            {
                baseHandle = "GdalInternalHandleNeverOwns";
                break;
            }

            if (ds == "MMKiwi.GdalNet.Handles.GdalInternalHandle")
            {
                needsDoesntOwn = needsOwns = true;
                baseHandle = "GdalInternalHandle";
                break;
            }

            parentClass = parentClass.BaseType;
        }

        if (baseHandle is null)
            return new GenerationInfo.ErrorBadBase() { ClassSymbol = classSyntax, ParentClass = parentClasses };

        if ((needsOwns && generateOwns) || (needsDoesntOwn && generateDoesntOwn))
        {
            foreach (var member in classSymbol.GetMembers().OfType<INamedTypeSymbol>())
            {
                switch (member.Name)
                {
                    case "Owns":
                        hasOwns = true;
                        break;
                    case "DoesntOwn":
                        hasDoesntOwn = true;
                        break;
                }
            }
        }

        foreach (var baseInterface in classSymbol.Interfaces.Where(baseInterface
                     => baseInterface.IsGenericType &&
                        baseInterface.ConstructedFrom.ToDisplayString()
                            .StartsWith("MMKiwi.GdalNet.Handles.IConstructableHandle<")))
        {
            needsConstructMethod = true;
            foreach (IMethodSymbol symbol in baseInterface.GetMembers().OfType<IMethodSymbol>())
            {
                if (symbol != null)
                    hasConstructMethod |= classSymbol.FindImplementationForInterfaceMember(symbol) is not null;
            }
        }


        hasConstructor = classSymbol.Constructors.Any(constructor =>
            constructor.Parameters.Length == 1 && constructor.Parameters[0].Type.ToDisplayString() == "bool");
        
        return new GenerationInfo.Ok
        {
            ClassSymbol = classSyntax,
            GenerateConstruct = needsConstructMethod && !hasConstructMethod,
            BaseHandleType = baseHandle,
            GenerateConstructor =
                !hasConstructor && baseHandle == "GdalInternalHandle" &&
                constructorVisibility != MemberVisibility.DoNotGenerate,
            ConstructorVisibility = constructorVisibility.ToStringFast(),
            IsSealedOrAbstract = classSymbol.IsAbstract || classSymbol.IsSealed,
            GenerateOwns = needsOwns && generateOwns && !hasOwns,
            GenerateDoesntOwn = needsDoesntOwn && generateDoesntOwn && !hasDoesntOwn
        };
    }

    static void Execute(Compilation compilation, ImmutableArray<GenerationInfo> classes,
        SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }

        foreach (GenerationInfo cls in classes.Distinct(GenerationInfo.ClassNameEqualityComparer.Default))
        {
            switch (cls)
            {
                case GenerationInfo.ErrorBadBase ebb:
                    context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG00018",
                            "Class must be partial",
                            "Class {0} must subclass GdalInternalHandle. Parent classes: {1}",
                            "Gdal.SourceGenerator",
                            DiagnosticSeverity.Warning,
                            true),
                        cls.ClassSymbol.GetLocation(),
                        cls.ClassSymbol.Identifier, string.Join(", ", ebb.ParentClass)));
                    break;
                case GenerationInfo.ErrorNotPartial:
                    context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG00010",
                            "Class must be partial",
                            "Class {0} must be partial for the source generator to work.",
                            "Gdal.SourceGenerator",
                            DiagnosticSeverity.Warning,
                            true),
                        cls.ClassSymbol.GetLocation(),
                        cls.ClassSymbol.Identifier));
                    break;
                case GenerationInfo.Ok genInfo:
                    {
                        if (!genInfo.IsSealedOrAbstract)
                        {
                            context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG00012",
                                    "Handle should be sealed or abstract",
                                    "Class {0} should be sealed or abstract.",
                                    "Gdal.SourceGenerator",
                                    DiagnosticSeverity.Warning,
                                    true),
                                cls.ClassSymbol.GetLocation(),
                                cls.ClassSymbol.Identifier));
                        }

                        // generate the source code and add it to the output
                        string result = HandleGenerationHelper.GenerateExtensionClass(compilation, genInfo, context);
                        context.AddSource($"Construct.{cls.ClassSymbol.ToFullDisplayName()}.g.cs",
                            SourceText.From(result, Encoding.UTF8));
                        break;
                    }
            }
        }
    }

    public abstract class GenerationInfo
    {
        public required ClassDeclarationSyntax ClassSymbol { get; init; }

        public override int GetHashCode()
        {
            return ClassSymbol.GetHashCode();
        }

        public class ClassNameEqualityComparer : EqualityComparer<GenerationInfo>
        {
            public new static ClassNameEqualityComparer Default => new();

            public override bool Equals(GenerationInfo x, GenerationInfo y) =>
                x.ClassSymbol.ToFullDisplayName() == y.ClassSymbol.ToFullDisplayName();

            public override int GetHashCode(GenerationInfo obj) => obj.ClassSymbol.ToFullDisplayName().GetHashCode();
        }

        public class ErrorNotPartial : GenerationInfo;

        public class ErrorBadBase : GenerationInfo
        {
            public required IEnumerable<string> ParentClass { get; init; }
        }

        public class Ok : GenerationInfo
        {
            public required bool GenerateConstructor { get; init; }
            public required bool GenerateConstruct { get; init; }
            public required string BaseHandleType { get; init; }
            public required string ConstructorVisibility { get; init; }
            public required bool IsSealedOrAbstract { get; init; }
            public required bool GenerateOwns { get; init; }
            public required bool GenerateDoesntOwn { get; init; }
        }
    }
}