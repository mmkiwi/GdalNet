// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Text;

namespace MMKiwi.GdalNet.Marshallers;

[CustomMarshaller(typeof(string[]), MarshalMode.Default, typeof(CStringArrayMarshal))]
[CustomMarshaller(typeof(IEnumerable<string>), MarshalMode.Default, typeof(CStringArrayMarshal))]
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

    public static byte** ConvertToUnmanaged(string[]? managed) => ConvertToUnmanagedCore(managed);
    public static byte** ConvertToUnmanaged(IEnumerable<string>? managed) => ConvertToUnmanagedCore(managed);

    internal static byte** ConvertToUnmanagedCore(IEnumerable<string>? managed)
    {
        if (managed is null || !managed.Any())
            return default;

        if(!managed.TryGetNonEnumeratedCount(out int count))
        {
            managed = managed.ToArray();
        }

        byte** mem = (byte**)NativeMemory.Alloc((nuint)count + 1);

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
            if (i == 0)
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