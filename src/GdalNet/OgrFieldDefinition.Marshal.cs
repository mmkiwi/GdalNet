// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public partial class OgrFieldDefinition : IConstructibleWrapper<OgrFieldDefinition, OgrFieldDefinition.MarshalHandle>, IHasHandle<OgrFieldDefinition.MarshalHandle>
{
    internal MarshalHandle Handle { get; }

    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    internal partial class MarshalHandle : GdalInternalHandleNeverOwns, IConstructibleHandle<MarshalHandle>
    {
        static MarshalHandle IConstructibleHandle<MarshalHandle>.Construct(bool ownsHandle)
        {
            Debug.Assert(ownsHandle is false); // Should never be true
            return new();
        }
    }
}
