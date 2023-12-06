﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet.InteropSourceGen;

[Generator]
public class HandleGenerator : IIncrementalGenerator
{
    const string AttributeFull = $"{nameof(MMKiwi)}.{nameof(GdalNet)}.{nameof(InteropAttributes)}.{nameof(GdalGenerateHandleAttribute)}";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {

        // Do a simple filter for methods
        IncrementalValuesProvider<GenerationInfo> methodDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                AttributeFull,
                predicate: (node, _) => node is ClassDeclarationSyntax, // select methods with attributes
                transform: GetClasses) // sect the methods with the [GdalWrapperMethod] attribute
            .Where(static m => m is not null)!; // filter out attributed methods that we don't care about

        // Combine the selected methods with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<GenerationInfo>)> compilationAndMethods
            = context.CompilationProvider.Combine(methodDeclarations.Collect());

        // Generate the source using the compilation and methods
        context.RegisterSourceOutput(compilationAndMethods, static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static GenerationInfo? GetClasses(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        // we know the node is a MethodDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)context.TargetNode;
        INamedTypeSymbol classSymbol = (INamedTypeSymbol)context.TargetSymbol;

        AttributeData attribute = context.Attributes[0];

        bool needsConstructMethod = false;
        bool hasConstructor = false;
        bool hasConstructMethod = false;

        MemberVisibility constructorVisibility = MemberVisibility.Protected;

        foreach (KeyValuePair<string, TypedConstant> namedArgument in attribute.NamedArguments)
        {
            if (namedArgument.Key == nameof(GdalGenerateHandleAttribute.ConstuctorVisibility) && namedArgument.Value.Value is int cv)
            {
                constructorVisibility = (MemberVisibility)cv;
            }
        }

        string? baseHandle = null;

        var parentClass = classSymbol.BaseType;
        while (parentClass != null)
        {
            if (parentClass.ToDisplayString() == "MMKiwi.GdalNet.GdalInternalHandleNeverOwns")
            {
                baseHandle = "GdalInternalHandleNeverOwns";
                break;
            }
            else if (parentClass.ToDisplayString() == "MMKiwi.GdalNet.GdalInternalHandle")
            {
                baseHandle = "GdalInternalHandle";
                break;
            }
            parentClass = parentClass.BaseType;
        }

        if (baseHandle is null)
            return null;

        foreach (INamedTypeSymbol baseInterface in classSymbol.Interfaces)
        {
            if (baseInterface.IsGenericType && baseInterface.ConstructedFrom.ToDisplayString() is "MMKiwi.GdalNet.IConstructableHandle<THandle>")
            {
                needsConstructMethod = true;
                foreach (ISymbol member in baseInterface.GetMembers())
                {
                    if (member is IMethodSymbol)
                    {
                        hasConstructMethod |= classSymbol.FindImplementationForInterfaceMember(member) is not null;
                    }
                }
            }
        }

        foreach (IMethodSymbol constructor in classSymbol.Constructors)
        {
            if (constructor.Parameters.Length == 1 && constructor.Parameters[0].Type.ToDisplayString() == "bool")
                hasConstructor = true;
        }

        return new()
        {
            ClassSymbol = classSyntax,
            GenerateConstruct = needsConstructMethod && !hasConstructMethod,
            BaseHandleType = baseHandle,
            GenerateConstructor = !hasConstructor && baseHandle == "GdalInternalHandle" && constructorVisibility != MemberVisibility.DoNotGenerate,
            ConstructorVisibility = constructorVisibility.ToStringFast()
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
            if (cls.ClassSymbol is null)
                continue;
            // generate the source code and add it to the output
            string? result = HandleGenerationHelper.GenerateExtensionClass(compilation, cls!, context);
            if (result != null)
                context.AddSource($"Construct.{cls.ClassSymbol.ToFullDisplayName()}.g.cs", SourceText.From(result, Encoding.UTF8));
        }
    }

    public class GenerationInfo
    {
        public required ClassDeclarationSyntax ClassSymbol { get; init; }
        public required bool GenerateConstructor { get; init; }
        public required bool GenerateConstruct { get; init; }
        public required string BaseHandleType { get; init; }
        public required string ConstructorVisibility { get; init; }

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