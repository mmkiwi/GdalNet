// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal.In))]
public sealed partial class GdalDataset
{
    internal static new partial class Marshal
    {
        [CustomMarshaller(typeof(GdalDataset), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(GdalDataset? handle) => handle is null ? 0 : handle.Handle;
        }

        [CustomMarshaller(typeof(GdalDataset), MarshalMode.Default, typeof(DoesNotOwnHandle))]
        internal static partial class DoesNotOwnHandle
        {
            public static nint ConvertToUnmanaged(GdalDataset? handle) => handle is null ? 0 : handle.Handle;
            public static GdalDataset? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new GdalDataset(pointer, false);
            }
        }

        [CustomMarshaller(typeof(GdalDataset), MarshalMode.Default, typeof(OwnsHandle))]
        internal static partial class OwnsHandle
        {
            public static nint ConvertToUnmanaged(GdalDataset? handle) => handle is null ? 0 : handle.Handle;
            public static GdalDataset? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new GdalDataset(pointer, true);
            }
        }
    }
}
