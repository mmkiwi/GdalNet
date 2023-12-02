﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public partial class OgrField
{
    public OgrFeature Feature { get; }
    public OgrFieldDefinition FieldDefinition { get; }
    public int Index { get; }

    internal OgrField(OgrFeature feature, int index)
    {
        Feature = feature;
        Index = index;
        FieldDefinition = OgrFeature.Interop.OGR_F_GetFieldDefnRef(Feature, Index);
    }
}