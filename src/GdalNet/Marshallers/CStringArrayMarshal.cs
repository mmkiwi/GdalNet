// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

using MMKiwi.GdalNet.CHelpers;

namespace MMKiwi.GdalNet.Marshallers;

[CustomMarshaller(typeof(string[]), MarshalMode.Default, typeof(StringArray))]
[CustomMarshaller(typeof(IEnumerable<string>), MarshalMode.Default, typeof(EnumerableMarshal))]
[CustomMarshaller(typeof(IReadOnlyDictionary<string, string>), MarshalMode.Default, typeof(ReadOnlyDictionaryMarshal))]
[CustomMarshaller(typeof(Dictionary<string, string>), MarshalMode.Default, typeof(DictionaryMarshal))]
[CustomMarshaller(typeof(string[]), MarshalMode.ManagedToUnmanagedOut, typeof(FreeReturn))]
internal unsafe static class CStringArrayMarshal
{
    private static T ConvertToManagedDictionary<T>(byte** unmanaged, T results)
        where T : IDictionary<string, string>
    {
        int i = 0;
        while (true)
        {
            byte* currStringPtr = unmanaged[i];

            if (currStringPtr == null)
                break;

            ReadOnlySpan<byte> currString = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(currStringPtr);
            int equalLoc = currString.IndexOf("="u8);
            string key = Encoding.UTF8.GetString(currString[..equalLoc]);
            string value = Encoding.UTF8.GetString(currString[(equalLoc + 1)..]);
            results[key] = value;
            i++;
        }

        return results;
    }

    public static class DictionaryMarshal
    {
        public static Dictionary<string, string>? ConvertToManaged(byte** unmanaged)
            => unmanaged == null ? null : ConvertToManagedDictionary(unmanaged, new Dictionary<string, string>());

        public static byte** ConvertToUnmanaged(Dictionary<string, string>? managed)
            => ReadOnlyDictionaryMarshal.ConvertToUnmanaged(managed);
        
        public static void Free(byte** unmanaged) => FreeCore(unmanaged);

    }

    public static class ReadOnlyDictionaryMarshal
    {
        public static IReadOnlyDictionary<string, string>? ConvertToManaged(byte** unmanaged)
        {
            if (unmanaged == null)
                return null;
            
            var result = ConvertToManagedDictionary(unmanaged, ImmutableDictionary.CreateBuilder<string,string>());
            return result.ToImmutable();
        }

        public static byte** ConvertToUnmanaged(IReadOnlyDictionary<string, string>? managed)
        {
            return ConvertToUnmanagedCore(managed?.Select(kvp => $"{kvp.Key}={kvp.Value}"), managed?.Count ?? 0);
        }
        
        public static void Free(byte** unmanaged) => FreeCore(unmanaged);

    }

    public static class StringArray
    {
        public static string[]? ConvertToManaged(byte** unmanaged)
        {
            List<string> results = [];
            int i = 0;
            if (unmanaged == null)
                return null;

            while (true)
            {
                byte* currStringPtr = unmanaged[i];

                if (currStringPtr == null)
                    break;

                ReadOnlySpan<byte> currString = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(currStringPtr);
                results.Add(Encoding.UTF8.GetString(currString));

                i++;
            }

            return [.. results];
        }

        public static byte** ConvertToUnmanaged(string[]? managed) => ConvertToUnmanagedCore(managed);
        
        public static void Free(byte** unmanaged) => FreeCore(unmanaged);

    }

    public static class EnumerableMarshal
    {
        public static byte** ConvertToUnmanaged(IEnumerable<string>? managed) => ConvertToUnmanagedCore(managed);

        public static IEnumerable<string>? ConvertToManaged(byte** unmanaged) =>
            StringArray.ConvertToManaged(unmanaged);

        public static void Free(byte** unmanaged) => FreeCore(unmanaged);
    }

    private static void FreeCore(byte** unmanaged)
    {
        if (unmanaged == null)
            return;
        int i = 0;
        while (true)
        {
            byte* cur = unmanaged[i];
            if (cur is null)
                break;

            Utf8StringMarshaller.Free(cur);

            i++;
        }

        NativeMemory.Free(unmanaged);
    }
    
    private static byte** ConvertToUnmanagedCore(IEnumerable<string>? managed, int count = -1)
    {
        if (managed is null)
            return default;

        if (count == -1)
        {
            if (!managed.TryGetNonEnumeratedCount(out count))
            {
                managed = managed.ToArray();
                count = managed.Count();
            }
        }

        byte** mem = (byte**)NativeMemory.Alloc((nuint)count + 1, (nuint)sizeof(nint));

        int i = 0;
        foreach (string item in managed)
        {
            mem[i] = Utf8StringMarshaller.ConvertToUnmanaged(item);
            i++;
        }

        mem[count] = null; // The last pointer in the array needs to be null

        return mem;
    }
    
    [CLSCompliant(false)]
    public static class FreeReturn
    {
        public static string[]? ConvertToManaged(byte** unmanaged) =>
            StringArray.ConvertToManaged(unmanaged);

        public static void Free(byte** unmanaged) => CStringList.Interop.CSLDestroy(unmanaged);
    }
}