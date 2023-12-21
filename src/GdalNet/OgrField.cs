// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public class OgrField
{
    public OgrFeature Feature { get; }

    private readonly Lazy<OgrFieldDefinition> _fieldDefinition;
    public OgrFieldDefinition FieldDefinition => _fieldDefinition.Value;
    public int Index { get; }

    internal OgrField(OgrFeature feature, int index)
    {
        Feature = feature;
        Index = index;
        _fieldDefinition = new(GetFieldDefinition);
    }

    private OgrFieldDefinition GetFieldDefinition()
        => OgrApiH.OGR_F_GetFieldDefnRef(this.Feature, this.Index);
}