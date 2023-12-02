// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalHandleMarshallerIn<GdalDataset, GdalDataset.MarshalHandle>))]
public sealed partial class GdalDataset :IConstructibleWrapper<GdalDataset, GdalDataset.MarshalHandle>
{
    private GdalDataset(MarshalHandle handle) : base(handle)
    {
        Handle = handle;
        RasterBands = new(this);
        Layers = new(this);
    }
    MarshalHandle Handle { get; }
    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    static GdalDataset? IConstructibleWrapper<GdalDataset, MarshalHandle>.Construct(MarshalHandle handle) => new(handle);

    internal sealed class MarshalHandle : GdalInternalHandle, IConstructibleHandle<MarshalHandle>
    {
        private MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        public static MarshalHandle Construct(bool ownsHandle) => new(ownsHandle);

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
    }
}
