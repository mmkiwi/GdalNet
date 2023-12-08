﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using FluentAssertions.Execution;

using MMKiwi.GdalNet.CHelpers;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet.UnitTests;

public unsafe class CStringArrayMarshalTests
{

    [Theory]
    [MemberData(nameof(TestStrings))]
    public unsafe void TestManagedToUnmanaged(string[] data)
    {
        byte** marshal = CStringArrayMarshal.ConvertToUnmanaged(data);
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
            CStringArrayMarshal.Free(marshal);
        }
    }

    [Theory]
    [MemberData(nameof(TestDictionaries))]
    public unsafe void TestManagedToUnmanagedDictionary(Dictionary<string, string> data)
    {
        byte** marshal = CStringArrayMarshal.DictionaryMarshal.ConvertToUnmanaged(data);
        try
        {
            using AssertionScope _ = new();
            for (int i = 0; i < data.Count; i++)
            {
                string? currString = Utf8StringMarshaller.ConvertToManaged(marshal[i]);
                currString.Should().NotBeNull();
                int eqIndex = currString!.IndexOf('=');
                string key = currString[..eqIndex];
                string value = currString[(eqIndex + 1)..];
                data.TryGetValue(key, out string? dictValue).Should().BeTrue($"{key} should be a key");
                dictValue.Should().Be(value);
            }
                ((nint)marshal[data.Count]).Should().Be(0);
        }
        finally
        {
            CStringArrayMarshal.Free(marshal);
        }
    }

    [Theory]
    [MemberData(nameof(TestStrings))]
    public unsafe void TestManagedToUnmanagedEnumerable(string[] data)
    {
        byte** marshal = CStringArrayMarshal.EnumerableMarshal.ConvertToUnmanaged(data);
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
            CStringArrayMarshal.Free(marshal);
        }
    }

    [Fact]
    public unsafe void TestManagedToUnmanagedNull()
    {
        byte** marshal = CStringArrayMarshal.ConvertToUnmanaged(null!);
        try
        {
            ((nint)marshal).Should().Be(0);
        }
        finally
        {
            CStringArrayMarshal.Free(marshal);
        }
    }

    [Fact]
    public unsafe void TestManagedToUnmanagedNullDictionary()
    {
        byte** marshal = CStringArrayMarshal.DictionaryMarshal.ConvertToUnmanaged(null!);
        try
        {
            ((nint)marshal).Should().Be(0);
        }
        finally
        {
            CStringArrayMarshal.Free(marshal);
        }
    }

    [Fact]
    public unsafe void TestManagedToUnmanagedNullEnumerable()
    {
        byte** marshal = CStringArrayMarshal.EnumerableMarshal.ConvertToUnmanaged(null!);
        try
        {
            ((nint)marshal).Should().Be(0);
        }
        finally
        {
            CStringArrayMarshal.Free(marshal);
        }
    }

    [Fact]
    public unsafe void TestManagedToUnmanagedEmpty()
    {
        var marshal = CStringArrayMarshal.ConvertToUnmanaged([]);
        try
        {
            ((nint)marshal).Should().Be(0);
        }
        finally
        {
            CStringArrayMarshal.Free(marshal);
        }
    }

    [Fact]
    public unsafe void TestManagedToUnmanagedEmptyDictionary()
    {
        var marshal = CStringArrayMarshal.DictionaryMarshal.ConvertToUnmanaged(new Dictionary<string,string>());
        try
        {
            ((nint)marshal).Should().Be(0);
        }
        finally
        {
            CStringArrayMarshal.Free(marshal);
        }
    }

    [Fact]
    public unsafe void TestManagedToUnmanagedEmptyEnumerable()
    {
        var marshal = CStringArrayMarshal.EnumerableMarshal.ConvertToUnmanaged(Array.Empty<string>());
        try
        {
            ((nint)marshal).Should().Be(0);
        }
        finally
        {
            CStringArrayMarshal.Free(marshal);
        }
    }

    [Theory]
    [MemberData(nameof(TestStrings))]
    public void TestUnmanagedToManaged(string[] data)
    {
        using CStringList csl = CStringList.Create(data[0]);
        for (int i = 1; i < data.Length; i++)
        {
            csl.AddString(data[i]);
        }

        string[]? result = CStringArrayMarshal.ConvertToManaged((byte**)csl.Handle.DangerousGetHandle());

        result.Should().BeEquivalentTo(data);

    }

    [Theory]
    [MemberData(nameof(TestDictionaries))]
    public void TestUnmanagedToManagedDictionary(Dictionary<string,string> data)
    {
        string[] dataArray = data.Select(kvp => $"{kvp.Key}={kvp.Value}").ToArray();
        using CStringList csl = CStringList.Create(dataArray[0]);
        for (int i = 1; i < dataArray.Length; i++)
        {
            csl.AddString(dataArray[i]);
        }

        string[]? result = CStringArrayMarshal.ConvertToManaged((byte**)csl.Handle.DangerousGetHandle());

        result.Should().BeEquivalentTo(dataArray);

    }

    [Theory]
    [MemberData(nameof(TestStrings))]
    public void TestUnmanagedToManagedEnumerable(string[] data)
    {
        using CStringList csl = CStringList.Create(data[0]);
        for (int i = 1; i < data.Length; i++)
        {
            csl.AddString(data[i]);
        }

        IEnumerable<string>? result = CStringArrayMarshal.EnumerableMarshal.ConvertToManaged((byte**)csl.Handle.DangerousGetHandle());

        result.Should().BeEquivalentTo(data);

    }

    [Fact]
    public void TestUnmanagedToManagedNull()
    {
        string[]? result = CStringArrayMarshal.ConvertToManaged(null)?.ToArray();
        result.Should().BeNull();
    }

    [Fact]
    public unsafe void TestUnmanagedToManagedNullDictionary()
    {
        Dictionary<string, string>? result = CStringArrayMarshal.DictionaryMarshal.ConvertToManaged(null);
        result.Should().BeNull();
    }

    [Fact]
    public unsafe void TestUnmanagedToManagedNullEnumerable()
    {
        IEnumerable<string>? result = CStringArrayMarshal.EnumerableMarshal.ConvertToManaged(null);
        result.Should().BeNull();
    }

    [Fact]
    public unsafe void TestUnmanagedToManagedEmpty()
    {
        byte** single = (byte**)NativeMemory.Alloc((nuint)sizeof(nint));
        single[0] = null;

        string[]? result = CStringArrayMarshal.ConvertToManaged(single)?.ToArray();
        result.Should().BeEmpty();
    }

    [Fact]
    public unsafe void TestUnmanagedToManagedEmptEnumerable()
    {
        byte** single = (byte**)NativeMemory.Alloc((nuint)sizeof(nint));
        single[0] = null;

        IEnumerable<string>? result = CStringArrayMarshal.EnumerableMarshal.ConvertToManaged(single);
        result.Should().BeEmpty();
    }

    [Fact]
    public unsafe void TestUnmanagedToManagedEmptyDictionary()
    {
        byte** single = (byte**)NativeMemory.Alloc((nuint)sizeof(nint));
        single[0] = null;

        Dictionary<string, string>? result = CStringArrayMarshal.DictionaryMarshal.ConvertToManaged(single);
        result.Should().BeEmpty();
    }

    public static IEnumerable<object[]> TestStrings =>

        new List<object[]>() {
            new object[] { TestData.AsciiOnly },
            new object[] { TestData.MixedUnicode }
        };

    public static IEnumerable<object[]> TestDictionaries =>

    new List<object[]>() {
            new object[] { TestData.ParameterPair }
    };

    public static class TestData
    {
        public static string[] AsciiOnly =>
            [
                "This is a test",
                "Each member of the array is null-terminated",
                "The entire array ends with two nulls in a row"
            ];

        public static string[] MixedUnicode =>
            [
                "This is a test with some Ùnicode characters",
                "This should perform the same, but some characters will take up multiple bytes",
                "CKJ: 乪 乫 Emoji:😄 BOM: \uFEFF"
            ];

        public static Dictionary<string, string> ParameterPair => new()
        {
                {"TestParameter", "This is a test" },
                {"TestParameter2", "This is a test that has an = sign in the value" },
                {"TestParameter3", "This is a test parameter with unicode 😄" },
        };
    }
}