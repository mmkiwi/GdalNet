﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MMKiwi.GdalNet.InteropSourceGen;

public static class SourceGenerationHelper
{
    public const string MarkerNamespace = "MMKiwi.GdalNet.Interop";
    public const string MarkerClass = "GdalWrapperMethodAttribute";
    public const string HelperClass = "GdalConstructionHelper";
    public const string MarkerFullName = $"{MarkerNamespace}.{MarkerClass}";
    public const string Attribute = $$"""
#nullable enable 
namespace {{MarkerNamespace}}
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class {{MarkerClass}} : System.Attribute
    {
        public string? MethodName {get; set;}
    }

    internal static class {{HelperClass}}
    {
        public static TRes Construct<TRes,THandle>(THandle handle) 
            where TRes: class, IConstructibleWrapper<TRes,THandle>
            where THandle: GdalInternalHandle
        {
            if(handle.IsInvalid) 
                throw new InvalidOperationException("Cannot marshal null handle");
            return TRes.Construct(handle);
        }

        public static TRes? ConstructNullable<TRes,THandle>(THandle handle) 
            where TRes: class, IConstructibleWrapper<TRes,THandle>
            where THandle: GdalInternalHandle
        {
            return handle.IsInvalid ? null : TRes.Construct(handle);
        }

        public static THandle GetNullHandle<THandle>()
            where THandle:GdalInternalHandle, IConstructibleHandle<THandle>
        {
            return THandle.Construct(false);
        }
    }
}
""";

