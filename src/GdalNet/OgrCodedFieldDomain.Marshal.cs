// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal.In))]
public sealed partial class OgrCodedFieldDomain
{
    new internal static partial class Marshal
    {
        [CustomMarshaller(typeof(OgrCodedFieldDomain), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(OgrCodedFieldDomain? handle) => handle is null ? 0 : handle.Handle;
        }

        [CustomMarshaller(typeof(OgrCodedFieldDomain), MarshalMode.Default, typeof(DoesNotOwnHandle))]
        internal static partial class DoesNotOwnHandle
        {
            public static nint ConvertToUnmanaged(OgrCodedFieldDomain? handle) => handle is null ? 0 : handle.Handle;
            public static OgrCodedFieldDomain? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new OgrCodedFieldDomain(pointer, false);
            }
        }

        [CustomMarshaller(typeof(OgrCodedFieldDomain), MarshalMode.Default, typeof(OwnsHandle))]
        internal static partial class OwnsHandle
        {
            public static nint ConvertToUnmanaged(OgrCodedFieldDomain? handle) => handle is null ? 0 : handle.Handle;
            public static OgrCodedFieldDomain? ConvertToManaged(nint pointer)
            {
                return pointer <= 0 ? null : new OgrCodedFieldDomain(pointer, true);
            }
        }
    }
}