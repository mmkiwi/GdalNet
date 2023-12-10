// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MMKiwi.GdalNet.InteropSourceGen;

public static class HandleGenerationHelper
{
    public const string MarkerNamespace = "MMKiwi.GdalNet.InteropAttributes";
    public const string MarkerClass = "GdalGenerateWrapperAttribute";
    public const string MarkerFullName = $"{MarkerNamespace}.{MarkerClass}";

    static readonly SymbolDisplayFormat s_symbolDisplayFormat =
        new(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

    internal static string GenerateExtensionClass(Compilation compilation, HandleGenerator.GenerationInfo.Ok genInfo,
        SourceProductionContext context)
    {
        StringBuilder resFile = new();

        Stack<TypeDeclarationSyntax> parentClasses = [];
        Stack<BaseNamespaceDeclarationSyntax> parentNamespaces = [];

        string className = genInfo.ClassSymbol.Identifier.ToString();

        SyntaxNode? parent = genInfo.ClassSymbol;

        while (parent is not null)
        {
            switch (parent)
            {
                case BaseNamespaceDeclarationSyntax ns:
                    parentNamespaces.Push(ns);
                    break;
                case TypeDeclarationSyntax cls:
                    parentClasses.Push(cls);
                    break;
                case CompilationUnitSyntax cus:
                    foreach (var use in cus.Usings)
                        resFile.AppendLine(use.ToString());
                    break;
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
        }

        if (genInfo.GenerateConstruct)
        {
            Debug.Assert(genInfo.BaseHandleType is "GdalInternalHandle" or "GdalInternalHandleNeverOwns");
            if (genInfo.BaseHandleType is "GdalInternalHandleNeverOwns")
            {
                resFile.AppendLine($$"""
                                     
                                         [global::System.CodeDom.Compiler.GeneratedCodeAttribute("MMKiwi.GdalNet.SourceGenerator", "0.0.1.000")]
                                         static {{className}} IConstructableHandle<{{className}}>.Construct(bool ownsHandle) => new();
                                     """);
            }
            else if (genInfo.BaseHandleType is "GdalInternalHandle")
            {
                resFile.AppendLine($$"""
                                     
                                         [global::System.CodeDom.Compiler.GeneratedCodeAttribute("MMKiwi.GdalNet.SourceGenerator", "0.0.1.000")]
                                         static {{className}} IConstructableHandle<{{className}}>.Construct(bool ownsHandle) => ownsHandle ? new Owns() : new DoesntOwn();
                                     """);
            }
        }

        if (genInfo.GenerateConstructor)
        {
            resFile.AppendLine($$"""
                                 
                                     [global::System.CodeDom.Compiler.GeneratedCodeAttribute("MMKiwi.GdalNet.SourceGenerator", "0.0.1.000")]
                                     {{genInfo.ConstructorVisibility}} {{className}}(bool ownsHandle): base(ownsHandle) { }
                                 """);
        }

        for(int i = 0; i < parentClasses.Count + parentNamespaces.Count; i++)
            resFile.AppendLine("}");

        return resFile.ToString();
    }
}