// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalHandleMarshallerIn<OgrCodedFieldDomain, OgrCodedFieldDomain.MarshalHandle>))]
public sealed partial class OgrCodedFieldDomain: IConstructibleWrapper<OgrCodedFieldDomain, OgrCodedFieldDomain.MarshalHandle>
{
    private OgrCodedFieldDomain(MarshalHandle handle) : base(handle) { }
    new private MarshalHandle Handle => (MarshalHandle)base.Handle;
    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    static OgrCodedFieldDomain? IConstructibleWrapper<OgrCodedFieldDomain, MarshalHandle>.Construct(MarshalHandle handle)
        => new(handle);

    new internal class MarshalHandle: OgrFieldDomain.MarshalHandle, IConstructibleHandle<MarshalHandle>
    {
        public MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        static MarshalHandle IConstructibleHandle<MarshalHandle>.Construct(bool ownsHandle) => new(ownsHandle);
    }
}