// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.InteropAttributes;
using MMKiwi.GdalNet.InteropSourceGen;

namespace MMKiwi.GdalNet.UnitTests.SourceGenerators;

[UsesVerify]
public class HandleGeneratorTest
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
    public Task TestErrorNotPartial()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using MMKiwi.GdalNet.Handles;
            
            namespace Test;

            [GdalGenerateHandle]
            internal sealed class TestHandleNeverOwns : GdalInternalHandleNeverOwns, IConstructableHandle<TestHandleNeverOwns>
            {
            }
            """);

        var driver = GeneratorDriver([source]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestWarnNotSealedOrAbstract()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using MMKiwi.GdalNet.Handles;

            namespace Test;

            [GdalGenerateHandle]
            internal partial class TestHandleNeverOwns : GdalInternalHandleNeverOwns, IConstructableHandle<TestHandleNeverOwns>
            {
            }
            """);

        var driver = GeneratorDriver([source]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestWarnNoBase()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using MMKiwi.GdalNet.Handles;

            namespace Test;

            [GdalGenerateHandle]
            internal partial class TestHandleNeverOwns : SafeHandle, IConstructableHandle<TestHandleNeverOwns>
            {
            }
            """);

        var driver = GeneratorDriver([source]);

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
            using MMKiwi.GdalNet.Handles;

            namespace Test;

            [GdalGenerateHandle]
            internal sealed partial class TestHandleNeverOwns : GdalInternalHandleNeverOwns, IConstructableHandle<TestHandleNeverOwns>
            {
            }
            """);

        var driver = GeneratorDriver([source]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Fact]
    public Task TestHasConstructor()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using MMKiwi.GdalNet.Handles;

            namespace Test;

            [GdalGenerateHandle]
            internal sealed partial class TestHandle : GdalInternalHandle, IConstructableHandle<TestHandle>
            {
                public TestHandle(bool ownsHandle) : base(ownsHandle)
                {
                    Console.WriteLine("test");
                }
            }
            """);

        var driver = GeneratorDriver([source]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots");
    }
    
    [Theory]
    [ClassData(typeof(MemberVisibilities))]
    public Task TestConstructorVisibility(MemberVisibility memberVisibility)
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            $$"""
              using MMKiwi.GdalNet.InteropAttributes;
              using MMKiwi.GdalNet;
              using MMKiwi.GdalNet.Handles;

              namespace Test;

              [GdalGenerateHandle(ConstructorVisibility = MemberVisibility.{{memberVisibility}}) ]
              internal sealed partial class TestHandle : GdalInternalHandle, IConstructableHandle<TestHandle>
              {
              }
              """);

        var driver = GeneratorDriver([source]);

        var runResult = driver.GetRunResult().Results.Single();
        return Verify(runResult).UseDirectory("snapshots").UseParameters(memberVisibility);
    }
    
    [Fact]
    public Task TestInternalHandle()
    {
        SyntaxTree source = CSharpSyntaxTree.ParseText(
            """
            using MMKiwi.GdalNet.InteropAttributes;
            using MMKiwi.GdalNet;
            using MMKiwi.GdalNet.Handles;

            namespace Test;

            [GdalGenerateHandle]
            internal abstract partial class TestHandle : GdalInternalHandle
            {
                public class Owns(): TestHandle(true); 
                public class DoesntOwn(): TestHandle(false); 
            }
            """);

        var driver = GeneratorDriver([source]);

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
            MetadataReference.CreateFromFile(typeof(GdalInternalHandleNeverOwns).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IConstructableWrapper<,>).Assembly.Location)
        ];

        var compilation = CSharpCompilation.Create(InternalUnitTestConst.AssemblyName, syntaxTrees: trees, references: references);
        var generator = new HandleGenerator();

        var driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation);
    }
}
