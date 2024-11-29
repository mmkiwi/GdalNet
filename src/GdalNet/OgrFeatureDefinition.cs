// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<OgrFeatureDefinition, OgrFeatureDefinitionHandle>))]
public class OgrFeatureDefinition : IDisposable,
    IConstructableWrapper<OgrFeatureDefinition, OgrFeatureDefinitionHandle>,
    IHasHandle<OgrFeatureDefinitionHandle>
{
    static OgrFeatureDefinition IConstructableWrapper<OgrFeatureDefinition, OgrFeatureDefinitionHandle>.Construct(
        OgrFeatureDefinitionHandle handle)
        => new(handle);

    OgrFeatureDefinitionHandle IHasHandle<OgrFeatureDefinitionHandle>.Handle => Handle;

    private OgrFeatureDefinition(OgrFeatureDefinitionHandle handle) => Handle = handle;
    
    private OgrFeatureDefinitionHandle Handle { get; }
    public void Dispose() => Handle.Dispose();
}