// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Text;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MMKiwi.GdalNet.InteropSourceGen;

public static class SourceGenerationHelper
{
    public const string MarkerNamespace = "MMKiwi.GdalNet.Interop";
    public const string MarkerClass = "GdalWrapperMethodAttribute";
    public const string MarkerFullName = $"{MarkerNamespace}.{MarkerClass}";
    public const string Attribute = $$"""
namespace {{MarkerNamespace}}
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class {{MarkerClass}} : System.Attribute
    {
    }
}
""";

    internal static string GenerateExtensionClass(MethodDeclarationSyntax method)
    {
        StringBuilder resFile = new StringBuilder("#nullable enable");
        Microsoft.CodeAnalysis.SyntaxNode? parent = method.Parent;
        Stack<ClassDeclarationSyntax> ParentClasses = [];
        Stack<BaseNamespaceDeclarationSyntax> ParentNamespaces = [];
        while(parent is not null or CompilationUnitSyntax)
        {
            if (parent is BaseNamespaceDeclarationSyntax ns)
                ParentNamespaces.Push(ns);
            else if (parent is ClassDeclarationSyntax cls)
                ParentClasses.Push(cls);

            parent = parent.Parent;
        }

        foreach(var ns in ParentNamespaces)
        {
            resFile.AppendLine($$"""namespace {{ns.Name}} {""");
        }

        foreach(var cls in ParentClasses)
        {
            resFile.AppendLine($$"""
                {{cls.Modifiers}} class {{cls.Identifier}}
                { 
                """);
        }

        resFile.AppendLine($$"""
            {{method.Modifiers}} {{method.ReturnType}} {{method.Identifier}}{{method.ParameterList}}
            {
                throw new NotImplementedException();
            }
        """);

        foreach (var ns in ParentClasses)
        {
            resFile.AppendLine("}");
        }
        foreach (var ns in ParentNamespaces)
        {
            resFile.AppendLine("}");
        }

        return resFile.ToString();
    }
}
