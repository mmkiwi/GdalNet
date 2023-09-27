// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace MMKiwi.GdalNet.SourceGenerators;

[Generator]
public class MarshalGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Add the marker attribute to the compilation
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource($"implementation/{MarshalHelper.AttributeName}.g.cs", SourceText.From(MarshalHelper.Attribute, Encoding.UTF8)));

        // Do a simple filter for classes
        IncrementalValuesProvider<ClassDeclarationSyntax> classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // select classes with attributes
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // sect the classes with the [GenerateGdalMarshal] attribute
            .Where(static m => m is not null)!; // filter out attributed classes that we don't care about

        // Combine the selected enums with the `Compilation`
        IncrementalValueProvider<(Compilation Compilation, ImmutableArray<ClassDeclarationSyntax> Classes)> compilationAndClasses
            = context.CompilationProvider.Combine(classDeclarations.Collect());

        IncrementalValueProvider<((Compilation Compilation, ImmutableArray<ClassDeclarationSyntax> Classes) Left, ParseOptions Parse)> compilationAndClassesAndParse = compilationAndClasses.Combine(context.ParseOptionsProvider);

        // Generate the source using the compilation and enums
        context.RegisterSourceOutput(compilationAndClassesAndParse,
            static (spc, source) => Execute(source.Left.Compilation, source.Left.Classes, source.Parse, spc));
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        => node is ClassDeclarationSyntax m && m.AttributeLists.Count > 0;


    static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a EnumDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        // loop through all the attributes on the method
        foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
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

                // Is the attribute the [GenerateGdalMarshal] attribute?
                if (fullName == MarshalHelper.FullAttributeName)
                {
                    // return the enum
                    return classDeclarationSyntax;
                }
            }
        }

        // we didn't find the attribute we were looking for
        return null;
    }

    static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, ParseOptions parse, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }

        // I'm not sure if this is actually necessary, but `[LoggerMessage]` does it, so seems like a good idea!
        IEnumerable<ClassDeclarationSyntax> distinctClasses = classes.Distinct();

        // Convert each EnumDeclarationSyntax to an EnumToGenerate
        List<MarshalClassToGenerate> classesToGenerate = GetTypesToGenerate(compilation, distinctClasses, context, context.CancellationToken);

        // If there were errors in the EnumDeclarationSyntax, we won't create an
        // EnumToGenerate for it, so make sure we have something to generate
        foreach (var @class in classesToGenerate)
        {
            if (parse.PreprocessorSymbolNames.Contains("GDAL_GENERATE_STUBS"))
            {
                string stub = MarshalHelper.GenerateMarshalStub(@class);
                context.AddSource($"stub/{@class.Namespace}{@class.Name}.g.cs", SourceText.From(stub, Encoding.UTF8));
            }
            string result = MarshalHelper.GenerateMarshalClass(@class);
            context.AddSource($"implementation/{@class.Namespace}{@class.Name}.g.cs", SourceText.From(result, Encoding.UTF8));
        }
    }


    static List<MarshalClassToGenerate> GetTypesToGenerate(Compilation compilation, IEnumerable<ClassDeclarationSyntax> classes, SourceProductionContext context, CancellationToken ct)
    {
        // Create a list to hold our output
        var classesToGenerate = new List<MarshalClassToGenerate>();
        // Get the semantic representation of our marker attribute 
        INamedTypeSymbol? classAttibute = compilation.GetTypeByMetadataName(MarshalHelper.FullAttributeName);

        if (classAttibute == null)
        {
            // If this is null, the compilation couldn't find the marker attribute type
            // which suggests there's something very wrong! Bail out..
            return classesToGenerate;
        }

        foreach (ClassDeclarationSyntax classDeclarationSyntax in classes)
        {
            // stop if we're asked to
            ct.ThrowIfCancellationRequested();

            // Get the semantic representation of the enum syntax
            SemanticModel semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
            {
                // something went wrong, bail out
                continue;
            }

            // Get the full type name of the enum e.g. Colour, 
            // or OuterClass<T>.Colour if it was nested in a generic type (for example)
            string className = classSymbol.Name;
            string? classNamespace = classDeclarationSyntax.GetNamespaceFrom()?.Name.ToString();

            var baseSymbol = classSymbol.BaseType;
            MarshalHidingType isHiding = MarshalHidingType.None;
            MarshalBaseType baseType = MarshalBaseType.Unknown;
            while (baseSymbol != null)
            {
                if (baseSymbol.Name == "GdalSafeHandle")
                {
                    baseType = MarshalBaseType.GdalSafeHandle;
                }
                else if (baseSymbol.Name == "GdalHandle" && baseType != MarshalBaseType.GdalSafeHandle)
                {
                    baseType = MarshalBaseType.GdalHandle;
                }
                if (baseSymbol.GetAttributes().Any(a => a.AttributeClass?.Equals(classAttibute, SymbolEqualityComparer.Default) ?? false))
                {
                    isHiding = baseSymbol.IsAbstract ? MarshalHidingType.In : MarshalHidingType.InOut;
                }

                baseSymbol = baseSymbol.BaseType;
            }

            if (baseType == MarshalBaseType.Unknown)
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDAL1001",
                                                                                    "Invalid base type",
                                                                                    "Could not generate marshalling for type {0} because it does not inherit from GdalHandle",
                                                                                    "GDAL",
                                                                                    DiagnosticSeverity.Warning,
                                                                                    true,
                                                                                    customTags: [className]),
                                                                                    GetAtrributeLocation(classDeclarationSyntax, semanticModel)));
            }

            ImmutableArray<ISymbol> allMembers = semanticModel.GetDeclaredSymbol(classDeclarationSyntax)?.GetMembers() ?? ImmutableArray<ISymbol>.Empty;
            bool hasConstructor = FindConstructor(compilation, allMembers, baseType);

            // Create an EnumToGenerate for use in the generation phase
            classesToGenerate.Add(new MarshalClassToGenerate(className, classNamespace, baseType, hasConstructor, semanticModel.GetDeclaredSymbol(classDeclarationSyntax)!.IsAbstract, isHiding));
        }

        return classesToGenerate;

        static Location? GetAtrributeLocation(ClassDeclarationSyntax classDeclarationSyntax, SemanticModel semanticModel)
        {
            foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                {
                    if (semanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                    {
                        // weird, we couldn't get the symbol, ignore it
                        continue;
                    }

                    INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                    string fullName = attributeContainingTypeSymbol.ToDisplayString();

                    // Is the attribute the [GenerateGdalMarshal] attribute?
                    if (fullName == MarshalHelper.FullAttributeName)
                    {
                        return attributeSyntax.GetLocation();
                    }
                }

            }
            return null;
        }

        static bool FindConstructor(Compilation compilation, ImmutableArray<ISymbol> allMembers, MarshalBaseType baseType)
        {

            var intPtrSyntax = compilation.GetTypeByMetadataName("System.IntPtr");
            var boolSyntax = compilation.GetTypeByMetadataName("System.Boolean");

            foreach (ISymbol member in allMembers)
            {
                if (member is IMethodSymbol ctor && ctor.MethodKind == MethodKind.Constructor)
                {
                    ImmutableArray<IParameterSymbol> parameters = ctor.Parameters;
                    if (baseType == MarshalBaseType.GdalHandle
                            && parameters.Length == 1
                            && parameters[0].Type.Equals(intPtrSyntax, SymbolEqualityComparer.Default))
                        return true;
                    if (baseType == MarshalBaseType.GdalSafeHandle
                            && parameters.Length == 2
                            && parameters[0].Type.Equals(intPtrSyntax, SymbolEqualityComparer.Default)
                            && parameters[1].Type.Equals(boolSyntax, SymbolEqualityComparer.Default))
                        return true;
                }
            }

            return false;
        }
    }

}
