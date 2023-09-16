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
            RasterYSize = 1593
        }],
        Layers = ImmutableArray<TestLayer>.Empty,
    };

    public static TestFile Geopackage => new()
    {
        Data = Resources.PublicDomainGpkg,
        Datasets = [
        new TestDataset()
        {
            Options = new Dictionary<string, string> { { "Table", "RasterAerial1" } },
            RasterCount = 4,
            RasterXSize = 1741,
            RasterYSize = 2233
        },
        new TestDataset()
        {
            Options = new Dictionary<string, string> { { "Table", "RasterAerial2" } },
            RasterCount = 4,
            RasterXSize = 1741,
            RasterYSize = 2233,
        }],
        Layers = [
            new TestLayer { Name = "tl_2020_us_uac20", Index = 0 },
            new TestLayer { Name = "tl_2022_10_prisecroads", Index = 1 }
        ]
    };

    public static ImmutableArray<TestFile> Files => [Jpeg, Geopackage];
    public static ImmutableList<DatasetInfo> Datasets { get; } = Files.SelectMany(file => file.Datasets.Select(dataset => new DatasetInfo(file, dataset))).ToImmutableList();
    public static int DatasetCount => Files.Sum(f => f.Datasets.Length);

    public static ImmutableList<LayerInfo> Layers { get; } = Files.SelectMany(file => file.Layers.Select(layer => new LayerInfo(file, layer))).ToImmutableList();
    public static int LayerCount => Files.Sum(f => f.Layers.Length);
}

public record class LayerInfo(TestFile File, TestLayer Layer);
public record class DatasetInfo(TestFile File, TestDataset Dataset);