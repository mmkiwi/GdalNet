// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<OgrGeometryFieldDefinition, OgrGeometryFieldDefinitionHandle>))]
public sealed class OgrGeometryFieldDefinition : IConstructableWrapper<OgrGeometryFieldDefinition, OgrGeometryFieldDefinitionHandle>,
    IHasHandle<OgrGeometryFieldDefinitionHandle>, IDisposable
{
    static OgrGeometryFieldDefinition IConstructableWrapper<OgrGeometryFieldDefinition, OgrGeometryFieldDefinitionHandle>.Construct(OgrGeometryFieldDefinitionHandle handle) => new(handle);
    OgrGeometryFieldDefinitionHandle IHasHandle<OgrGeometryFieldDefinitionHandle>.Handle => Handle;

    private OgrGeometryFieldDefinition(OgrGeometryFieldDefinitionHandle handle)
    {
        Handle = handle;
    }
    
    private OgrGeometryFieldDefinitionHandle Handle { get; }

    public void Dispose()
    {
        Handle.Dispose();
    }
}