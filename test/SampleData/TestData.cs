// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;

namespace MMKiwi.GdalNet.SampleData;

public static class TestData
{
    public static TestFile Jpeg => new()
    {
        Data = Resources.SampleJpg,
        Datasets = [
        new TestDataset()
        {

            RasterCount = 3,
            RasterXSize = 1203,
            RasterYSize = 1593,
        }]
    };

    public static TestFile GpkgRaster1 => new()
    {
        Data = Resources.PublicDomainGpkg,
        Datasets = [
        new TestDataset()
        {
            Options = new Dictionary<string, string> { {"Table", "RasterAerial1" } },
            RasterCount = 4,
            RasterXSize = 1741,
            RasterYSize = 2233,
        },
        new TestDataset()
        {
            Options = new Dictionary<string, string> { { "Table", "RasterAerial2" } },
            RasterCount = 4,
            RasterXSize = 1741,
            RasterYSize = 2233,
        }]
    };

    public static ImmutableArray<TestFile> Files => [Jpeg, GpkgRaster1];
    public static ImmutableList<(TestFile, TestDataset)> Datasets { get; } = Files.SelectMany(file => file.Datasets.Select(dataset => (file, dataset))).ToImmutableList();
    public static int DatasetCount => Files.Sum(f=>f.Datasets.Length);
}
