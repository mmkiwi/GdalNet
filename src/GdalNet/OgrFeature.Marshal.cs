// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal.In))]
public partial class OgrFeature
{
    private OgrFeature(nint pointer, bool ownsHandle) : base(pointer, ownsHandle) { }

    internal static partial class Marshal
    {
        [CustomMarshaller(typeof(OgrFeature), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(OgrFeature? handle) => handle is null ? 0 : handle.Handle;
        }

        [CustomMarshaller(typeof(OgrFeature), MarshalMode.Default, typeof(DoesNotOwnHandle))]
        internal static partial class DoesNotOwnHandle
        {
            public static nint ConvertToUnmanaged(OgrFeature? handle) => handle is null ? 0 : handle.Handle;
            public static OgrFeature? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new OgrFeature(pointer, false);
            }
        }

        [CustomMarshaller(typeof(OgrFeature), MarshalMode.Default, typeof(OwnsHandle))]
        internal static partial class OwnsHandle
        {
            public static nint ConvertToUnmanaged(OgrFeature? handle) => handle is null ? 0 : handle.Handle;
            public static OgrFeature? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new OgrFeature(pointer, true);
            }
        }
    }
}