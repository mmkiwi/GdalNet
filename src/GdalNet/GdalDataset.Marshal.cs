// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;

namespace MMKiwi.GdalNet;

public sealed partial class GdalDataset : IHasHandle<GdalDataset.MarshalHandle>, IConstructibleWrapper<GdalDataset, GdalDataset.MarshalHandle>
{
    private GdalDataset(MarshalHandle handle) : base(handle)
    {
        RasterBands = new(this);
        Layers = new(this);
    }
    new MarshalHandle Handle => (MarshalHandle)base.Handle;
    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    static GdalDataset IConstructibleWrapper<GdalDataset, MarshalHandle>.Construct(MarshalHandle handle) => new(handle);

    internal abstract class MarshalHandle : GdalInternalHandle
    {
        private MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            lock (ReentrantLock)
            {
                if (base.IsInvalid)
                    return false;
                GdalError.ResetErrors();
                Interop.GDALClose(handle);
                return GdalError.LastError is not null && GdalError.LastError.Severity is not GdalCplErr.Failure or GdalCplErr.Fatal;
            }
        }

        internal class DoesNotOwn : MarshalHandle
        {
            public DoesNotOwn() : base(false) { }
        }

        internal class Owns : MarshalHandle
        {
            public Owns() : base(false) { }
        }
    }
}
