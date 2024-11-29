// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet.Handles;

internal sealed class OgrSpatialReferenceHandle : GdalInternalHandle, IConstructableHandle<OgrSpatialReferenceHandle>
{
    private OgrSpatialReferenceHandle(bool ownsHandle) : base(ownsHandle)
    {
    }

    private protected override bool ReleaseHandleCore()
    {
        OgrSrsApiH.OSRDestroySpatialReference(handle);
        GdalError.ThrowIfError();
        return true;
    }

    public static OgrSpatialReferenceHandle Construct(bool ownsHandle) => new(ownsHandle);
}