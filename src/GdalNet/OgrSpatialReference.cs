// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshallerNeverOwns<OgrSpatialReference, OgrSpatialReferenceHandle>))]
public sealed class OgrSpatialReference : IConstructableWrapper<OgrSpatialReference, OgrSpatialReferenceHandle>, IHasHandle<OgrSpatialReferenceHandle>
{
    private OgrSpatialReference(OgrSpatialReferenceHandle handle) => Handle = handle;
    internal OgrSpatialReferenceHandle Handle { get; }

    static OgrSpatialReference IConstructableWrapper<OgrSpatialReference, OgrSpatialReferenceHandle>.Construct(OgrSpatialReferenceHandle handle)
        => new(handle);
    OgrSpatialReferenceHandle IHasHandle<OgrSpatialReferenceHandle>.Handle => Handle;
}