// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.Contracts;

namespace MMKiwi.GdalNet;

public partial class OgrFieldDomain:IConstructibleWrapper<OgrFieldDomain, OgrFieldDomain.MarshalHandle>, IDisposable, IHasHandle<OgrFieldDomain.MarshalHandle>
{
    private bool _disposedValue;

    private protected OgrFieldDomain(MarshalHandle handle) => Handle = handle;

    private protected MarshalHandle Handle { get; }

    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    internal abstract class MarshalHandle : GdalInternalHandle, IConstructibleHandle<MarshalHandle>
    {
        public MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        static MarshalHandle IConstructibleHandle<MarshalHandle>.Construct(bool ownsHandle) => ownsHandle ? new Owns() : new DoesntOwn();

        protected override bool ReleaseHandle()
        {
            lock (ReentrantLock)
            {
                if (base.IsInvalid)
                    return false;
                GdalError.ResetErrors();
                Interop.OGR_FldDomain_Destroy(handle);
                return GdalError.LastError is not null && GdalError.LastError.Severity is not GdalCplErr.Failure or GdalCplErr.Fatal;
            }
        }

        public sealed class Owns : MarshalHandle { public Owns() : base(true) { } }
        public sealed class DoesntOwn : MarshalHandle { public DoesntOwn() : base(true) { } }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                ((IDisposable)Handle).Dispose();
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
