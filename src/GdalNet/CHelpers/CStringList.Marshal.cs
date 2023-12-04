// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet.CHelpers;

[GdalGenerateWrapper(HandleSetVisibility = MemberVisibility.Private)]
internal unsafe sealed partial class CStringList : IConstructableWrapper<CStringList, CStringList.MarshalHandle>, IHasHandle<CStringList.MarshalHandle>
{
    internal abstract partial class MarshalHandle : GdalInternalHandle, IConstructableHandle<MarshalHandle>
    {
        public MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        public static MarshalHandle Construct(bool ownsHandle) => ownsHandle ? new Owns() : new DoesntOwn();
        protected override bool ReleaseHandle()
        {
            lock (ReentrantLock)
            {
                if (base.IsInvalid)
                    return false;
                GdalError.ResetErrors();
                Interop.CSLDestroy(handle);
                return GdalError.LastError is not null && GdalError.LastError.Severity is not GdalCplErr.Failure or GdalCplErr.Fatal ;
            }
        }

        internal sealed class Owns : MarshalHandle
        {
            public Owns() : base(true) { }
        }

        internal sealed class DoesntOwn : MarshalHandle
        {
            public DoesntOwn() : base(false) { }
        }
    }
}
