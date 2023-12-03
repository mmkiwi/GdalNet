// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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

        // Do a simple filter for methods
        IncrementalValuesProvider<MethodInfo2> methodDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(SourceGenerationHelper.MarkerFullName,
                predicate: (node, _) => node is MethodDeclarationSyntax, // select methods with attributes
                transform: GetMethodToGenerate) // sect the methods with the [GdalWrapperMethod] attribute
            .Where(static m => m is not null)!; // filter out attributed methods that we don't care about

        // Combine the selected methods with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<MethodInfo2>)> compilationAndMethods
            = context.CompilationProvider.Combine(methodDeclarations.Collect());

        // Generate the source using the compilation and methods
        context.RegisterSourceOutput(compilationAndMethods, static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static MethodInfo2? GetMethodToGenerate(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        // we know the node is a MethodDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        IMethodSymbol methodSymbol = (IMethodSymbol)context.TargetSymbol;
        MethodDeclarationSyntax methodSyntax = (MethodDeclarationSyntax)context.TargetNode;

        string methodName = methodSymbol.Name;

        foreach (AttributeData attributeData in methodSymbol.GetAttributes())
        {
            if (attributeData.AttributeClass?.Name != SourceGenerationHelper.MarkerClass ||
                attributeData.AttributeClass.ToDisplayString() != SourceGenerationHelper.MarkerFullName)
            {
                continue;
            }

            foreach (KeyValuePair<string, TypedConstant> namedArgument in attributeData.NamedArguments)
            {
                if (namedArgument.Key == "MethodName" && namedArgument.Value.Value?.ToString() is { } ns)
                {
                    methodName = ns;
                }
            }

            return new(methodSyntax, methodName);
        }
        // we didn't find the attribute we were looking for
        return null;
    }

    static void Execute(Compilation compilation, ImmutableArray<MethodInfo2> methods, SourceProductionContext context)
    {
        if (methods.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }

        static TypeDeclarationSyntax? GetParentClass(MethodInfo2 method)
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

        IEnumerable<IGrouping<TypeDeclarationSyntax?, MethodInfo2>> distinctClasses = methods.GroupBy(GetParentClass);

        foreach (var cls in distinctClasses)
        {
            if (cls.Key is null)
            {
                foreach (var method in cls)
                {
                    context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG0001",
                                                                                        "Could not generate method",
                                                                                        "Could not generate wrapper method for {0} because the parent class could not be found",
                                                                                        "GDal.SourceGenerator",
                                                                                        DiagnosticSeverity.Warning,
                                                                                        true),
                                                               method.Method.GetLocation(),
                                                               method.Method.ToDiagString()));
                }
            }
            else
            {
                // generate the source code and add it to the output
                string result = SourceGenerationHelper.GenerateExtensionClass(compilation, cls!, context);
                context.AddSource($"InteropGenerator.{cls.Key.ToFullDisplayName()}.g.cs", SourceText.From(result, Encoding.UTF8));
            }
        }
    }
}

public record class MethodInfo2(MethodDeclarationSyntax Method, string TargetName);