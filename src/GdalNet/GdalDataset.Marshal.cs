// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;
using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public sealed partial class GdalDataset : IHasHandle<GdalDataset.MarshalHandle>, IConstructableWrapper<GdalDataset, GdalDataset.MarshalHandle>
{
    private GdalDataset(MarshalHandle handle) : base(handle)
    {
        RasterBands = new(this);
        Layers = new(this);
    }
    new MarshalHandle Handle => (MarshalHandle)base.Handle;
    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    [GdalGenerateHandle]
    internal abstract partial class MarshalHandle : GdalInternalHandle
    {
        public sealed class DoesntOwn : MarshalHandle { public DoesntOwn() : base(false) { } }
        public sealed class Owns : MarshalHandle { public Owns() : base(true) { } }
        protected override GdalCplErr? ReleaseHandleCore() => Interop.GDALClose(handle);
    }
}
