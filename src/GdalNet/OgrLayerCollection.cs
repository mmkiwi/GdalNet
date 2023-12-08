// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace MMKiwi.GdalNet;

public class OgrLayerCollection : IReadOnlyList<OgrLayer>
{
    internal OgrLayerCollection(GdalDataset dataset)
    {
        Dataset = dataset;
    }

    [SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Clarity")]
    public OgrLayer this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            return GdalDataset.Interop.GDALDatasetGetLayer(Dataset, index);
        }
    }

    public OgrLayer this[string key]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(key);
            return GdalDataset.Interop.GDALDatasetGetLayerByName(Dataset, key) ?? throw new NotImplementedException();
        }
    }

    public GdalDataset Dataset { get; }

    public int Count => GdalDataset.Interop.GDALDatasetGetLayerCount(Dataset);

    public IEnumerator<OgrLayer> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
            yield return this[i];
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out OgrLayer value)
    {
        value = GdalDataset.Interop.GDALDatasetGetLayerByName(Dataset, key);
        return value != null;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
