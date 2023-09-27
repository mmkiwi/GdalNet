// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MMKiwi.GdalNet.SourceGenerators;

public static class MarshalHelper
{
    public const string AttributeNamespace = "MMKiwi.GdalNet.SourceGenerators";
    public const string AttributeName = "GenerateGdalMarshalAttribute";
    public const string FullAttributeName = $"{AttributeNamespace}.{AttributeName}";
    public const string Attribute = $$"""
        namespace {{AttributeNamespace}}
        {
            [System.AttributeUsage(System.AttributeTargets.Class)]
            internal partial class {{AttributeName}} : System.Attribute
            {
            }
        }
        """;

    public static BaseNamespaceDeclarationSyntax? GetNamespaceFrom(this SyntaxNode s)
        => s.Parent switch
        {
            BaseNamespaceDeclarationSyntax namespaceDeclarationSyntax => namespaceDeclarationSyntax,
            null => null,
            _ => GetNamespaceFrom(s.Parent)
        };

    internal static string GenerateMarshalStub(MarshalClassToGenerate m)
    {
        string inNew = m.IsHiding == MarshalHidingType.In ? "new" : "";
        string outNew = m.IsHiding == MarshalHidingType.InOut ? "new" : "";

        StringBuilder sb = new();
        if (m.Namespace is not null)
            sb.Append($$"""
                #nullable enable
                namespace {{m.Namespace}}
                {

                """);

        sb.Append($$"""
                [global::System.Runtime.InteropServices.Marshalling.NativeMarshalling(typeof(MarshalIn))]
                partial class {{m.Name}}
                {
                    [global::System.Runtime.InteropServices.Marshalling.CustomMarshaller(typeof({{m.Name}}), global::System.Runtime.InteropServices.Marshalling.MarshalMode.Default, typeof(MarshalIn))]
                    internal {{inNew}} static partial class MarshalIn
                    {
                        public static partial nint ConvertToUnmanaged({{m.Name}}? handle);
                    }

            """);

        if (m.BaseType == MarshalBaseType.GdalHandle && !m.IsAbstract)
        {
            sb.Append($$"""
                        [global::System.Runtime.InteropServices.Marshalling.CustomMarshaller(typeof({{m.Name}}), global::System.Runtime.InteropServices.Marshalling.MarshalMode.Default, typeof(MarshalDoesNotOwnHandle))]
                        internal {{outNew}} static partial class MarshalDoesNotOwnHandle
                        {
                            public static partial nint ConvertToUnmanaged({{m.Name}}? handle);
                            public static partial {{m.Name}}? ConvertToManaged(nint pointer);
                        }

                """);
        }
        else if (m.BaseType == MarshalBaseType.GdalSafeHandle && !m.IsAbstract)
        {
            sb.Append($$"""
                        [global::System.Runtime.InteropServices.Marshalling.CustomMarshaller(typeof({{m.Name}}), global::System.Runtime.InteropServices.Marshalling.MarshalMode.Default, typeof(MarshalDoesNotOwnHandle))]
                        internal {{outNew}} static partial class MarshalDoesNotOwnHandle
                        {
                            public static partial nint ConvertToUnmanaged({{m.Name}}? handle);
                            public static partial {{m.Name}}? ConvertToManaged(nint pointer);

                        }

                        [global::System.Runtime.InteropServices.Marshalling.CustomMarshaller(typeof({{m.Name}}), global::System.Runtime.InteropServices.Marshalling.MarshalMode.Default, typeof(MarshalOwnsHandle))]
                        internal {{outNew}} static partial class MarshalOwnsHandle
                        {
                            public static partial nint ConvertToUnmanaged({{m.Name}}? handle);
                            public static partial {{m.Name}}? ConvertToManaged(nint pointer);
                        }

                """);
        }

        //close class
        sb.Append($$"""
                }

            """);

        //close namespace
        if (m.Namespace is not null)
            sb.AppendLine("}");

        return sb.ToString();
    }

    internal static string GenerateMarshalClass(MarshalClassToGenerate m)
    {
        string inNew = m.IsHiding == MarshalHidingType.In ? "new" : "";
        string outNew = m.IsHiding == MarshalHidingType.InOut ? "new" : "";
        StringBuilder sb = new();
        if (m.Namespace is not null)
            sb.Append($$"""
                #nullable enable
                namespace {{m.Namespace}}
                {

                """);

        sb.Append($$"""
                partial class {{m.Name}}
                {
                    internal static partial class MarshalIn
                    {
                        public static partial nint ConvertToUnmanaged({{m.Name}}? handle) => handle is null ? 0 : handle.Handle;
                    }

            """);

        if(m.BaseType == MarshalBaseType.GdalHandle && !m.IsAbstract)
        {
            if(!m.HasConstructor)
            {
                sb.Append($$"""
                            private {{m.Name}}(nint pointer) : base(pointer) { }

                    """);
            }
            sb.Append($$"""
                        internal {{inNew}} static partial class MarshalDoesNotOwnHandle
                        {
                            public static partial nint ConvertToUnmanaged({{m.Name}}? handle) => handle is null ? 0 : handle.Handle;
                            public static partial {{m.Name}}? ConvertToManaged(nint pointer)
                            {
                                return pointer <= 0 ? null : new {{m.Name}}(pointer);
                            }
                        }

                """);
        }
        else if(m.BaseType == MarshalBaseType.GdalSafeHandle && !m.IsAbstract)
        {
            if (!m.HasConstructor)
            {
                sb.Append($$"""
                            private {{m.Name}}(nint pointer, bool ownsHandle) : base(pointer, ownsHandle) { }

                    """);
            }
            sb.Append($$"""
                        internal {{outNew}} static partial class MarshalDoesNotOwnHandle
                        {
                            public static partial nint ConvertToUnmanaged({{m.Name}}? handle) => handle is null ? 0 : handle.Handle;
                            public static partial {{m.Name}}? ConvertToManaged(nint pointer)
                            {
                                return pointer <= 0 ? null : new {{m.Name}}(pointer, false);
                            }
                        }

                        internal {{outNew}} static partial class MarshalOwnsHandle
                        {
                            public static partial nint ConvertToUnmanaged({{m.Name}}? handle) => handle is null ? 0 : handle.Handle;
                            public static partial {{m.Name}}? ConvertToManaged(nint pointer)
                            {
                                return pointer <= 0 ? null : new {{m.Name}}(pointer, true);
                            }
                        }

                """);
        }

        //close class
        sb.Append($$"""
                }

            """);

        //close namespace
        if (m.Namespace is not null)
            sb.AppendLine("}");

        return sb.ToString();
    }
}
