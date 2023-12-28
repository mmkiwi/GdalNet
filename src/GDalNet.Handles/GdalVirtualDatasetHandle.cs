// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.



using MMKiwi.CBindingSG;

namespace MMKiwi.GdalNet.Handles;

[CbsgGenerateHandle]
internal abstract partial class GdalVirtualDatasetHandle : GdalInternalHandle, IConstructableHandle<GdalVirtualDatasetHandle>
{
    private protected override GdalCplErr? ReleaseHandleCore()
    {
        int res = Interop.VSIFCloseL(handle);
        return res >= 0 ? GdalCplErr.Failure : GdalCplErr.None;
    }
}