// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet.Handles;

internal class OgrGeometryHandle(bool ownsHandle) : GdalInternalHandle(ownsHandle), IConstructableHandle<OgrGeometryHandle>
{
    private protected override bool ReleaseHandleCore()
    {
        OgrApiH.OGR_G_DestroyGeometry(handle);
        return true;
    }
    public static OgrGeometryHandle Construct(bool ownsHandle) => new(ownsHandle);

}