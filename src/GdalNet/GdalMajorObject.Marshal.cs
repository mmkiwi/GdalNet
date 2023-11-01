// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal.In))]
public abstract partial class GdalMajorObject
{
    internal static partial class Marshal
    {
        [CustomMarshaller(typeof(GdalMajorObject), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(GdalMajorObject? handle) => handle is null ? 0 : handle.Handle;
        }
    }
}