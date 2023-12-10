// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using FluentAssertions.Execution;

namespace MMKiwi.GdalNet.UnitTests;

//[Collection("GDAL")]
public sealed class GdalRasterBandTests:DatasetTestBase
{

    [Theory]
    [MemberData(nameof(Datasets))]
    public void TestRasterIterate(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);

        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options)!;
        var dataset = virtualDataset.Dataset;

        foreach (var band in dataset.RasterBands)
            band.DataType.Should().Be(GdalDataType.Byte);
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void ThrowOnNegativeIndex(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);

        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options)!;
        var dataset = virtualDataset.Dataset;

        var action = () => dataset.RasterBands[-1];
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void ThrowOnLargeIndex(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);

        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options)!;
        var dataset = virtualDataset.Dataset;

        var action = () => dataset.RasterBands[dataset.RasterBands.Count + 1];
        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}
