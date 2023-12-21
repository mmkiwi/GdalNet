// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;

namespace MMKiwi.GdalNet;

public class OgrFieldCollection : IReadOnlyList<OgrField>
{
    internal OgrFieldCollection(OgrFeature feature)
    {
        Feature = feature;
    }
    public OgrField this[int index] => throw new NotImplementedException();

    public int Count => OgrApiH.OGR_F_GetFieldCount(Feature);

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
