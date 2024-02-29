// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;

namespace MMKiwi.GdalNet;

public class GdalBandCollection : IReadOnlyList<GdalRasterBand>
{
    internal GdalBandCollection(GdalDataset dataset)
    {
        Dataset = dataset;
    }

    [SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Clarity")]
    public GdalRasterBand this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return GdalH.GDALGetRasterBand(Dataset, index + 1) ?? throw new InvalidOperationException();
        }
    }

    public int Count => GdalH.GDALGetRasterCount(Dataset);

    private GdalDataset Dataset { get; }

    public IEnumerator<GdalRasterBand> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return this[i];
        }
    }

    [ExcludeFromCodeCoverage]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

