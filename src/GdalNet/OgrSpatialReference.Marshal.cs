// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public partial class OgrSpatialReference
{
    internal static partial class Marshal
    {
        [CustomMarshaller(typeof(OgrSpatialReference), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(OgrSpatialReference? handle) => handle is null ? 0 : handle.Handle;
        }

        [CustomMarshaller(typeof(OgrSpatialReference), MarshalMode.Default, typeof(DoesNotOwnHandle))]
        internal static partial class DoesNotOwnHandle
        {
            public static nint ConvertToUnmanaged(OgrSpatialReference? handle) => handle is null ? 0 : handle.Handle;
            public static OgrSpatialReference? ConvertToManaged(nint pointer) => pointer <= 0 ? null : new OgrSpatialReference(pointer);
        }
    }
}