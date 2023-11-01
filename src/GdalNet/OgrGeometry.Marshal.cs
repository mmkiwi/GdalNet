// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal.In))]
public partial class OgrGeometry
{
    internal static partial class Marshal
    {
        [CustomMarshaller(typeof(OgrGeometry), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(OgrGeometry? handle) => handle is null ? 0 : handle.Handle;
        }

        [CustomMarshaller(typeof(OgrGeometry), MarshalMode.Default, typeof(DoesNotOwnHandle))]
        internal static partial class DoesNotOwnHandle
        {
            public static nint ConvertToUnmanaged(OgrGeometry? handle) => handle is null ? 0 : handle.Handle;
            public static OgrGeometry? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new OgrGeometry(pointer);
            }
        }

    }
}
