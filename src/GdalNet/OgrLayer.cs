// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;

using MMKiwi.CBindingSG;
using MMKiwi.GdalNet.Handles;


namespace MMKiwi.GdalNet;

[CbsgGenerateWrapper]
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
}
