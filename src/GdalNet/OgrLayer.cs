// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshallerNeverOwns<OgrLayer,OgrLayerHandle>))]
public class OgrLayer: IConstructableWrapper<OgrLayer, OgrLayerHandle>, IHasHandle<OgrLayerHandle>
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
        var result = OgrApiH.OGR_L_GetFeatureCount(this, force);
        GdalError.ThrowIfError();
        return result;
    }

    public OgrFeatureDefinition FeatureDefinition
    {
        get
        {
            var result = OgrApiH.OGR_L_GetLayerDefn(this);
            GdalError.ThrowIfError();
            return result;
        }
    }


    public OgrFeatureCollection Features { get; }

    public bool TryGetFeatureById(long id, [NotNullWhen(true)] out OgrFeature? feature)
    {
        feature = OgrApiH.OGR_L_GetFeature(this, id);
        GdalError.ThrowIfError();
        return feature is not null;
    }

    public OgrFeature GetFeatureById(long id)
    {
        var feature = OgrApiH.OGR_L_GetFeature(this, id);
        GdalError.ThrowIfError();
        return feature ?? throw new InvalidOperationException($"Could not get feature with ID {id} from layer {Name}");
    }
    static OgrLayer IConstructableWrapper<OgrLayer, OgrLayerHandle>.Construct(OgrLayerHandle handle) => new(handle);
    
    private OgrLayerHandle Handle { get; }
    OgrLayerHandle IHasHandle<OgrLayerHandle>.Handle => Handle;

    public void CreateField(OgrFieldDefinition field, bool approximateOk)
    {
        OgrApiH.OGR_L_CreateField(this, field, approximateOk).ThrowIfError();
    }
}
