// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public partial class OgrSpatialReference: IConstructableWrapper<OgrSpatialReference, OgrSpatialReference.MarshalHandle>, IHasHandle<OgrSpatialReference.MarshalHandle>
{
    internal class MarshalHandle : GdalInternalHandleNeverOwns, IConstructableHandle<MarshalHandle>
    {
        static MarshalHandle IConstructableHandle<MarshalHandle>.Construct(bool ownsHandle)
        {
            Debug.Assert(ownsHandle is false); // Should never be true
            return new();
        }
    }
}