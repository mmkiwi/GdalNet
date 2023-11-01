// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.CHelpers;

[NativeMarshalling(typeof(Marshal.In))]
internal unsafe sealed partial class CStringList
{
    private CStringList(nint pointer, bool ownsHandle) : base(pointer, ownsHandle) { }

    internal static partial class Marshal
    {
        [CustomMarshaller(typeof(CStringList), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(CStringList? handle) => handle is null ? 0 : handle.Handle;
        }

        [CustomMarshaller(typeof(CStringList), MarshalMode.Default, typeof(DoesNotOwnHandle))]
        internal static partial class DoesNotOwnHandle
        {
            public static nint ConvertToUnmanaged(CStringList? handle) => handle is null ? 0 : handle.Handle;
            public static CStringList? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new CStringList(pointer, false);
            }
        }

        [CustomMarshaller(typeof(CStringList), MarshalMode.Default, typeof(OwnsHandle))]
        internal static partial class OwnsHandle
        {
            public static nint ConvertToUnmanaged(CStringList? handle) => handle is null ? 0 : handle.Handle;
            public static CStringList? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new CStringList(pointer, true);
            }
        }
    }
}
