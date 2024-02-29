// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet.Handles;

internal class GdalVirtualDatasetHandle : GdalInternalHandle, IConstructableHandle<GdalVirtualDatasetHandle>
{
    private protected override bool ReleaseHandleCore()
    {
        return GdalH.VSIFCloseL(handle) == 0;
    }
    internal GdalVirtualDatasetHandle(bool ownsHandle) : base(ownsHandle)
    {
    }
    static GdalVirtualDatasetHandle IConstructableHandle<GdalVirtualDatasetHandle>.Construct(bool ownsHandle)
        => new(ownsHandle);
}