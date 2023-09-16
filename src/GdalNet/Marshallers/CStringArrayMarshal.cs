// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Text;

namespace MMKiwi.GdalNet.Marshallers;

[CustomMarshaller(typeof(string[]), MarshalMode.Default, typeof(CStringArrayMarshal))]
[CustomMarshaller(typeof(IEnumerable<string>), MarshalMode.Default, typeof(EnumerableMarshal))]
[CustomMarshaller(typeof(IReadOnlyDictionary<string,string>), MarshalMode.Default, typeof(DictionaryMarshal))]
[CustomMarshaller(typeof(Dictionary<string, string>), MarshalMode.Default, typeof(DictionaryMarshal))]
[CustomMarshaller(typeof(string[]), MarshalMode.ManagedToUnmanagedOut, typeof(FreeReturn))]
internal unsafe static partial class CStringArrayMarshal
{
    public static string[]? ConvertToManaged(byte** unmanaged)
    {
        List<string> results = new();
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

        return results.ToArray();
    }

    public static class DictionaryMarshal
    {
        public static Dictionary<string, string>? ConvertToManaged(byte** unmanaged)
        {
            
            int i = 0;
            if (unmanaged == null)
                return null;

            Dictionary<string, string> results = new();
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

        public static byte** ConvertToUnmanaged(IReadOnlyDictionary<string, string>? managed)
        {
            return ConvertToUnmanagedCore(managed?.Select(kvp => $"{kvp.Key}={kvp.Value}"), managed?.Count ?? 0);
        }
    }

    public static byte** ConvertToUnmanaged(string[]? managed) => ConvertToUnmanagedCore(managed);
    
    public static class EnumerableMarshal
    {
        public static byte** ConvertToUnmanaged(IEnumerable<string>? managed) => ConvertToUnmanagedCore(managed);
        public static IEnumerable<string>? ConvertToManaged(byte** unmanaged) => CStringArrayMarshal.ConvertToManaged(unmanaged);
    }

    private static byte** ConvertToUnmanagedCore(IEnumerable<string>? managed, int count = -1)
    {
        if (managed is null || !managed.Any())
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

    public static void Free(byte** unmanaged)
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

    [CLSCompliant(false)]
    public static partial class FreeReturn
    {
        public static string[]? ConvertToManaged(byte** unmanaged) => CStringArrayMarshal.ConvertToManaged(unmanaged);

        public static void Free(byte** unmanaged) => CSLDestroy(unmanaged);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial void CSLDestroy(byte** unmanaged);
    }
}