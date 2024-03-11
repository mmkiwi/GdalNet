using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

using Xunit;

using Verifier =
    Microsoft.CodeAnalysis.CSharp.Testing.XUnit.AnalyzerVerifier<MMKiwi.GdalNet.Analyzers.Internal.GdalMissingErrorCheckAnalyzer>;

namespace MMKiwi.GdalNet.Analyzers.Internal.Tests;

public class GdalMissingErrorCheckAnalyzerTests
{
    const string attribute = @"
namespace MMKiwi.GdalNet.Error
{
    using MMKiwi.GdalNet.Interop;
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
    public class GdalEnforceErrorHandlingAttribute : System.Attribute
    {
        public GdalEnforceErrorHandlingAttribute(bool handleErrors = true)
        {
            HandleErrors = handleErrors;
        }
        public bool HandleErrors { get; set; }
    }

    public static class GdalError
    {
        public static void ThrowIfError() {}

        public static void ThrowIfError(this GdalCplErr error) { }
        public static void ThrowIfError(this OgrError error) { }
    }

}

namespace MMKiwi.GdalNet.Interop
{
    public enum GdalCplErr {}
    public enum OgrError {}
}

";

    [Theory]
    [MemberData(nameof(GenerateCases))]
    public async Task TestAttributeShouldWarn(RunInfo info)
    {
        bool shouldDiagnose = GetShouldDiagnose(info);

        string text = $$"""
                        using MMKiwi.GdalNet.Error;
                        namespace Test
                        {
                        
                            {{info.ClassAttribute}}
                            public static class TestInteropClass
                            {
                                {{info.MethodAttribute}}
                                public static void TestMethod() { }
                            }
                            
                            public class TestCallingClass
                            {
                                public void TestCallingMethodNoError()
                                {
                                    TestInteropClass.TestMethod();
                                }
                            }
                            
                        }
                        """;

        var expected = Verifier.Diagnostic(GdalMissingErrorCheckAnalyzer.MissingThrowIfError)
            .WithMessage("Missing call to GdalError.ThrowIfError() after method TestMethod")
            .WithSpan("/0/Test1.cs", 16, 13, 16, 42)
            .WithArguments("TestMethod");
        var analyzer = new CSharpAnalyzerTest<GdalMissingErrorCheckAnalyzer, XUnitVerifier>
        {
            TestState =
            {
                Sources =
                {
                    attribute,  text
                }
            },
        };
        if (shouldDiagnose)
            analyzer.ExpectedDiagnostics.Add(expected);
        await analyzer.RunAsync();
    }

    [Theory]
    [MemberData(nameof(GenerateCases))]
    public async Task TestAttributeShouldWarnTwiceReturn(RunInfo info)
    {
        bool shouldDiagnose = GetShouldDiagnose(info);

        string text = $$"""
                        using MMKiwi.GdalNet.Error;
                        using MMKiwi.GdalNet.Interop;
                        namespace Test
                        {
                            {{info.ClassAttribute}}
                            public static class TestInteropClass
                            {
                                {{info.MethodAttribute}}
                                public static GdalCplErr TestMethod() => default;
                            }
                            
                            public class TestCallingClass
                            {
                                public void TestCallingMethodNoError()
                                {
                                    var x = TestInteropClass.TestMethod();
                                }
                            }
                        }
                        """;

        var expected = Verifier.Diagnostic(GdalMissingErrorCheckAnalyzer.MissingThrowIfError)
            .WithMessage("Missing call to GdalError.ThrowIfError() after method TestMethod")
            .WithSpan("/0/Test1.cs", 16, 21, 16, 50)
            .WithArguments("TestMethod");
        var expected2 = Verifier.Diagnostic(GdalMissingErrorCheckAnalyzer.NotHandlingReturnType)
            .WithMessage("Return value GdalCplErr from method TestMethod is not handled")
            .WithSpan("/0/Test1.cs", 16, 21, 16, 50)
            .WithArguments("TestMethod");

        var analyzer = new CSharpAnalyzerTest<GdalMissingErrorCheckAnalyzer, XUnitVerifier>
        {
            TestState =
            {
                Sources =
                {
                    attribute,  text
                }
            },
        };
        if (shouldDiagnose)
        {
            analyzer.ExpectedDiagnostics.Add(expected2);
            analyzer.ExpectedDiagnostics.Add(expected);
        }

        await analyzer.RunAsync();
    }

