using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace MMKiwi.GdalNet.Analyzers.Internal;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class GdalMissingErrorCheckAnalyzer : DiagnosticAnalyzer
{
    private const string MarkerAttribute = "MMKiwi.GdalNet.Error.GdalEnforceErrorHandlingAttribute";

    // Preferred format of DiagnosticId is Your Prefix + Number, e.g. CA1234.
    private const string DiagnosticId1 = "GDAL0001";
    private const string DiagnosticId2 = "GDAL0002";

    // Feel free to use raw strings if you don't need localization.
    private static LocalizableString Title1 => "Missing call to ThrowIfError()";
    private static LocalizableString Title2 => "Not handling returned GdalCplError";

    // The message that will be displayed to the user.
    private static LocalizableString MessageFormat1 => "Missing call to GdalError.ThrowIfError() after method {0}";
    private static LocalizableString MessageFormat2 => "Return value GdalCplErr from method {0} is not handled";

    private static LocalizableString Description1 => "Missing GdalThrowIfError call";
    private static LocalizableString Description2 => "Not handling returned GdalCplError";

    // The category of the diagnostic (Design, Naming etc.).
    private const string Category = "Reliability";

    private static readonly DiagnosticDescriptor s_missingThrowIfError
        = new(DiagnosticId1, Title1, MessageFormat1, Category,
            DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description1);


    private static readonly DiagnosticDescriptor s_notHandlingReturnType
        = new(DiagnosticId2, Title2, MessageFormat2, Category,
            DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description2);

    public static DiagnosticDescriptor MissingThrowIfError => s_missingThrowIfError;
    public static DiagnosticDescriptor NotHandlingReturnType => s_notHandlingReturnType;


    // Keep in mind: you have to list your rules here.
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
        = [s_missingThrowIfError, s_notHandlingReturnType];

    public override void Initialize(AnalysisContext context)
    {
        // You must call this method to avoid analyzing generated code.
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        // You must call this method to enable the Concurrent Execution.
        context.EnableConcurrentExecution();

        // Subscribe to semantic (compile time) action invocation, e.g. method invocation.
        context.RegisterOperationAction(AnalyzeOperation, OperationKind.Invocation);
    }

    /// <summary>
    /// Executed on the completion of the semantic analysis associated with the Invocation operation.
    /// </summary>
    /// <param name="context">Operation context.</param>
    private void AnalyzeOperation(OperationAnalysisContext context)
    {
        // The Roslyn architecture is based on inheritance.
        // To get the required metadata, we should match the 'Operation' and 'Syntax' objects to the particular types,
        // which are based on the 'OperationKind' parameter specified in the 'Register...' method.
        if (context.Operation is not IInvocationOperation invocationOperation ||
            context.Operation.Syntax is not InvocationExpressionSyntax invocationSyntax)
            return;

        var methodSymbol = invocationOperation.TargetMethod;

        if (methodSymbol.MethodKind != MethodKind.Ordinary)
            return;

        if (!CheckForAttribute(methodSymbol))
            return;

        BlockSyntax? sblock = GetParentOfType<BlockSyntax>(invocationSyntax);

        if (methodSymbol.ReturnType.ToDisplayString() is "MMKiwi.GdalNet.Interop.GdalCplErr" or "MMKiwi.GdalNet.Interop.OgrError")
        {
            if (CheckForExtensionMethod(invocationSyntax, context.Operation.SemanticModel))
                return;
            else
            {
                var diagnosticExt = Diagnostic.Create(s_notHandlingReturnType,
                    // The highlighted area in the analyzed source code. Keep it as specific as possible.
                    invocationSyntax.GetLocation(),
                    // The value is passed to the 'MessageFormat' argument of your rule.
                    methodSymbol.MetadataName);

                // Reporting a diagnostic is the primary outcome of analyzers.
                context.ReportDiagnostic(diagnosticExt);
            }
        }


        if (CheckInvoke(sblock, invocationSyntax, context.Operation.SemanticModel))
            return;

        var diagnostic = Diagnostic.Create(s_missingThrowIfError,
            // The highlighted area in the analyzed source code. Keep it as specific as possible.
            invocationSyntax.GetLocation(),
            // The value is passed to the 'MessageFormat' argument of your rule.
            methodSymbol.MetadataName);

        // Reporting a diagnostic is the primary outcome of analyzers.
        context.ReportDiagnostic(diagnostic);
    }

    private bool CheckForExtensionMethod(InvocationExpressionSyntax invocationSyntax, SemanticModel? model)
    {
        if (model == null)
            return false;
        
        // Check to see if this is a child of another invocation
        if (GetParentOfType<InvocationExpressionSyntax>(invocationSyntax) is not { } parentInvocation)
            return false;

        if (model.GetSymbolInfo(parentInvocation).Symbol is not IMethodSymbol parentMethod)
            return false;

        string methodName = parentMethod.ToDisplayString();

        return methodName == "MMKiwi.GdalNet.Interop.GdalCplErr.ThrowIfError()" || 
               methodName == "MMKiwi.GdalNet.Interop.OgrError.ThrowIfError()";
    }

    private static bool CheckInvoke(BlockSyntax? block, InvocationExpressionSyntax invocation, SemanticModel? model)
    {
        if (GetParentOfType<StatementSyntax>(invocation) is not { } parentStatement || block is null)
            return false;

        ImmutableArray<StatementSyntax> blockChildren = block.Statements.ToImmutableArray();

        int invokeIndex = blockChildren.IndexOf(parentStatement);

        if (blockChildren.Length <= (invokeIndex + 1))
            return false;

        StatementSyntax nextChild = blockChildren[invokeIndex + 1];
        var nextInvocation = nextChild.DescendantNodes().OfType<InvocationExpressionSyntax>().FirstOrDefault();
        if (nextInvocation is null || model?.GetSymbolInfo(nextInvocation).Symbol is not IMethodSymbol parentMethod)
            return false;

        return parentMethod.ToDisplayString() == "MMKiwi.GdalNet.Error.GdalError.ThrowIfError()";
    }
    private static bool CheckForAttribute(IMethodSymbol methodSymbol)
    {
        bool? method = CheckForAttribute(methodSymbol.GetAttributes());
        switch (method)
        {
            case true:
                return true;
            case false:
                return false;
        }

        var @class = CheckForAttribute(methodSymbol.ContainingType.GetAttributes());
        return @class switch
        {
            true => true,
            false => false,
            null => false
        };

    }

    private static T? GetParentOfType<T>(SyntaxNode node)
        where T : SyntaxNode
    {
        SyntaxNode? current = node.Parent;
        while (current is not null)
        {
            if (current is T parentType)
                return parentType;
            current = current.Parent;
        }
        return null;
    }

    private static bool? CheckForAttribute(ImmutableArray<AttributeData> attributeList)
    {
        var attribute = attributeList.FirstOrDefault(att => att.AttributeClass?.ToDisplayString() == MarkerAttribute);
        return attribute is not null ? (attribute.ConstructorArguments.Length == 0 || attribute.ConstructorArguments[0].Value is true) : null;
    }
}