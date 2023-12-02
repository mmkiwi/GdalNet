// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using BenchmarkDotNet.Attributes;

using OSGeo.GDAL;
using OSGeo.OGR;

namespace Benchmark;

public partial class GdalBenchmarks
{
    static readonly string[] s_config = ["TABLE=RasterAerial1"];

    [Benchmark(Baseline = true)]
    public (int, List<DataType>) GdalSwig()
    {
        GdalConfiguration.ConfigureGdal();

        List<DataType> dataTypes = [];

        using (Dataset dataset = Gdal.OpenEx(FileName, 0, null, s_config, null))
        {

            for (int i = 0; i < dataset.RasterCount; i++)
            {
                Band band = dataset.GetRasterBand(i + 1);
                dataTypes.Add(band.DataType);
            }
        }

        using var a = Ogr.Open(FileName, 0);
        return (a.GetLayerCount(), dataTypes);
    }
}
