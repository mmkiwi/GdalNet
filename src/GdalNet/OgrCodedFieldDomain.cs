// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.VisualBasic.FileIO;

namespace MMKiwi.GdalNet;

public sealed partial class OgrCodedFieldDomain : OgrFieldDomain
{
    private OgrCodedFieldDomain(nint handle, bool ownsHandle) : base(handle, ownsHandle) { }
    public static OgrCodedFieldDomain Create(string name, string description, FieldType fieldType, OgrFieldSubType fieldSubType) => throw new NotImplementedException();
}