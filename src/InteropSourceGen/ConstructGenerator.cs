// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.CodeAnalysis;
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
                transform: GetMethodsToGenerate) // sect the methods with the [GdalWrapperMethod] attribute
            .Where(static m => m is not null)!; // filter out attributed methods that we don't care about

        // Combine the selected methods with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<GenerationInfo>)> compilationAndMethods
            = context.CompilationProvider.Combine(methodDeclarations.Collect());

        // Generate the source using the compilation and methods
        context.RegisterSourceOutput(compilationAndMethods, static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static GenerationInfo? GetMethodsToGenerate(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        // we know the node is a MethodDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)context.TargetNode;
        INamedTypeSymbol classSymbol = (INamedTypeSymbol)context.TargetSymbol;

        AttributeData attribute = context.Attributes.First();

        string visibility = "private";

        foreach (KeyValuePair<string, TypedConstant> namedArgument in attribute.NamedArguments)
        {
            if (namedArgument.Key == nameof(ConstructorVisibility) && namedArgument.Value.Value is int cv)
            {
                visibility = ((ConstructorVisibility)cv).ToStringFast();
            }
        }

        string? wrapperTypeStr = null;
        string? handleTypeStr = null;
        bool needsConstructor = false;
        bool needsConstructMethod = false;
        foreach (INamedTypeSymbol baseInterface in classSymbol.Interfaces)
        {
            if (baseInterface.IsGenericType && baseInterface.ConstructedFrom.ToDisplayString() is "MMKiwi.GdalNet.IConstructibleWrapper<TRes, THandle>")
            {
                foreach (ISymbol member in baseInterface.GetMembers())
                {
                    if (member is IMethodSymbol methodSymbol)
                    {
                        // Get handle type
                        ITypeSymbol handleType = baseInterface.TypeArguments[1];
                        needsConstructor |= !classSymbol.Constructors.Any(c => c.Parameters.Length == 1 && c.Parameters[0].Type.Equals(handleType, SymbolEqualityComparer.Default));

                        var receiver = (INamedTypeSymbol)methodSymbol.ReceiverType!;

                        wrapperTypeStr = receiver.TypeArguments[0].ToDisplayString();
                        handleTypeStr = receiver.TypeArguments[1].ToDisplayString();

                        needsConstructMethod |= classSymbol.FindImplementationForInterfaceMember(member) is null;
                    }
                }
            }
        }

        if (wrapperTypeStr == null || handleTypeStr == null)
        {
            return null;
        }

        return new()
        {
            ClassSymbol = classSyntax,
            NeedsConstructor = needsConstructor,
            NeedsConstructMethod = needsConstructMethod,
            ConstructorVisibility = visibility,
            HandleType = handleTypeStr,
            WrapperType = wrapperTypeStr,
        };
    }

    static void Execute(Compilation compilation, ImmutableArray<GenerationInfo> classes, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }

        foreach (GenerationInfo cls in classes.Distinct(GenerationInfo.ClassNameEqualityComparer.Default))
        {
            if (cls.ClassSymbol is null || (cls.HandleType is null && cls.WrapperType is null && cls.NeedsConstructor == false))
                continue;
            // generate the source code and add it to the output
            string? result = ConstructGenerationHelper.GenerateExtensionClass(compilation, cls!, context);
            if (result != null)
                context.AddSource($"Construct.{cls.ClassSymbol.ToFullDisplayName()}.g.cs", SourceText.From(result, Encoding.UTF8));
        }
    }

    public class GenerationInfo
    {
        public required ClassDeclarationSyntax ClassSymbol { get; init; }
        public required string WrapperType { get; init; }
        public required string HandleType { get; init; }
        public required string ConstructorVisibility { get; init; }
        public required bool NeedsConstructor { get; init; }
        public required bool NeedsConstructMethod { get; init; }

        public override int GetHashCode()
        {
            return ClassSymbol.GetHashCode();
        }

        public class ClassNameEqualityComparer : EqualityComparer<GenerationInfo>
        {
            public new static ClassNameEqualityComparer Default => new();
            public override bool Equals(GenerationInfo x, GenerationInfo y) => x.ClassSymbol.ToFullDisplayName() == y.ClassSymbol.ToFullDisplayName();

            public override int GetHashCode(GenerationInfo obj) => obj.ClassSymbol.ToFullDisplayName().GetHashCode();
        }
    }
}
