// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MMKiwi.GdalNet.InteropSourceGen;

public static class Extensions
{
    public static string ToDiagString(this MethodDeclarationSyntax syntax)
    {
        StringBuilder builder = new();
        builder.Append(syntax.Identifier);
        builder.Append('(');
        builder.Append(string.Join(",", syntax.ParameterList.Parameters.Select(p => p.Type)));
        builder.Append(')');
        return builder.ToString();
    }

    public static string ToFullDisplayName(this TypeDeclarationSyntax typeDeclaration)
    {
        StringBuilder res = new(typeDeclaration.Identifier.Text);
        var parent = typeDeclaration.Parent;
        while (parent is not null or CompilationUnitSyntax)
        {
            if (parent is TypeDeclarationSyntax tds)
            {
                res.Append($".{tds.Identifier}");
            }
            else if (parent is BaseNamespaceDeclarationSyntax nds)
            {
                res.Append($".{nds.Name}");
            }
            parent = parent.Parent;
        }

        return res.ToString();
    }

    public class RemoveAttributeRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitAttributeList(AttributeListSyntax node)  => default;
    }

    public static SyntaxNode RemoveAttributes(this ParameterListSyntax parameters)
    {
        var rewriter = new RemoveAttributeRewriter();
        var result = rewriter.Visit(parameters);
        return result;
    }
}