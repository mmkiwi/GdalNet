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
        // Do a simple filter for methods
        IncrementalValuesProvider<MethodGenerationInfo> methodDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(InteropGenerationHelper.MarkerFullName,
                predicate: (node, _) => node is MethodDeclarationSyntax, // select methods with attributes
                transform: GetMethodToGenerate) // sect the methods with the [GdalWrapperMethod] attribute
            .Where(static m => m is not null)!; // filter out attributed methods that we don't care about

        // Combine the selected methods with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<MethodGenerationInfo>)> compilationAndMethods
            = context.CompilationProvider.Combine(methodDeclarations.Collect());

        // Generate the source using the compilation and methods
        context.RegisterSourceOutput(compilationAndMethods, static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static MethodGenerationInfo? GetMethodToGenerate(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        // we know the node is a MethodDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        IMethodSymbol methodSymbol = (IMethodSymbol)context.TargetSymbol;
        MethodDeclarationSyntax methodSyntax = (MethodDeclarationSyntax)context.TargetNode;

        string methodName = methodSymbol.Name;

        foreach (var attributeData in methodSymbol.GetAttributes()
                                                  .Where(attributeData => 
                                                      attributeData.AttributeClass?.Name == InteropGenerationHelper.MarkerClass &&
                                                      attributeData.AttributeClass.ToDisplayString() == InteropGenerationHelper.MarkerFullName))
        {
            foreach (KeyValuePair<string, TypedConstant> namedArgument in attributeData.NamedArguments)
            {
                if (namedArgument.Key == "MethodName" && namedArgument.Value.Value?.ToString() is { } ns)
                {
                    methodName = ns;
                }
            }

            return new MethodGenerationInfo(methodSyntax, methodName);
        }
        // we didn't find the attribute we were looking for
        return null;
    }

    static void Execute(Compilation compilation, ImmutableArray<MethodGenerationInfo> methods, SourceProductionContext context)
    {
        if (methods.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }

        IEnumerable<IGrouping<TypeDeclarationSyntax?, MethodGenerationInfo>> distinctClasses = methods.GroupBy(GetParentClass);

        foreach (var cls in distinctClasses)
        {
            if (cls.Key is null)
            {
                foreach (var method in cls)
                {
                    context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG0001",
                                                                                        "Could not generate method",
                                                                                        "Could not generate wrapper method for {0} because the parent class could not be found",
                                                                                        "Gdal.SourceGenerator",
                                                                                        DiagnosticSeverity.Warning,
                                                                                        true),
                                                               method.Method.GetLocation(),
                                                               method.Method.ToDiagString()));
                }
            }
            else
            {
                // generate the source code and add it to the output
                string result = InteropGenerationHelper.GenerateExtensionClass(compilation, cls!, context);
                context.AddSource($"InteropGenerator.{cls.Key.ToFullDisplayName()}.g.cs", SourceText.From(result, Encoding.UTF8));
            }
        }

        return;

        static TypeDeclarationSyntax? GetParentClass(MethodGenerationInfo method)
        {
            var parent = method.Method.Parent;
            while (parent is not null or CompilationUnitSyntax)
            {
                if (parent is TypeDeclarationSyntax parentType)
                    return parentType;
                parent = parent.Parent;
            }
            return null;
        }
    }
}
