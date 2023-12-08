// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public sealed partial class GdalVirtualDataset : IConstructibleWrapper<GdalVirtualDataset, GdalVirtualDataset.MarshalHandle>, IDisposable
{
    private bool _disposedValue;

    private GdalVirtualDataset(MarshalHandle handle) => Handle = handle;
    private MarshalHandle Handle { get; }

    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    static GdalVirtualDataset? IConstructibleWrapper<GdalVirtualDataset, MarshalHandle>.Construct(MarshalHandle handle) => new(handle);

    internal class MarshalHandle : GdalInternalHandle, IConstructibleHandle<MarshalHandle>
    {
        public MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        public static MarshalHandle Construct(bool ownsHandle) => new(ownsHandle);

        protected override bool ReleaseHandle()
        {
            lock (ReentrantLock)
            {
                GdalError.ResetErrors();
                int res = Interop.VSIFCloseL(handle);
                return res >= 0 && GdalError.LastError is not null && GdalError.LastError.Severity is not GdalCplErr.Failure or GdalCplErr.Fatal;
            }
        }
    }

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                MemoryHandle.Dispose();
                Handle.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}