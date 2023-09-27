// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;

namespace MMKiwi.GdalNet;

[SourceGenerators.GenerateGdalMarshal]
public partial class OgrFeature : GdalSafeHandle
{
    protected override bool ReleaseHandle()
    {
        if (Handle != nint.Zero)
        {
            Interop.OGR_F_Destroy(this);
            return true;
        }
        else return false;
    }
}

public partial class OgrFieldCollection : IReadOnlyList<OgrField>
{
    internal OgrFieldCollection(OgrFeature feature)
    {
        Feature = feature;
    }
    public OgrField this[int index] => throw new NotImplementedException();

    public int Count => OgrFeature.Interop.OGR_F_GetFieldCount(Feature);

    private OgrFeature Feature { get; }

    public IEnumerator<OgrField> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return new OgrField(Feature, i);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public partial class OgrField
{
    public OgrFeature Feature { get; }
    public OgrFieldDefinition FieldDefinition { get; }
    public int Index { get; }

    internal OgrField(OgrFeature feature, int index)
    {
        Feature = feature;
        Index = index;
        FieldDefinition = OgrFeature.Interop.OGR_F_GetFieldDefnRef(Feature, Index).AsReadOnly();
    }
}