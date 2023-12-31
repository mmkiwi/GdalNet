﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.



using MMKiwi.CBindingSG;

namespace MMKiwi.GdalNet.Handles;

[CbsgGenerateHandle]
internal abstract partial class OgrFeatureHandle : GdalInternalHandle, IConstructableHandle<OgrFeatureHandle>
{
    private protected override GdalCplErr? ReleaseHandleCore()
    {
        Interop.OGR_F_Destroy(handle);
        return null;
    }
}