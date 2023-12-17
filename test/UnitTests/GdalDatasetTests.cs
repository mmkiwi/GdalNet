// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.UnitTests;

//[Collection("GDAL")]
public sealed class GdalDatasetTests: DatasetTestBase
{
    [Theory]
    [MemberData(nameof(Datasets))]
    public void TestOpenDoesNotThrow(int index)
    {
        var datasetInfo = GetDataset(index);
        Action action = () =>
        {
            using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
            _ = virtualDataset.Dataset;
        };
        action.Should().NotThrow("should exist");
    }

    [Fact]
    public void TestOpenThrows()
    {
        Action action = () =>
        {
            using var dataset = GdalDataset.Open("DOESNOTEXIST");
            if (dataset is not null)
                throw new Exception(dataset.Description);
            throw new Exception("X");
        };
        action.Should().Throw<IOException>();
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void TestRasterCount(int index)
    {
        var datasetInfo = GetDataset(index);

        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.RasterBands.Count.Should().Be(datasetInfo.Dataset.RasterCount);
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void TestRasterXSize(int index)
    {
        var datasetInfo = GetDataset(index);

        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.RasterXSize.Should().Be(datasetInfo.Dataset.RasterXSize);
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void TestRasterYSize(int index)
    {
        var datasetInfo = GetDataset(index);

        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.RasterYSize.Should().Be(datasetInfo.Dataset.RasterYSize);
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void TestLayerCount(int index)
    {
        var datasetInfo = GetDataset(index);

        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.Layers.Count.Should().Be(datasetInfo.File.Layers.Length);
    }
}
