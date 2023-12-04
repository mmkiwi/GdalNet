// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MMKiwi.GdalNet.InteropSourceGen;

public static class ConstructGenerationHelper
{
    public const string MarkerNamespace = "MMKiwi.GdalNet.InteropAttributes";
    public const string MarkerClass = "GdalGenerateWrapperAttribute";
    public const string MarkerFullName = $"{MarkerNamespace}.{MarkerClass}";

    static readonly SymbolDisplayFormat s_symbolDisplayFormat = new(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

    internal static string? GenerateExtensionClass(Compilation compilation, ConstructGenerator.GenerationInfo genInfo, SourceProductionContext context)
    {
        StringBuilder resFile = new();

        Stack<TypeDeclarationSyntax> parentClasses = [];
        Stack<BaseNamespaceDeclarationSyntax> parentNamespaces = [];

        TypeDeclarationSyntax parentClass = genInfo.ClassSymbol;
        SyntaxNode? parent = genInfo.ClassSymbol;

        while (parent is not null)
        {
            if (parent is BaseNamespaceDeclarationSyntax ns)
                parentNamespaces.Push(ns);
            else if (parent is TypeDeclarationSyntax cls)
            {
                parentClasses.Push(cls);
            }

            if (parent is CompilationUnitSyntax cus)
            {
                foreach (var use in cus.Usings)
                    resFile.AppendLine(use.ToString());
            }

            parent = parent.Parent;
        }

        resFile.AppendLine("#nullable enable");

        foreach (var ns in parentNamespaces)
        {
            resFile.AppendLine($$"""namespace {{ns.Name}} {""");
        }

        foreach (var cls in parentClasses)
        {
            resFile.AppendLine($$"""

                {{cls.Modifiers}} {{cls.Keyword}} {{cls.Identifier}}
                { 
                """);
            cls.Members.OfType<MethodDeclarationSyntax>();
        }

        string wrapperType = genInfo.WrapperType.Split('.').Last();

        if (genInfo.NeedsConstructMethod)
        {
            resFile.AppendLine($"""

                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("MMKiwi.GdalNet.SourceGenerator", "0.0.1.000")]
            """);
            resFile.AppendLine($"    static global::{genInfo.WrapperType} IConstructibleWrapper<global::{genInfo.WrapperType}, global::{genInfo.HandleType}>.Construct(global::{genInfo.HandleType} handle) => new(handle);");
        }

        if (genInfo.NeedsConstructor)
        {
            resFile.AppendLine($"""

                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("MMKiwi.GdalNet.SourceGenerator", "0.0.1.000")]
            """);
            resFile.AppendLine($"    {genInfo.ConstructorVisibility} {wrapperType}(global::{genInfo.HandleType} handle) => Handle = handle;");
        }

        foreach (var ns in parentClasses)
        {
            resFile.AppendLine("}");
        }
        foreach (var ns in parentNamespaces)
        {
            resFile.AppendLine("}");
        }

        return resFile.ToString();
    }
}
