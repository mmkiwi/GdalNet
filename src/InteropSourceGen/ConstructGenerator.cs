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
public class ConstructGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Do a simple filter for methods
        IncrementalValuesProvider<GenerationInfo> methodDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                ConstructGenerationHelper.MarkerFullName,
                predicate: (node, _) => node is ClassDeclarationSyntax, // select methods with attributes
                transform: GetMethodsToGenerate); // sect the methods with the [GdalWrapperMethod] attribute

        // Combine the selected methods with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<GenerationInfo>)> compilationAndMethods
            = context.CompilationProvider.Combine(methodDeclarations.Collect());

        // Generate the source using the compilation and methods
        context.RegisterSourceOutput(compilationAndMethods,
            static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static GenerationInfo GetMethodsToGenerate(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        // we know the node is a MethodDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)context.TargetNode;
        INamedTypeSymbol classSymbol = (INamedTypeSymbol)context.TargetSymbol;

        if (!classSyntax.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)))
        {
            return new GenerationInfo.ErrorNotPartial { ClassSyntax = classSyntax };
        }

        AttributeData attribute = context.Attributes.First();

        MemberVisibility ctorVisibility = MemberVisibility.Private;
        MemberVisibility handleVisibility = MemberVisibility.Internal;
        MemberVisibility handleSetVisibility = MemberVisibility.DoNotGenerate;

        foreach (KeyValuePair<string, TypedConstant> namedArgument in attribute.NamedArguments)
        {
            switch (namedArgument.Key)
            {
                case nameof(GdalGenerateWrapperAttribute.ConstructorVisibility)
                    when namedArgument.Value.Value is int cv:
                    ctorVisibility = (MemberVisibility)cv;
                    break;
                case nameof(GdalGenerateWrapperAttribute.HandleVisibility) when namedArgument.Value.Value is int hv:
                    handleVisibility = (MemberVisibility)hv;
                    break;
                case nameof(GdalGenerateWrapperAttribute.HandleSetVisibility) when namedArgument.Value.Value is int mh:
                    handleSetVisibility = (MemberVisibility)mh;
                    break;
            }
        }

        string wrapperTypeStr = classSymbol.ToDisplayString();
        string? handleTypeStr = null;

        // Only needed iif the class implements IConstructableWrapper, so start by assuming they aren't needed
        // until that interface is encountered
        bool hasConstructMethod = true;

        // Only needed iif the class implements IHasHandle, so start by assuming they aren't needed
        // until that interface is encountered
        bool hasConstructor = true;
        bool hasExplicitHandle = true;
        bool hasImplicitHandle = true;

        bool needsIDisposable = false;
        bool hasIDisposable = false;

        foreach (INamedTypeSymbol baseInterface in classSymbol.Interfaces)
        {
            if (baseInterface.IsGenericType &&
                baseInterface.ConstructedFrom.ToDisplayString() is
                    "MMKiwi.GdalNet.IConstructableWrapper<TRes, THandle>")
            {
                hasConstructMethod = false;
                foreach (ISymbol member in baseInterface.GetMembers())
                {
                    if (member is IMethodSymbol)
                    {
                        ITypeSymbol handleType = baseInterface.TypeArguments[1];

                        handleTypeStr = handleType.ToDisplayString();

                        hasConstructMethod |= classSymbol.FindImplementationForInterfaceMember(member) is not null;
                    }
                }
            }

            if (baseInterface.IsGenericType &&
                baseInterface.ConstructedFrom.ToDisplayString() is "MMKiwi.GdalNet.IHasHandle<THandle>")
            {
                // These are only true if it implements IHasHandle
                hasExplicitHandle = false;
                hasConstructor = false;
                hasImplicitHandle = false;

                ITypeSymbol handleType = baseInterface.TypeArguments[0];
                handleTypeStr = handleType.ToDisplayString();

                var handleParent = handleType;
                while (handleParent is not null)
                {
                    if (handleParent.ToDisplayString() == "MMKiwi.GdalNet.GdalInternalHandleNeverOwns")
                    {
                        needsIDisposable = false;
                        break;
                    }

                    if (handleParent.ToDisplayString() == "MMKiwi.GdalNet.GdalInternalHandle")
                    {
                        needsIDisposable = true;
                        break;
                    }

                    handleParent = handleParent.BaseType;
                }

                foreach (var member in Enumerable.OfType<IPropertySymbol>(baseInterface.GetMembers()))
                {
                    // Get handle type
                    hasExplicitHandle |= classSymbol.FindImplementationForInterfaceMember(member) is not null;

                    for (int index = 0; index < classSymbol.Constructors.Length; index++)
                    {
                        IMethodSymbol implementedCtor = classSymbol.Constructors[index];
                        if (implementedCtor.Parameters.Length == 1 && implementedCtor.Parameters[0].Type
                                .Equals(handleType, SymbolEqualityComparer.Default))
                        {
                            hasConstructor = true;
                        }
                    }

                    foreach (var implementedMember in classSymbol.GetMembers().OfType<IPropertySymbol>())
                    {
                        if (implementedMember.Name == "Handle" &&
                            implementedMember.Type.Equals(handleType, SymbolEqualityComparer.IncludeNullability))
                        {
                            hasImplicitHandle = true;
                        }
                    }
                }
            }

            if (baseInterface.ToDisplayString() == "System.IDisposable")
            {
                hasIDisposable = true;
            }
        }

        if (handleTypeStr == null)
        {
            return new GenerationInfo.ErrorDoesNotImplement { ClassSyntax = classSyntax };
        }

        if (needsIDisposable && !hasIDisposable) // Check to see if parent classes have IDisposable
        {
            hasIDisposable = FindIDisposableInParent(classSymbol);
        }

        return new GenerationInfo.Ok
        {
            ClassSyntax = classSyntax,
            NeedsConstructor = !hasConstructor,
            NeedsConstructMethod = !hasConstructMethod && ctorVisibility != MemberVisibility.DoNotGenerate,
            ConstructorVisibility = ctorVisibility.ToStringFast(),
            HandleType = handleTypeStr,
            WrapperType = wrapperTypeStr,
            NeedsExplicitHandle = !hasExplicitHandle,
            NeedsImplicitHandle = !hasImplicitHandle && handleVisibility != MemberVisibility.DoNotGenerate,
            HandleVisibility = handleVisibility.ToStringFast(),
            HandleSetVisibility = handleSetVisibility.ToStringFast(),
            MissingIDisposable = needsIDisposable && !hasIDisposable
        };

        static bool FindIDisposableInParent(INamedTypeSymbol classSymbol)
        {
            var parent = classSymbol.BaseType;
            while (parent != null)
            {
                foreach (var baseInterface in parent.Interfaces)
                {
                    if (baseInterface.ToDisplayString() == "System.IDisposable")
                    {
                        return true;
                    }
                }

                parent = parent.BaseType;
            }

            return false;
        }
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
            if (cls is GenerationInfo.ErrorNotPartial)
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG00010",
                        "Class must be partial",
                        "Class {0} must be partial for the source generator to work.",
                        "GDal.SourceGenerator",
                        DiagnosticSeverity.Warning,
                        true),
                    cls.ClassSyntax.GetLocation(),
                    cls.ClassSyntax.Identifier));
            }
            else if (cls is GenerationInfo.ErrorDoesNotImplement)
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG00011",
                        "Class does not implement IConstructableHandle or IHasHandle",
                        "Class {0} must implement IConstructableHandle or IHasHandle to generate the wrapper type.",
                        "GDal.SourceGenerator",
                        DiagnosticSeverity.Warning,
                        true),
                    cls.ClassSyntax.GetLocation(),
                    cls.ClassSyntax.Identifier));
            }
            else if (cls is GenerationInfo.Ok genInfo)
            {
                if (genInfo.NeedsConstructMethod is false &&
                    genInfo.NeedsExplicitHandle is false &&
                    genInfo.NeedsImplicitHandle is false &&
                    genInfo.NeedsConstructor is false)
                    continue;

                if (genInfo.MissingIDisposable)
                {
                    context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG0008",
                            "Missing IDisposable",
                            "Class {0} implements IHasHandle, but does not implement IDisposable.",
                            "GDal.SourceGenerator",
                            DiagnosticSeverity.Warning,
                            true),
                        cls.ClassSyntax.GetLocation(),
                        cls.ClassSyntax.Identifier));
                }

                // generate the source code and add it to the output
                string result = ConstructGenerationHelper.GenerateExtensionClass(compilation, genInfo, context);
                context.AddSource($"Construct.{cls.ClassSyntax.ToFullDisplayName()}.g.cs",
                    SourceText.From(result, Encoding.UTF8));
            }
        }
    }

    public abstract class GenerationInfo
    {
        public required ClassDeclarationSyntax ClassSyntax { get; init; }

        public class ErrorNotPartial : GenerationInfo;

        public class ErrorDoesNotImplement : GenerationInfo;

        public class Ok : GenerationInfo
        {
            public required string WrapperType { get; init; }
            public required string HandleType { get; init; }
            public required string ConstructorVisibility { get; init; }
            public required bool NeedsConstructor { get; init; }
            public required bool NeedsConstructMethod { get; init; }
            public required bool NeedsExplicitHandle { get; init; }
            public required bool NeedsImplicitHandle { get; init; }

            public required string HandleVisibility { get; init; }
            public required string? HandleSetVisibility { get; init; }
            public bool MissingIDisposable { get; internal set; }
        }

        public override int GetHashCode()
        {
            return ClassSyntax.GetHashCode();
        }

        public class ClassNameEqualityComparer : EqualityComparer<GenerationInfo>
        {
            public new static ClassNameEqualityComparer Default => new();

            public override bool Equals(GenerationInfo x, GenerationInfo y) =>
                x.ClassSyntax.ToFullDisplayName() == y.ClassSyntax.ToFullDisplayName();

            public override int GetHashCode(GenerationInfo obj) => obj.ClassSyntax.ToFullDisplayName().GetHashCode();
        }
    }
}