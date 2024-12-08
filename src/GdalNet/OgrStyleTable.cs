// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<OgrStyleTable, OgrStyleTableHandle>))]
public sealed class OgrStyleTable : IConstructableWrapper<OgrStyleTable, OgrStyleTableHandle>,
    IHasHandle<OgrStyleTableHandle>, IDisposable
{
    static OgrStyleTable IConstructableWrapper<OgrStyleTable, OgrStyleTableHandle>.Construct(OgrStyleTableHandle handle) => new(handle);
    OgrStyleTableHandle IHasHandle<OgrStyleTableHandle>.Handle => Handle;

    private OgrStyleTable(OgrStyleTableHandle handle)
    {
        Handle = handle;
    }
    private OgrStyleTableHandle Handle { get; }
    public void Dispose()
    {
        Handle.Dispose();
    }
}