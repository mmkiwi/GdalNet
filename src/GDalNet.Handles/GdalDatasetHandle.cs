// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.CBindingSG;

namespace MMKiwi.GdalNet.Handles;

[CbsgGenerateHandle]
internal abstract partial class GdalDatasetHandle : GdalInternalHandle, IConstructableHandle<GdalDatasetHandle>
{
    private protected override GdalCplErr? ReleaseHandleCore() => Interop.GDALClose(handle);
}