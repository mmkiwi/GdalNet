// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public sealed partial class GdalVirtualDataset : IConstructableWrapper<GdalVirtualDataset, GdalVirtualDataset.MarshalHandle>, IDisposable, IHasHandle<GdalVirtualDataset.MarshalHandle>
{
    private bool _disposedValue;

    internal abstract class MarshalHandle : GdalInternalHandle
    {
        public MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        protected override GdalCplErr? ReleaseHandleCore()
        {
            int res = Interop.VSIFCloseL(handle);
            return res >= 0 ? GdalCplErr.Failure : GdalCplErr.None;
        }

        public sealed class Owns : MarshalHandle { public Owns() : base(true) { } }
        public sealed class DoesntOwn : MarshalHandle { public DoesntOwn() : base(true) { } }
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