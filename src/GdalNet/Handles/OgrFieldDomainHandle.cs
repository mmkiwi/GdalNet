// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet.Handles;

internal class OgrFieldDomainHandle : GdalInternalHandle, IConstructableHandle<OgrFieldDomainHandle>
{
    private protected override bool ReleaseHandleCore()
    {
        OgrApiH.OGR_FldDomain_Destroy(handle);
        return true;
    }
    private protected OgrFieldDomainHandle(bool ownsHandle) : base(ownsHandle)
    {
    }
    public static OgrFieldDomainHandle Construct(bool ownsHandle) => new(ownsHandle);
}

internal sealed class OgrCodedFieldDomainHandle : OgrFieldDomainHandle, IConstructableHandle<OgrCodedFieldDomainHandle>
{
    private OgrCodedFieldDomainHandle(bool ownsHandle) : base(ownsHandle)
    {
    }
    
    public static OgrCodedFieldDomainHandle Construct(bool ownsHandle) => new(ownsHandle);
}