// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using FluentAssertions.Execution;

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet.UnitTests;

public unsafe class DoubleNullTerminatedStringMarshalTests
{

    [Theory]
    [MemberData(nameof(TestStrings))]
    public unsafe void TestManagedToUnmanaged(string[] data)
    {
        byte** marshal = DoubleNullTerminatedStringMarshal.ConvertToUnmanagedCore(data);
        try
        {
            using AssertionScope _ = new();
            for (int i = 0; i < data.Length; i++)
            {
                string? currString = Utf8StringMarshaller.ConvertToManaged(marshal[i]);
                currString.Should().Be(data[i]);
            }
                ((nint)marshal[data.Length]).Should().Be(0);
        }
        finally
        {
            DoubleNullTerminatedStringMarshal.Free(marshal);
        }
    }

    [Fact]
    public unsafe void TestManagedToUnmanagedNull()
    {
        byte** marshal = DoubleNullTerminatedStringMarshal.ConvertToUnmanagedCore(null!);
        try
        {
            ((nint)marshal).Should().Be(0);
        }
        finally
        {
            DoubleNullTerminatedStringMarshal.Free(marshal);
        }
    }

    [Fact]
    public unsafe void TestManagedToUnmanagedEmpty()
    {
        var marshal = DoubleNullTerminatedStringMarshal.ConvertToUnmanagedCore(ImmutableList<string>.Empty);
        try
        {
            ((nint)marshal).Should().Be(0);
        }
        finally
        {
            DoubleNullTerminatedStringMarshal.Free(marshal);
        }
    }

    /*[Theory]
    [MemberData(nameof(TestStrings))]
    public void TestUnmanagedToManaged(string[] data)
    {
        throw new NotImplementedException();

    }*/
#warning TODO test vs. cpl_string.h methods

    [Fact]
    public void TestUnmanagedToManagedNull()
    {
        string[]? result = DoubleNullTerminatedStringMarshal.ConvertToManaged(null)?.ToArray();
        result.Should().BeNull();
    }

    [Fact]
    public unsafe void TestUnmanagedToManagedEmpty()
    {
        byte** single = (byte**)NativeMemory.Alloc(1);
        single[0] = null;

        string[]? result = DoubleNullTerminatedStringMarshal.ConvertToManaged(single)?.ToArray();
        result.Should().BeEmpty();
    }

    public static IEnumerable<object[]> TestStrings =>

        new List<object[]>() { 
            new object[] { TestData.AsciiOnly },
            new object[] { TestData.MixedUnicode }
        };

    public static class TestData
    {
        public static string[] AsciiOnly => new string[]
            {
                "This is a test",
                "Each member of the array is null-terminated",
                "The entire array ends with two nulls in a row"
            };

        public static string[] MixedUnicode => new string[]
            {
                "This is a test with some Ùnicode characters",
                "This should perform the same, but some characters will take up multiple bytes",
                "CKJ: 乪 乫 Emoji:😄 BOM: \uFEFF"
            };
    }
}