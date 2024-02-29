// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

namespace MMKiwi.GdalNet.Marshallers;
[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(Utf8StringNoFree))]
internal unsafe static class Utf8StringNoFree
{
    public static string? ConvertToManaged(byte* unmanaged)
        => unmanaged == null ? null : Marshal.PtrToStringUTF8((nint)unmanaged);

    public static byte* ConvertToUnmanaged(string? value)
        => Utf8StringMarshaller.ConvertToUnmanaged(value);
}