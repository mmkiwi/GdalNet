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
public class InteropGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Add the marker attribute to the compilation
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource("GdalWrapperMethodAttribute.g.cs",
                                                                      SourceText.From(SourceGenerationHelper.Attribute, Encoding.UTF8)));

        // Do a simple filter for enums
        IncrementalValuesProvider<MethodDeclarationSyntax> enumDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // select enums with attributes
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // sect the enum with the [EnumExtensions] attribute
            .Where(static m => m is not null)!; // filter out attributed enums that we don't care about

        // Combine the selected enums with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<MethodDeclarationSyntax>)> compilationAndEnums
            = context.CompilationProvider.Combine(enumDeclarations.Collect());

        // Generate the source using the compilation and enums
        context.RegisterSourceOutput(compilationAndEnums,
            static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node) => node is MethodDeclarationSyntax m && m.AttributeLists.Count > 0;

    static MethodDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a EnumDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var methodDeclarationSyntax = (MethodDeclarationSyntax)context.Node;

        // loop through all the attributes on the method
        foreach (AttributeListSyntax attributeListSyntax in methodDeclarationSyntax.AttributeLists)
        {
            foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    // weird, we couldn't get the symbol, ignore it
                    continue;
                }

                INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                string fullName = attributeContainingTypeSymbol.ToDisplayString();

                // Is the attribute the [EnumExtensions] attribute?
                if (fullName == SourceGenerationHelper.MarkerFullName)
                {
                    // return the enum
                    return methodDeclarationSyntax;
                }
            }
        }

        // we didn't find the attribute we were looking for
        return null;
    }

    static void Execute(Compilation compilation, ImmutableArray<MethodDeclarationSyntax> enums, SourceProductionContext context)
    {
        if (enums.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }

        // I'm not sure if this is actually necessary, but `[LoggerMessage]` does it, so seems like a good idea!
        IEnumerable<MethodDeclarationSyntax> distinctEnums = enums.Distinct();

        foreach (var method in distinctEnums)
        {
            // generate the source code and add it to the output
            string result = SourceGenerationHelper.GenerateExtensionClass(method);
            context.AddSource($"InteropGenerator.{Guid.NewGuid()}.g.cs", SourceText.From(result, Encoding.UTF8));
        }
    }
}
