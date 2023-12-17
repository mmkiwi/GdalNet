// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet.Handles;

[GdalGenerateHandle]
internal abstract partial class OgrGeometryHandle : GdalInternalHandle, IConstructableHandle<OgrGeometryHandle>
{
    protected override GdalCplErr? ReleaseHandleCore()
    {
        Interop.OGR_G_DestroyGeometry(handle);
        return null;
    }
}