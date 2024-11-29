// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;

using MMKiwi.GdalNet.Error;

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

            var res = GdalH.GDALDatasetGetLayer(Dataset, index);
            GdalError.ThrowIfError();
            return res ?? throw new GdalException("Unknown error retrieving layer");
        }
    }

    public OgrLayer this[string key]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(key);
            var result = GdalH.GDALDatasetGetLayerByName(Dataset, key);
            GdalError.ThrowIfError();
            return result ?? throw new GdalException($"Error getting layer {key}. GDAL did not report an error.");
        }
    }

    public GdalDataset Dataset { get; }

    public int Count
    {
        get
        {
            var result = GdalH.GDALDatasetGetLayerCount(Dataset);
            GdalError.ThrowIfError();
            return result;
        }
    }

    public IEnumerator<OgrLayer> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
            yield return this[i];
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out OgrLayer value)
    {
        value = GdalH.GDALDatasetGetLayerByName(Dataset, key);
        GdalError.ThrowIfError();
        return value != null;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public OgrLayer Create(string name, OgrSpatialReference? spatialReference = null,
        OgrWkbGeometryType geometryType = OgrWkbGeometryType.Unknown, string[]? options = null)
    {
        var result = GdalH.GDALDatasetCreateLayer(Dataset, name, spatialReference, geometryType, options);
        GdalError.ThrowIfError();
        return result;
    }
}