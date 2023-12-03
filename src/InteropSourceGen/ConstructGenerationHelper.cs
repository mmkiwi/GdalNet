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
    static readonly SymbolDisplayFormat s_symbolDisplayFormat = new(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

    internal static string? GenerateExtensionClass(Compilation compilation, ConstructGenerator.GenerationInfo classGroup, SourceProductionContext context)
    {
        StringBuilder resFile = new();

        Stack<TypeDeclarationSyntax> parentClasses = [];
        Stack<BaseNamespaceDeclarationSyntax> parentNamespaces = [];

        TypeDeclarationSyntax parentClass = classGroup.ClassSymbol;
        SyntaxNode? parent = classGroup.ClassSymbol;

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

        if (classGroup.ConstructMethod.ReceiverType is not INamedTypeSymbol reciever)
            return null;

        ITypeSymbol wrapperType = reciever.TypeArguments[0];
        ITypeSymbol handleType = reciever.TypeArguments[1];

        resFile.AppendLine($"""
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("MMKiwi.GdalNet.SourceGenerator", "0.0.1.000")]
        """);
        resFile.AppendLine($"    static global::{wrapperType.ToDisplayString()} IConstructibleWrapper<global::{wrapperType.ToDisplayString()}, global::{handleType.ToDisplayString()}>.Construct(global::{handleType.ToDisplayString()} handle) => new(handle);");

        if(classGroup.NeedsConstructor)
        {
            resFile.AppendLine($"""
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("MMKiwi.GdalNet.SourceGenerator", "0.0.1.000")]
            """);
            resFile.AppendLine($"    private {wrapperType.Name}(global::{handleType.ToDisplayString()} handle) => Handle = handle;");
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
