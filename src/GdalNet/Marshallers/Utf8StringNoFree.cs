// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.Marshallers;
[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(Utf8StringNoFree))]
[SuppressMessage("Usage", "SYSLIB1057:Marshaller type does not have the required shape", Justification = "This method is only for char* return values")]
internal unsafe static class Utf8StringNoFree
{
    public static string? ConvertToManaged(byte* unmanaged)
        => unmanaged == null ? null : Marshal.PtrToStringUTF8((nint)unmanaged);
}
