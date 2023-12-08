// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using BenchmarkDotNet.Attributes;

using MMKiwi.GdalNet;

namespace Benchmark;

public partial class GdalBenchmarks
{
    [Benchmark]
    public (int, List<GdalDataType>) GdalNet()
    {
        GdalInfo.RegisterAllDrivers();
        using var dataset = GdalDataset.Open(FileName, openOptions: new Dictionary<string, string> { { "TABLE", "RasterAerial1" } });
        List<GdalDataType> dataTypes = [];
        foreach (var band in dataset.RasterBands)
        {
            dataTypes.Add(band.DataType);
        }

        return (dataset.Layers.Count, dataTypes);
    }
}
