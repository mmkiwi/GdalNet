// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.CBindingSG;

namespace MMKiwi.GdalNet.Handles;

[CbsgNeverOwns]
internal abstract class GdalInternalHandleNeverOwns() : GdalInternalHandle(false)
{
    private protected override GdalCplErr? ReleaseHandleCore() => default;
    protected override bool ReleaseHandle() => true;
}