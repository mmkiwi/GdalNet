// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal.In))]
public partial class OgrLayer
{
    internal static partial class Marshal
    {
        [CustomMarshaller(typeof(OgrLayer), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(OgrLayer? handle) => handle is null ? 0 : handle.Handle;
        }

        [CustomMarshaller(typeof(OgrLayer), MarshalMode.Default, typeof(DoesNotOwnHandle))]
        internal static partial class DoesNotOwnHandle
        {
            public static nint ConvertToUnmanaged(OgrLayer? handle) => handle is null ? 0 : handle.Handle;
            public static OgrLayer? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new OgrLayer(pointer);
            }
        }
    }

}
