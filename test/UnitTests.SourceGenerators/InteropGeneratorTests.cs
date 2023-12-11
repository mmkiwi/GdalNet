// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using MMKiwi.GdalNet.InteropAttributes;
using MMKiwi.GdalNet.InteropSourceGen;

namespace MMKiwi.GdalNet.UnitTests.SourceGenerators;

[UsesVerify]
public class InteropGeneratorTest
{
    [Fact]
    public Task Driver()
    {
        var driver = GeneratorDriver();

        return Verify(driver);
    }

    [Fact]
    public Task RunResults()
    {
        var driver = GeneratorDriver();

        var runResults = driver.GetRunResult();
        return Verify(runResults);
    }

    private SyntaxTree GetClasses()
        => CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System;

            namespace Test;

            public class TestWrapper : IHasHandle<TestHandle>, IConstructableWrapper<TestWrapper, TestHandle>
            {
            }
            
            public class TestHandle : IConstructableHandle<TestHandle>
            {
            }
            
            public class DummyAttribute : Attribute {}
            """);
    
    [Fact]
    public Task TestDefaults()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;
            
            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(TestHandle dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestMethodName()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethodOtherName(TestHandle dataset);
                
                [GdalWrapperMethod(MethodName = "TestCMethodOtherName"]
                public static partial int TestCMethod(TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestMethodNameIncompatible()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;
            public partial static class Parent
            {
                public partial static class Interop
                {
                    [LibraryImport("TESTLIBRARY")]
                    private static partial int TestCMethodOtherName();
                    
                    [GdalWrapperMethod(MethodName = "TestCMethodOtherName"]
                    public static partial int TestCMethod(TestWrapper dataset);
                }
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestHandleOut()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(out TestHandle dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(out TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestHandleRef()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(ref TestHandle dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(ref TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestHandleIn()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(in TestHandle dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(in TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestScalarOut()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(out int dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(out int dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestScalarRef()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(ref int dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(ref int dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }

    [Fact]
    public Task TestHandleReturn()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial TestHandle TestCMethod();
                
                [GdalWrapperMethod]
                public static partial TestWrapper TestCMethod();
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestParamHasAttributes()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int OtherMethod([Dummy] int dataset);
                
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod([Dummy] int dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod([Dummy] int dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestScalarDirect()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(int dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(int dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestScalarIn()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(in int dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(in int dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestAdditionalAttributes()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [Dummy]
                [LibraryImport("TESTLIBRARY")]
                [Dummy]
                private static partial int TestCMethod(TestHandle dataset);
                
                [Dummy]
                [GdalWrapperMethod]
                [Dummy]
                public static partial int TestCMethod(TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestWarnNoLibraryImport()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [Dummy]
                private static partial int TestCMethod(TestHandle dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }

    [Fact]
    public Task TestWarnNoClass()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            [Dummy]
            private static partial int TestCMethod(TestHandle dataset);
            
            [GdalWrapperMethod]
            public static partial int TestCMethod(TestWrapper dataset);
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestWarnNoPartial()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(TestHandle dataset);
                
                [GdalWrapperMethod]
                public static int TestCMethod(TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestWarnNoInterop()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [GdalWrapperMethod]
                public static partial int TestCMethod(TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    
    [Fact]
    public Task TestWarnInvalidReturn()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial bool TestCMethod(TestHandle dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestMultipleCandidates()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System.Runtime.InteropServices;

            namespace Test;

            public partial static class Interop
            {
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(TestHandle dataset, object null);
                
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(object null);
            
                [LibraryImport("TESTLIBRARY")]
                private static partial int TestCMethod(TestHandle dataset);
                
                [GdalWrapperMethod]
                public static partial int TestCMethod(TestWrapper dataset);
            }
            """);

        var driver = GeneratorDriver([source, GetClasses()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }

    
    static GeneratorDriver GeneratorDriver(IEnumerable<SyntaxTree>? trees = null)
    {
        string dotNetAssemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location)!;
        
        IEnumerable<PortableExecutableReference> references =
        [

            MetadataReference.CreateFromFile(Path.Combine(dotNetAssemblyPath, "mscorlib.dll")),
            MetadataReference.CreateFromFile(Path.Combine(dotNetAssemblyPath, "System.dll")),
            MetadataReference.CreateFromFile(Path.Combine(dotNetAssemblyPath, "System.Core.dll")),
            MetadataReference.CreateFromFile(Path.Combine(dotNetAssemblyPath, "System.Private.CoreLib.dll")),
            MetadataReference.CreateFromFile(Path.Combine(dotNetAssemblyPath, "System.Runtime.dll")),
            MetadataReference.CreateFromFile(typeof(GdalWrapperMethodAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(GdalInternalHandleNeverOwns).Assembly.Location)
        ];

        var compilation = CSharpCompilation.Create(InternalUnitTestConst.AssemblyName, syntaxTrees: trees, references: references);
        var generator = new InteropGenerator();


        var driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation);
    }
}