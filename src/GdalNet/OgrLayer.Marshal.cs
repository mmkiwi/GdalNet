// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;
using System.Reflection;

namespace MMKiwi.GdalNet;

public sealed partial class OgrLayer : IConstructibleWrapper<OgrLayer, OgrLayer.MarshalHandle>, IHasHandle<OgrLayer.MarshalHandle>
{
    internal OgrLayer(MarshalHandle handle)
    {
        Handle = handle;
        Features = new(this);
    }

    internal MarshalHandle Handle { get; }

    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    static OgrLayer IConstructibleWrapper<OgrLayer, MarshalHandle>.Construct(MarshalHandle handle) 
        => new(handle);

    internal class MarshalHandle : GdalInternalHandleNeverOwns, IConstructibleHandle<MarshalHandle>
    {
        static MarshalHandle IConstructibleHandle<MarshalHandle>.Construct(bool ownsHandle)
        {
            Debug.Assert(ownsHandle is false); // Should never be true
            return new();
        }
    }
}
