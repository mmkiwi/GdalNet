// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public sealed partial class OgrCodedFieldDomain: IConstructableWrapper<OgrCodedFieldDomain, OgrCodedFieldDomain.MarshalHandle>, IHasHandle<OgrCodedFieldDomain.MarshalHandle>
{
    private OgrCodedFieldDomain(MarshalHandle handle) : base(handle) { }
    new private MarshalHandle Handle => (MarshalHandle)base.Handle;
    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    [GdalGenerateHandle]
    new internal abstract partial class MarshalHandle : OgrFieldDomain.MarshalHandle, IConstructableHandle<MarshalHandle>
    {
        protected MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        public new sealed class DoesntOwn : MarshalHandle { public DoesntOwn() : base(false) { } }

        public new sealed class Owns : MarshalHandle { public Owns() : base(true) { } }
    }
}