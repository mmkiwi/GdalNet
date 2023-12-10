// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public sealed partial class GdalVirtualDataset : IConstructableWrapper<GdalVirtualDataset, GdalVirtualDataset.MarshalHandle>, IHasHandle<GdalVirtualDataset.MarshalHandle>
{
    [GdalGenerateHandle]
    internal abstract partial class MarshalHandle : GdalInternalHandle, IConstructableHandle<MarshalHandle>
    {
        protected override GdalCplErr? ReleaseHandleCore()
        {
            int res = Interop.VSIFCloseL(handle);
            return res >= 0 ? GdalCplErr.Failure : GdalCplErr.None;
        }

        public sealed class Owns() : MarshalHandle(true);
        public sealed class DoesntOwn() : MarshalHandle(true);
    }
}