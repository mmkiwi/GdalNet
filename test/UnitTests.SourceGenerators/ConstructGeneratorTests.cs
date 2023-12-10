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
public class ConstructGeneratorTest
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

    [Fact]
    public Task TestInternalHandleHasConstructor()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;

            namespace Test;

            [GdalGenerateWrapper]
            public partial class TestWrapper : IConstructableWrapper<TestWrapper, TestHandleNeverOwns>, IHasHandle<TestHandleNeverOwns>
            {
                public TestWrapper(TestHandleNeverOwns handle) {}
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandleNeverOwns()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestInternalHandleHasImplicitHandle()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;

            namespace Test;

            [GdalGenerateWrapper]
            public partial class TestWrapper : IConstructableWrapper<TestWrapper, TestHandleNeverOwns>, IHasHandle<TestHandleNeverOwns>
            {
                public TestHandleNeverOwns Handle => null!;
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandleNeverOwns()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestInternalHandleHasEverything()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;

            namespace Test;

            [GdalGenerateWrapper]
            public partial class TestWrapper : IConstructableWrapper<TestWrapper, TestHandleNeverOwns>, IHasHandle<TestHandleNeverOwns>
            {
                public TestHandleNeverOwns Handle => null!;
                public TestWrapper(TestHandleNeverOwns handle) {}
                static TestWrapper IConstructableWrapper<TestWrapper, TestHandleNeverOwns>.Construct(TestHandleNeverOwns handle) => new(handle);
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandleNeverOwns()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }

    [Fact]
    public Task TestInternalHandleNeverOwns()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;

            namespace Test;

            [GdalGenerateWrapper]
            public partial class TestWrapper : IConstructableWrapper<TestWrapper, TestHandleNeverOwns>, IHasHandle<TestHandleNeverOwns>
            {
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandleNeverOwns()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }

    private static SyntaxTree GetInternalHandleNeverOwns()
        => CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;

            namespace Test;

            [GdalGenerateHandle]
            internal sealed partial class TestHandleNeverOwns : GdalInternalHandleNeverOwns, IConstructableHandle<TestHandleNeverOwns>
            {
                static TestHandle IConstructableHandle<TestHandleNeverOwns>.Construct(bool ownsHandle) => new();
            }
            """);
    
    private static SyntaxTree GetInternalHandle()
        => CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;

            namespace Test;

            [GdalGenerateHandle]
            internal abstract partial class TestHandle : GdalInternalHandle, IConstructableHandle<TestHandle>
            {
                protected override GdalCplErr? ReleaseHandleCore() => null;
                public sealed class Owns() : TestHandle(true);
                public sealed class DoesntOwn() : TestHandle(true);
                
                static TestHandle IConstructableHandle<TestHandle>.Construct(bool ownsHandle) => ownsHandle ? new Owns() : new DoesntOwn();
                
                protected TestHandle(bool ownsHandle): base(ownsHandle) { }
            }
            """);

    [Fact]
    public Task TestInternalHandleDoesNotImplementIDisposable()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System;

            namespace Test;

            [GdalGenerateWrapper]
            public partial class TestWrapper : IConstructableWrapper<TestWrapper, TestHandle>, IHasHandle<TestHandle>
            {
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandle()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestParentImplementsIDisposable()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System;

            namespace Test;
            
            public class ParentClass : IDisposable
            {
                public void Dispose() {}
            } 
            
            [GdalGenerateWrapper]
            public partial class TestWrapper : ParentClass, IConstructableWrapper<TestWrapper, TestHandle>, IHasHandle<TestHandle>
            {
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandle()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestParentDoesNotImplementIDisposable()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System;

            namespace Test;

            public class ParentClass;

            [GdalGenerateWrapper]
            public partial class TestWrapper : ParentClass, IConstructableWrapper<TestWrapper, TestHandle>, IHasHandle<TestHandle>
            {
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandle()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }


    [Fact]
    public Task TestInternalHandle()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System;

            namespace Test;

            [GdalGenerateWrapper]
            public partial class TestWrapper : IConstructableWrapper<TestWrapper, TestHandle>, IHasHandle<TestHandle>, IDisposable
            {
                public bool Dispose()
                {
                    Handle.Dispose();
                }
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandle()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }

    public static IEnumerable<object[]> MemberVisibilities => MemberVisibilityExtensions.GetValues().Select(m=>new object[]{m});
    
    [Theory]
    [MemberData(nameof(MemberVisibilities))]
    public Task TestConstructorVisibility(MemberVisibility memberVisibility)
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            $$"""
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System;

            namespace Test;

            [GdalGenerateWrapper(ConstructorVisibility = MemberVisibility.{{memberVisibility}})]
            public partial class TestWrapper : IConstructableWrapper<TestWrapper, TestHandle>, IHasHandle<TestHandle>, IDisposable
            {
                public bool Dispose()
                {
                    Handle.Dispose();
                }
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandle()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots").UseParameters(memberVisibility);
    }
    
    [Theory]
    [MemberData(nameof(MemberVisibilities))]
    public Task TestHandleVisibility(MemberVisibility memberVisibility)
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            $$"""
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System;
            namespace Test;

            [GdalGenerateWrapper(HandleVisibility = MemberVisibility.{{memberVisibility}})]
            public partial class TestWrapper : IConstructableWrapper<TestWrapper, TestHandle>, IHasHandle<TestHandle>, IDisposable
            {
                public void Dispose()
                {
                    Handle.Dispose();
                }
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandle()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots").UseParameters(memberVisibility);
    }

    [Fact]
    public Task TestErrorNotPartial()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System;
            namespace Test;

            [GdalGenerateWrapper]
            public class TestWrapper : IConstructableWrapper<TestWrapper, TestHandleNeverOwns>, IHasHandle<TestHandleNeverOwns>
            {
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandleNeverOwns()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    
    [Fact]
    public Task TestErrorDoesNotImplement()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System;
            namespace Test;

            [GdalGenerateWrapper]
            public partial class TestWrapper
            {
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandle()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Theory]
    [MemberData(nameof(MemberVisibilities))]
    public Task TestHandleSetVisibility(MemberVisibility memberVisibility)
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            $$"""
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using System;
            namespace Test;

            [GdalGenerateWrapper(HandleSetVisibility = MemberVisibility.{{memberVisibility}})]
            public partial class TestWrapper : IConstructableWrapper<TestWrapper, TestHandle>, IHasHandle<TestHandle>, IDisposable
            {
                public void Dispose()
                {
                    Handle.Dispose();
                }
            }
            """);

        var driver = GeneratorDriver([source, GetInternalHandle()]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots").UseParameters(memberVisibility);
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
        var generator = new ConstructGenerator();


        var driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation);
    }
}