    [Theory]
    [MemberData(nameof(GenerateCases))]
    public async Task TestAttributeShouldWarnReturn(RunInfo info)
    {
        bool shouldDiagnose = GetShouldDiagnose(info);

        string text = $$"""
                        using MMKiwi.GdalNet.Error;
                        using MMKiwi.GdalNet.Interop;
                        namespace Test
                        {
                            {{info.ClassAttribute}}
                            public static class TestInteropClass
                            {
                                {{info.MethodAttribute}}
                                public static GdalCplErr TestMethod() => default;
                            }
                            
                            public class TestCallingClass
                            {
                                public void TestCallingMethodNoError()
                                {
                                    TestInteropClass.TestMethod();
                                    GdalError.ThrowIfError();
                                }
                            }
                        }
                        """;

        var expected = Verifier.Diagnostic(GdalMissingErrorCheckAnalyzer.NotHandlingReturnType)
            .WithMessage("Return value GdalCplErr from method TestMethod is not handled")
            .WithSpan("/0/Test1.cs", 16, 13, 16, 42)
            .WithArguments("TestMethod");

        var analyzer = new CSharpAnalyzerTest<GdalMissingErrorCheckAnalyzer, XUnitVerifier>
        {
            TestState =
            {
                Sources =
                {
                    attribute,  text
                }
            },
        };
        if (shouldDiagnose)
        {
            analyzer.ExpectedDiagnostics.Add(expected);
        }

        await analyzer.RunAsync();
    }

    [Theory]
    [MemberData(nameof(GenerateCases))]
    public async Task TestAttributeShouldNotWarnReturn(RunInfo info)
    {
        string text = $$"""
                        using MMKiwi.GdalNet.Error;
                        using MMKiwi.GdalNet.Interop;
                        namespace Test
                        {
                        
                            {{info.ClassAttribute}}
                            public static class TestInteropClass
                            {
                                {{info.MethodAttribute}}
                                public static GdalCplErr TestMethod() => default;
                                
                                {{info.MethodAttribute}}
                                public static OgrError TestOgrMethod() => default;
                            }
                            
                            public class TestCallingClass
                            {
                                public void TestCallingMethodHasThrow()
                                {
                                    TestInteropClass.TestMethod().ThrowIfError();
                                    TestInteropClass.TestOgrMethod().ThrowIfError();
                                }
                            }
                        }
                        """;

        var analyzer = new CSharpAnalyzerTest<GdalMissingErrorCheckAnalyzer, XUnitVerifier>
        {
            TestState =
            {
                Sources =
                {
                    attribute, text
                }
            },
        };

        await analyzer.RunAsync();
    }


    [Theory]
    [MemberData(nameof(GenerateCases))]
    public async Task TestAttributeShouldNotWarnAssignment(RunInfo info)
    {
        string text = $$"""
                        using MMKiwi.GdalNet.Error;
                        namespace Test
                        {
                        
                            {{info.ClassAttribute}}
                            public static class TestInteropClass
                            {
                                {{info.MethodAttribute}}
                                public static int TestMethod() => default;
                            }
                            
                            public class TestCallingClass
                            {
                                public void TestCallingMethodHasThrow()
                                {
                                    var x = TestInteropClass.TestMethod();
                                    GdalError.ThrowIfError();
                                }
                            }
                        }
                        """;

        var analyzer = new CSharpAnalyzerTest<GdalMissingErrorCheckAnalyzer, XUnitVerifier>
        {
            TestState =
            {
                Sources =
                {
                    attribute, text
                }
            },
        };

        await analyzer.RunAsync();
    }

    private static bool GetShouldDiagnose(RunInfo info)
    {
        return (info.Class, info.Method) switch
        {
            // If method has attribute, then go with that
            (_, AttributeType.Implicit or AttributeType.True) => true,
            (_, AttributeType.False) => false,

            // If method doesnt have attribute, but class does, go with that
            (AttributeType.Implicit or AttributeType.True, AttributeType.None) => true,
            (AttributeType.False or AttributeType.None, AttributeType.None) => false,

            // Shouldn't hit this since we've covered every scenario above
            _ => throw new NotImplementedException()
        };
    }

    public static IEnumerable<object[]> GenerateCases()
    {
#if FALSE
        // If we need to debug one case, set the above to TRUE 
        return [[new RunInfo(AttributeType.False, AttributeType.Implicit)]];
#else
        ImmutableArray<AttributeType> types =
        [
            AttributeType.None,
            AttributeType.Implicit,
            AttributeType.True,
            AttributeType.False
        ];
        foreach (var @class in types)
        foreach (var method in types)
            yield return [new RunInfo(@class, method)];
#endif
    }

    public record struct RunInfo(AttributeType Class, AttributeType Method)
    {
        public string ClassAttribute => Class switch
        {

            AttributeType.None => "",
            AttributeType.Implicit => "[GdalEnforceErrorHandling]",
            AttributeType.True => "[GdalEnforceErrorHandling(true)]",
            AttributeType.False => "[GdalEnforceErrorHandling(false)]",
            _ => throw new ArgumentOutOfRangeException()
        };

        public string MethodAttribute => Method switch
        {

            AttributeType.None => "",
            AttributeType.Implicit => "[GdalEnforceErrorHandling]",
            AttributeType.True => "[GdalEnforceErrorHandling(true)]",
            AttributeType.False => "[GdalEnforceErrorHandling(false)]",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}