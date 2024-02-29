// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet.Handles;

internal class OgrFeatureHandle : GdalInternalHandle, IConstructableHandle<OgrFeatureHandle>
{
    private protected override bool ReleaseHandleCore()
    {
        OgrApiH.OGR_F_Destroy(handle);
        return true;
    }
    private OgrFeatureHandle(bool ownsHandle) : base(ownsHandle)
    {
    }
    public static OgrFeatureHandle Construct(bool ownsHandle) => new(ownsHandle);
}