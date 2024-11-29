// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet.Handles;

internal class OgrFeatureDefinitionHandle : GdalInternalHandle, IConstructableHandle<OgrFeatureDefinitionHandle>
{
    private protected override bool ReleaseHandleCore()
    {
        OgrApiH.OGR_FD_Destroy(handle);
        GdalError.ThrowIfError();
        return true;
    }
    private OgrFeatureDefinitionHandle(bool ownsHandle) : base(ownsHandle)
    {
    }
    public static OgrFeatureDefinitionHandle Construct(bool ownsHandle) => new(ownsHandle);
}