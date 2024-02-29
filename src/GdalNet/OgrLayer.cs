// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;


namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshallerNeverOwns<OgrLayer,OgrLayerHandle>))]
public partial class OgrLayer: IConstructableWrapper<OgrLayer, OgrLayerHandle>, IHasHandle<OgrLayerHandle>
{
    internal OgrLayer(OgrLayerHandle handle)
    {
        Handle = handle;
        Features = new OgrFeatureCollection(this);
    }

    public string Name
    {
        get
        {
            string name = OgrApiH.OGR_L_GetName(this);
            GdalError.ThrowIfError();
            return name;
        }
    }

    public long GetFeatureCount(bool force = false)
    {
        return OgrApiH.OGR_L_GetFeatureCount(this, force);
    }


    public OgrFeatureCollection Features { get; }

    public bool TryGetFeatureById(long id, [NotNullWhen(true)] out OgrFeature? feature)
    {
        feature = OgrApiH.OGR_L_GetFeature(this, id);
        return feature is not null;
    }

    public OgrFeature GetFeatureById(long id)
    {
        var feature = OgrApiH.OGR_L_GetFeature(this, id);
        GdalError.ThrowIfError();
        return feature ?? throw new InvalidOperationException($"Could not get feature with ID {id} from layer {Name}");
    }
    static OgrLayer IConstructableWrapper<OgrLayer, OgrLayerHandle>.Construct(OgrLayerHandle handle) => new(handle);
    
    internal OgrLayerHandle Handle { get; }
    OgrLayerHandle IHasHandle<OgrLayerHandle>.Handle => Handle;
}
