// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace MMKiwi.GdalNet.InteropSourceGen;

[Generator]
public class ConstructGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Do a simple filter for methods
        IncrementalValuesProvider<GenerationInfo> methodDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: (node, _) => node is ClassDeclarationSyntax, // select methods with attributes
                transform: GetMethodToGenerate) // sect the methods with the [GdalWrapperMethod] attribute
            .Where(static m => m.ClassSymbol is not null); // filter out attributed methods that we don't care about

        // Combine the selected methods with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<GenerationInfo>)> compilationAndMethods
            = context.CompilationProvider.Combine(methodDeclarations.Collect());

        // Generate the source using the compilation and methods
        context.RegisterSourceOutput(compilationAndMethods, static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static GenerationInfo GetMethodToGenerate(GeneratorSyntaxContext context, CancellationToken ct)
    {
        // we know the node is a MethodDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)context.Node;

        INamedTypeSymbol classSymbol = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(classSyntax)!;

        foreach (INamedTypeSymbol baseInterface in classSymbol.Interfaces)
        {
            if (baseInterface.IsGenericType && baseInterface.ConstructedFrom.ToDisplayString() is "MMKiwi.GdalNet.IConstructibleWrapper<TRes, THandle>")
            {
                var methodsToGenerate = ImmutableList.CreateBuilder<ISymbol>();
                foreach (ISymbol member in baseInterface.GetMembers())
                {
                    if (member is IMethodSymbol methodSymbol && classSymbol.FindImplementationForInterfaceMember(member) is null)
                    {
                        // Get handle type
                        ITypeSymbol handleType = baseInterface.TypeArguments[1];

                        //check for constructor
                        bool needsConstructor = !classSymbol.Constructors.Any(c => c.Parameters.Length == 1 && c.Parameters[0].Type.Equals(handleType, SymbolEqualityComparer.Default));
                        return new(classSyntax, methodSymbol, needsConstructor);
                    }
                }
            }
        }

        // we didn't find the attribute we were looking for
        return default;
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
            if (cls.ClassSymbol is null || cls.ConstructMethod is null)
                continue;
            // generate the source code and add it to the output
            string? result = ConstructGenerationHelper.GenerateExtensionClass(compilation, cls!, context);
            if (result != null)
                context.AddSource($"Construct.{cls.ClassSymbol.ToFullDisplayName()}.g.cs", SourceText.From(result, Encoding.UTF8));
        }
    }

    public readonly struct GenerationInfo
    {
        public GenerationInfo(ClassDeclarationSyntax classSymbol, IMethodSymbol constructMethod, bool needsConstructor)
        {
            ClassSymbol = classSymbol;
            ConstructMethod = constructMethod;
            NeedsConstructor = needsConstructor;
        }

        public ClassDeclarationSyntax ClassSymbol { get; }
        public IMethodSymbol ConstructMethod { get; }
        public bool NeedsConstructor { get; }

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