    static readonly SymbolDisplayFormat s_symbolDisplayFormat = new(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

    internal static string GenerateExtensionClass(Compilation compilation, IGrouping<TypeDeclarationSyntax, MethodInfo2> classGroup, SourceProductionContext context)
    {
        StringBuilder resFile = new();

        Stack<TypeDeclarationSyntax> parentClasses = [];
        Stack<BaseNamespaceDeclarationSyntax> parentNamespaces = [];

        TypeDeclarationSyntax parentClass = classGroup.Key;
        SyntaxNode? parent = classGroup.Key;

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
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("MMKiwi.GdalNet.SourceGenerator", "0.0.1.000")]
                {{cls.Modifiers}} {{cls.Keyword}} {{cls.Identifier}}
                { 
                """);
            cls.Members.OfType<MethodDeclarationSyntax>();
        }

        foreach (MethodInfo2 methodInfo in classGroup)
        {
            var method = methodInfo.Method;
            if (!method.Modifiers.Any(mod => mod.IsKind(SyntaxKind.PartialKeyword)))
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG0002",
                                                                                    "Method must be partial",
                                                                                    "Could not generate wrapper method for {0} because it is not partial",
                                                                                    "GDal.SourceGenerator",
                                                                                    DiagnosticSeverity.Warning,
                                                                                    true),
                                                           method.GetLocation(),
                                                           method.ToDiagString()));
                continue;
            }

            //Find the raw method to call
            MethodTransformations? interopMethod = FindInteropMethod(parentClass, methodInfo, compilation, context);

            if (interopMethod == null)
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG0003",
                                                                                    "Could not generate wrapper method",
                                                                                    "Could not generate wrapper method for {0}.",
                                                                                    "GDal.SourceGenerator",
                                                                                    DiagnosticSeverity.Warning,
                                                                                    true),
                                                           method.GetLocation(),
                                                           method.ToDiagString()));
                resFile.AppendLine($$"""
                    {{method.Modifiers}} {{method.ReturnType}} {{method.Identifier}}{{method.ParameterList.RemoveAttributes()}}
                    {
                        throw new NotImplementedException();
                    }
                """);
                continue;
            }

            resFile.AppendLine($$"""
            {{method.Modifiers}} {{method.ReturnType}} {{method.Identifier}}{{method.ParameterList.RemoveAttributes()}}
            {
        {{GenerateMethod(method, interopMethod)}}
            }
        """);
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

    private static string GenerateMethod(MethodDeclarationSyntax method, MethodTransformations interopMethod)
    {
        const string space = "        ";
        StringBuilder methodString = new();

        if (interopMethod.Return == TransformType.WrapperOut)
        {
            methodString.AppendLine($"{space}{method.ReturnType} __return_value = null!;");
            methodString.AppendLine($"{space}{interopMethod.InteropMethod.ReturnType} __return_value_raw;");
        }
        else if (interopMethod.Return == TransformType.Direct)
        {
            methodString.AppendLine($"{space}{method.ReturnType} __return_value;");
        }

        foreach (var param in interopMethod.Parameters)
        {
            if (param.TransformType == TransformType.WrapperIn)
            {
                if (param.WrapperParam!.Type is NullableTypeSyntax)
                {
                    methodString.AppendLine($"{space}{param.InteropParam.Type} __param_{param.WrapperParam.Identifier} = ({param.WrapperParam.Identifier} as IHasHandle<{param.InteropParam.Type}>)?.Handle ?? {MarkerNamespace}.{HelperClass}.GetNullHandle<{param.InteropParam.Type}>();");
                }
                else
                {
                    methodString.AppendLine($"{space}ArgumentNullException.ThrowIfNull({param.WrapperParam.Identifier});");
                    methodString.AppendLine($"{space}{param.InteropParam.Type} __param_{param.WrapperParam.Identifier} = ((IHasHandle<{param.InteropParam.Type}>){param.WrapperParam.Identifier}).Handle;");
                }
            }

            else if (param.TransformType == TransformType.WrapperOut)
            {
                methodString.AppendLine($"{space}{param.InteropParam.Type} __out_{param.InteropParam.Identifier}_raw;");
            }
            else if (param.TransformType == TransformType.WrapperRef)
            {
                if (param.WrapperParam!.Type is NullableTypeSyntax)
                {
                    methodString.AppendLine($"{space}{param.InteropParam.Type} __ref_{param.InteropParam.Identifier}_raw = ({param.WrapperParam.Identifier} as IHasHandle<{param.InteropParam.Type}>)?.Handle ?? {MarkerNamespace}.{HelperClass}.GetNullHandle<{param.InteropParam.Type}>();");
                }
                else
                {
                    methodString.AppendLine($"{space}ArgumentNullException.ThrowIfNull({param.WrapperParam.Identifier});");
                    methodString.AppendLine($"{space}{param.InteropParam.Type} __ref_{param.InteropParam.Identifier}_raw = ((IHasHandle<{param.InteropParam.Type}>){param.WrapperParam.Identifier}).Handle");
                }
            }
        }

        methodString.Append(space);
        if (interopMethod.Return == TransformType.Direct)
        {
            methodString.Append(" __return_value =");
        }

        else if (interopMethod.Return == TransformType.WrapperOut)
        {
            methodString.Append(" __return_value_raw =");
        }

        methodString.Append($" {interopMethod.InteropMethod.Identifier}(");

        bool isFirst = true;
        foreach (var param in interopMethod.Parameters)
        {
            if (!isFirst)
            {
                methodString.Append(", ");
            }
            isFirst = false;
            if (param.TransformType == TransformType.WrapperIn)
            {
                methodString.Append($"__param_{param.WrapperParam!.Identifier}");
            }
            else if (param.TransformType == TransformType.Direct)
            {
                methodString.Append($"{param.WrapperParam!.Identifier}");
            }
            else if (param.TransformType == TransformType.DirectOut)
            {
                methodString.Append($"out {param.WrapperParam!.Identifier}");
            }
            else if (param.TransformType == TransformType.DirectIn)
            {
                methodString.Append($"in {param.WrapperParam!.Identifier}");
            }
            else if (param.TransformType == TransformType.DirectRef)
            {
                methodString.Append($"ref {param.WrapperParam!.Identifier}");
            }
            else if (param.TransformType == TransformType.WrapperOut)
            {
                methodString.Append($"out __out_{param.InteropParam.Identifier}_raw");
            }
            else if (param.TransformType == TransformType.WrapperRef)
            {
                methodString.Append($"ref __ref_{param.InteropParam.Identifier}_raw");
            }
        }

        methodString.AppendLine(");");

        foreach (var param in interopMethod.Parameters)
        {
            if (param.TransformType == TransformType.WrapperOut)
            {
                if (param.WrapperParam!.Type is NullableTypeSyntax nts)
                {
                    methodString.AppendLine($"{space}{param.InteropParam.Identifier} =  {MarkerNamespace}.{HelperClass}.ConstructNullable<{nts.ElementType}, {param.InteropParam.Type}>(__out_{param.InteropParam.Identifier}_raw);");
                }
                else
                {
                    methodString.AppendLine($"{space}{param.InteropParam.Identifier} =  {MarkerNamespace}.{HelperClass}.Construct<{param.WrapperParam.Type}, {param.InteropParam.Type}>(__out_{param.InteropParam.Identifier}_raw);");
                }
            }
            else if (param.TransformType == TransformType.WrapperRef)
            {
                if (param.WrapperParam!.Type is NullableTypeSyntax nts)
                {
                    methodString.AppendLine($"{space}{param.InteropParam.Identifier} =  {MarkerNamespace}.{HelperClass}.ConstructNullable<{nts.ElementType}, {param.InteropParam.Type}>(__ref_{param.InteropParam.Identifier}_raw);");
                }
                else
                {
                    methodString.AppendLine($"{space}{param.InteropParam.Identifier} =  {MarkerNamespace}.{HelperClass}.Construct<{param.WrapperParam.Type}, {param.InteropParam.Type}>(__ref_{param.InteropParam.Identifier}_raw);");
                }
            }
        }

        if (interopMethod.Return == TransformType.Direct)
        {
            methodString.AppendLine($"{space}return __return_value;");
        }

        else if (interopMethod.Return == TransformType.WrapperOut)
        {
            if (method.ReturnType is NullableTypeSyntax nts)
                methodString.AppendLine($"{space}__return_value = {MarkerNamespace}.{HelperClass}.ConstructNullable<{nts.ElementType}, {interopMethod.InteropMethod.ReturnType}>(__return_value_raw);");
            else
                methodString.AppendLine($"{space}__return_value = {MarkerNamespace}.{HelperClass}.Construct<{method.ReturnType}, {interopMethod.InteropMethod.ReturnType}>(__return_value_raw);");
            methodString.AppendLine($"{space}return __return_value;");
        }

        return methodString.ToString();
    }

    private static bool CheckForLibraryImport(MethodDeclarationSyntax candidateSibling, Compilation compilation)
    {
        foreach (var att in candidateSibling.AttributeLists.SelectMany(attList => attList.Attributes))
        {
            var att2 = att.Name;
            if (compilation.GetSemanticModel(candidateSibling.SyntaxTree).GetSymbolInfo(att).Symbol is not IMethodSymbol attributeSymbol)
            {
                continue;
            }

            INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
            string fullName = attributeContainingTypeSymbol.ToDisplayString();

            if (fullName == "System.Runtime.InteropServices.LibraryImportAttribute")
                return true;
        }
        return false;
    }

    private static MethodTransformations? FindInteropMethod(TypeDeclarationSyntax parentClass, MethodInfo2 methodInfo, Compilation compilation, SourceProductionContext context)
    {
        var wrapperMethod = methodInfo.Method;
        //For now, name must be the same. TODO: Add parameter to attribute to override
        foreach (MethodDeclarationSyntax candidateInterop in parentClass.Members.OfType<MethodDeclarationSyntax>())
        {
            if (candidateInterop.Identifier.ToFullString() != methodInfo.TargetName) // Name must match
                continue;

            if (candidateInterop.IsEquivalentTo(wrapperMethod)) // Cant match on itself
                continue;

            if (candidateInterop.ParameterList.Parameters.Count != wrapperMethod.ParameterList.Parameters.Count) // Parameter count must be the same
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG0005",
                                                                                    "Partial match",
                                                                                    "Skipping match for method {0}. Method {1} does not have the same number of parameters.",
                                                                                    "GDal.SourceGenerator",
                                                                                    DiagnosticSeverity.Warning,
                                                                                    true),
                                                           wrapperMethod.GetLocation(),
                                                           wrapperMethod.ToDiagString(), candidateInterop.ToDiagString()));
                continue;
            }

            if (!CheckForLibraryImport(candidateInterop, compilation)) // Must have [LibraryImport]
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG0005",
                                                                                    "Partial match",
                                                                                    "Skipping match for method {0}. Method {1} is missing the LibraryImport attribute.",
                                                                                    "GDal.SourceGenerator",
                                                                                    DiagnosticSeverity.Warning,
                                                                                    true),
                                           wrapperMethod.GetLocation(),
                                           wrapperMethod.ToDiagString(), candidateInterop.ToDiagString()));
                continue;
            }

            ImmutableArray<ParameterCompatibility> parameters = IterateParameters(wrapperMethod, compilation, candidateInterop);
            var invalidParameters = parameters.Where(p => p.TransformType is TransformType.Invalid);
            if (invalidParameters.Any()) // One or more invalid parameters
            {
                foreach (var invalidParam in invalidParameters)
                    context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG0005",
                                                                                        "Partial match",
                                                                                        "Skipping match for method {0}. Parameter {1} of method {2} cannot be matched.",
                                                                                        "GDal.SourceGenerator",
                                                                                        DiagnosticSeverity.Warning,
                                                                                        true),
                                                               wrapperMethod.GetLocation(),
                                                               wrapperMethod.ToDiagString(), invalidParam.InteropParam?.Identifier, candidateInterop.ToDiagString()));
                continue;
            }

            var returnCompatibility = CheckReturn(wrapperMethod, candidateInterop, compilation);
            if (returnCompatibility is TransformType.Invalid)
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDSG0005",
                                                                    "Partial match",
                                                                    "Skipping match for method {0}. Method {1} has an incompatible return type.",
                                                                    "GDal.SourceGenerator",
                                                                    DiagnosticSeverity.Warning,
                                                                    true),
                           wrapperMethod.GetLocation(),
                           wrapperMethod.ToDiagString(), candidateInterop.ToDiagString()));
                continue;
            }

            return new(candidateInterop, parameters, returnCompatibility);
        }

        return null;
    }

    private static TransformType CheckReturn(MethodDeclarationSyntax wrapperMethod, MethodDeclarationSyntax candidateInterop, Compilation compilation)
    {
        if (wrapperMethod.ReturnType is null || candidateInterop.ReturnType is null)
            return TransformType.Invalid;

        if (wrapperMethod.ReturnType is PredefinedTypeSyntax predefined && predefined.Keyword.IsKind(SyntaxKind.VoidKeyword))
            return TransformType.Void;

        if (compilation.GetSemanticModel(wrapperMethod.ReturnType.SyntaxTree, true).GetSymbolInfo(wrapperMethod.ReturnType).Symbol is not ITypeSymbol wrapperTypeSymbol)
            return TransformType.Invalid;

        if (compilation.GetSemanticModel(candidateInterop.ReturnType.SyntaxTree, true).GetSymbolInfo(candidateInterop.ReturnType).Symbol is not ITypeSymbol interopTypeSymbol)
            return TransformType.Invalid;

        if (wrapperTypeSymbol.Equals(interopTypeSymbol, SymbolEqualityComparer.Default))
            return TransformType.Direct;

        List<ITypeSymbol> hierarchy = [];

        hierarchy.Add(interopTypeSymbol);
        var parent = interopTypeSymbol.BaseType;
        while (parent is not null)
        {
            hierarchy.Add(parent);
            parent = parent.BaseType;
        }

        foreach (var handleType in wrapperTypeSymbol.Interfaces.Where(i => i.Name is "IConstructibleWrapper"))
        {
            if (hierarchy.Contains(handleType.TypeArguments[1]))
            {
                return TransformType.WrapperOut;
            }
        }

        return TransformType.Invalid;
    }

    private static ImmutableArray<ParameterCompatibility> IterateParameters(MethodDeclarationSyntax wrapperMethod, Compilation compilation, MethodDeclarationSyntax candidateInterop)
    {
        var parameters = ImmutableArray.CreateBuilder<ParameterCompatibility>(wrapperMethod.ParameterList.Parameters.Count);

        //walk through each parameter
        for (int i = 0; i < wrapperMethod.ParameterList.Parameters.Count; i++)
        {
            parameters.Add(CheckParameterCompatibility(wrapperMethod.ParameterList.Parameters[i], candidateInterop.ParameterList.Parameters[i], compilation));
        }

        return parameters.ToImmutable();
    }

    private static ParameterCompatibility CheckParameterCompatibility(ParameterSyntax wrapperParam, ParameterSyntax interopParam, Compilation compilation)
    {
        if (wrapperParam.Type is null || interopParam.Type is null)
            return new(TransformType.Invalid, interopParam, wrapperParam);

        if (compilation.GetSemanticModel(wrapperParam.Type.SyntaxTree, true).GetSymbolInfo(wrapperParam.Type).Symbol is not ITypeSymbol wrapperTypeSymbol)
            return new(TransformType.Invalid, interopParam, wrapperParam);

        if (compilation.GetSemanticModel(interopParam.Type.SyntaxTree, true).GetSymbolInfo(interopParam.Type).Symbol is not ITypeSymbol interopTypeSymbol)
            return new(TransformType.Invalid, interopParam, wrapperParam);

        if (wrapperTypeSymbol.Equals(interopTypeSymbol, SymbolEqualityComparer.Default))
        {
            return new(wrapperParam.Modifiers switch
            {
            [{ RawKind: (int)SyntaxKind.RefKeyword }] => TransformType.DirectRef,
            [{ RawKind: (int)SyntaxKind.OutKeyword }] => TransformType.DirectOut,
            [{ RawKind: (int)SyntaxKind.InKeyword }] => TransformType.DirectIn,
                _ => TransformType.Direct,
            }, interopParam, wrapperParam);
        }

        foreach (var handleType in wrapperTypeSymbol.Interfaces.Where(i => i.Name == "IHasHandle"))
        {
            List<ITypeSymbol> hierarchy = [];

            hierarchy.Add(interopTypeSymbol);
            var parent = interopTypeSymbol.BaseType;
            while (parent is not null)
            {
                hierarchy.Add(parent);
                parent = parent.BaseType;
            }

            if (hierarchy.Contains(handleType.TypeArguments[0]))
            {
                return wrapperParam.Modifiers switch
                {
                [{ RawKind: (int)SyntaxKind.RefKeyword }] => new(TransformType.WrapperRef, interopParam, wrapperParam),
                [{ RawKind: (int)SyntaxKind.OutKeyword }] => new(TransformType.WrapperOut, interopParam, wrapperParam),
                    _ => new(TransformType.WrapperIn, interopParam, wrapperParam)
                };

            }
        }

        return new(TransformType.Invalid, interopParam, wrapperParam);
    }

    private record class MethodTransformations(MethodDeclarationSyntax InteropMethod, ImmutableArray<ParameterCompatibility> Parameters, TransformType Return);

    private readonly record struct ParameterCompatibility(TransformType TransformType, ParameterSyntax InteropParam, ParameterSyntax WrapperParam);

    private enum TransformType
    {
        Invalid,
        Void,
        Direct,
        DirectOut,
        DirectIn,
        DirectRef,
        WrapperIn,
        WrapperRef,
        WrapperOut
    }
}
