// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;


namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<GdalDriver, GdalInternalHandle>))]
public partial class GdalDriver : IConstructableWrapper<GdalDriver, GdalDriverHandle>, IHasHandle<GdalDriverHandle>
{
    private GdalDriver(GdalDriverHandle handle) => Handle = handle;
    internal GdalDriverHandle Handle { get; }

    static GdalDriver IConstructableWrapper<GdalDriver, GdalDriverHandle>.Construct(GdalDriverHandle handle)
        => new(handle);
    
    GdalDriverHandle IHasHandle<GdalDriverHandle>.Handle => Handle;
}
