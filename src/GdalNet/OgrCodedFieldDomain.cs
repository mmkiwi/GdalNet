// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.VisualBasic.FileIO;

using MMKiwi.CBindingSG;
using MMKiwi.GdalNet.Handles;


namespace MMKiwi.GdalNet;

[CbsgGenerateWrapper]
public sealed partial class OgrCodedFieldDomain : OgrFieldDomain, IConstructableWrapper<OgrCodedFieldDomain, OgrCodedFieldDomainHandle>, IHasHandle<OgrCodedFieldDomainHandle>
{
    public static OgrCodedFieldDomain Create(string name, string description, FieldType fieldType, OgrFieldSubType fieldSubType) => throw new NotImplementedException();

    private OgrCodedFieldDomain(OgrCodedFieldDomainHandle handle) : base(handle) { }
    new private OgrCodedFieldDomainHandle Handle => (OgrCodedFieldDomainHandle)base.Handle;

}