﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal.In))]
public partial class OgrFieldDomain
{
    protected OgrFieldDomain(nint handle, bool ownsHandle):base(handle, ownsHandle) { }

    internal static partial class Marshal
    {
        [CustomMarshaller(typeof(OgrFieldDomain), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(OgrFieldDomain? handle) => handle is null ? 0 : handle.Handle;
        }

        [CustomMarshaller(typeof(OgrFieldDomain), MarshalMode.Default, typeof(DoesNotOwnHandle))]
        internal static partial class DoesNotOwnHandle
        {
            public static nint ConvertToUnmanaged(OgrFieldDomain? handle) => handle is null ? 0 : handle.Handle;
            public static OgrFieldDomain? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new OgrFieldDomain(pointer, false);
            }
        }

        [CustomMarshaller(typeof(OgrFieldDomain), MarshalMode.Default, typeof(OwnsHandle))]
        internal static partial class OwnsHandle
        {
            public static nint ConvertToUnmanaged(OgrFieldDomain? handle) => handle is null ? 0 : handle.Handle;
            public static OgrFieldDomain? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new OgrFieldDomain(pointer, true);
            }
        }
    }
}
