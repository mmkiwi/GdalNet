// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public sealed partial class GdalDataset : IHasHandle<GdalDataset.MarshalHandle>, IConstructableWrapper<GdalDataset, GdalDataset.MarshalHandle>
{
    internal new MarshalHandle Handle => (MarshalHandle)base.Handle;

    [GdalGenerateHandle]
    internal abstract partial class MarshalHandle : GdalInternalHandle
    {
        public sealed class DoesntOwn() : MarshalHandle(false);
        public sealed class Owns() : MarshalHandle(true);
        protected override GdalCplErr? ReleaseHandleCore() => Interop.GDALClose(handle);
    }
}
