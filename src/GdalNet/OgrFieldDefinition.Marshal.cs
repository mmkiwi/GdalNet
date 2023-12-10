// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public partial class OgrFieldDefinition : IConstructableWrapper<OgrFieldDefinition, OgrFieldDefinition.MarshalHandle>, IHasHandle<OgrFieldDefinition.MarshalHandle>
{
    [GdalGenerateHandle]
    internal sealed partial class MarshalHandle : GdalInternalHandleNeverOwns, IConstructableHandle<MarshalHandle>;
}
