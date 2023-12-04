// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public sealed partial class OgrFeature : IDisposable, IConstructableWrapper<OgrFeature, OgrFeature.MarshalHandle>, IHasHandle<OgrFeature.MarshalHandle>
{
    public void Dispose()
    {
        ((IDisposable)Handle).Dispose();
    }

    internal class MarshalHandle : GdalInternalHandle, IConstructableHandle<MarshalHandle>
    {
        private MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        static MarshalHandle IConstructableHandle<MarshalHandle>.Construct(bool ownsHandle) => ownsHandle ? new Owns() : new DoesntOwn();

        protected override bool ReleaseHandle()
        {
            lock (ReentrantLock)
            {
                if (base.IsInvalid)
                    return false;
                GdalError.ResetErrors();
                Interop.OGR_F_Destroy(handle);
                return GdalError.LastError is not null && GdalError.LastError.Severity is not GdalCplErr.Failure or GdalCplErr.Fatal;
            }
        }

        internal class Owns: MarshalHandle
        {
            public Owns() : base(true) { }
        }

        internal class DoesntOwn : MarshalHandle
        {
            public DoesntOwn() : base(true) { }
        }
    }
}