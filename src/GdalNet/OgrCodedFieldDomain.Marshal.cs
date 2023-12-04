// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public sealed partial class OgrCodedFieldDomain: IConstructibleWrapper<OgrCodedFieldDomain, OgrCodedFieldDomain.MarshalHandle>, IHasHandle<OgrCodedFieldDomain.MarshalHandle>
{
    private OgrCodedFieldDomain(MarshalHandle handle) : base(handle) { }
    new private MarshalHandle Handle => (MarshalHandle)base.Handle;
    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    new internal abstract class MarshalHandle: OgrFieldDomain.MarshalHandle
    {
        public MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        new public sealed class Owns : MarshalHandle { public Owns() : base(true) { } }
        new public sealed class DoesntOwn : MarshalHandle { public DoesntOwn() : base(true) { } }
    }
}