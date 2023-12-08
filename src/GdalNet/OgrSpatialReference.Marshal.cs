// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalHandleMarshaller<OgrSpatialReference, MarshalHandle>))]
public partial class OgrSpatialReference:IConstructibleWrapper<OgrSpatialReference, OgrSpatialReference.MarshalHandle>
{
    private OgrSpatialReference(MarshalHandle handle) => Handle = handle;

    internal MarshalHandle Handle { get; }

    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    static OgrSpatialReference? IConstructibleWrapper<OgrSpatialReference, MarshalHandle>.Construct(MarshalHandle handle)
        => new(handle);

    internal class MarshalHandle:GdalInternalHandleNeverOwns
    {

    }
}