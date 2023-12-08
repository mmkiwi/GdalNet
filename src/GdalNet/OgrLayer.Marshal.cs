// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;
using System.Reflection;

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public sealed partial class OgrLayer : IConstructableWrapper<OgrLayer, OgrLayer.MarshalHandle>, IHasHandle<OgrLayer.MarshalHandle>
{
    internal OgrLayer(MarshalHandle handle)
    {
        Handle = handle;
        Features = new(this);
    }

    [GdalGenerateHandle]
    internal sealed partial class MarshalHandle : GdalInternalHandleNeverOwns, IConstructableHandle<MarshalHandle>    {    }
}
