// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.Contracts;

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper(ConstructorVisibility = MemberVisibility.PrivateProtected)]
public partial class OgrFieldDomain : IConstructableWrapper<OgrFieldDomain, OgrFieldDomain.MarshalHandle>, IDisposable, IHasHandle<OgrFieldDomain.MarshalHandle>
{
    private bool _disposedValue;

    internal abstract class MarshalHandle : GdalInternalHandle, IConstructableHandle<MarshalHandle>
    {
        public MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        static MarshalHandle IConstructableHandle<MarshalHandle>.Construct(bool ownsHandle) => ownsHandle ? new Owns() : new DoesntOwn();

        protected override GdalCplErr? ReleaseHandleCore()
        {
            Interop.OGR_FldDomain_Destroy(handle);
            return null;

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
