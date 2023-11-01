// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal.In))]
public sealed partial class GdalVirtualDataset
{
    internal static partial class Marshal
    {
        [CustomMarshaller(typeof(GdalVirtualDataset), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(GdalVirtualDataset? handle) => handle is null ? 0 : handle.Handle;
        }

        [CustomMarshaller(typeof(GdalVirtualDataset), MarshalMode.Default, typeof(DoesNotOwnHandle))]
        internal static partial class DoesNotOwnHandle
        {
            public static nint ConvertToUnmanaged(GdalVirtualDataset? handle) => handle is null ? 0 : handle.Handle;
            public static GdalVirtualDataset? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new GdalVirtualDataset(pointer, false);
            }
        }

        [CustomMarshaller(typeof(GdalVirtualDataset), MarshalMode.Default, typeof(OwnsHandle))]
        internal static partial class OwnsHandle
        {
            public static nint ConvertToUnmanaged(GdalVirtualDataset? handle) => handle is null ? 0 : handle.Handle;
            public static GdalVirtualDataset? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new GdalVirtualDataset(pointer, true);
            }
        }
    }
}