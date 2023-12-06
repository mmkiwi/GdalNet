// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public partial class OgrSpatialReference: IConstructableWrapper<OgrSpatialReference, OgrSpatialReference.MarshalHandle>, IHasHandle<OgrSpatialReference.MarshalHandle>
{
    [GdalGenerateHandle]
    internal sealed partial class MarshalHandle : GdalInternalHandleNeverOwns, IConstructableHandle<MarshalHandle> { }
}