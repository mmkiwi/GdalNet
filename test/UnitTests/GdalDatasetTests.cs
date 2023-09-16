// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet;

namespace MMKiwi.GdalNet.UnitTests;

public sealed class GdalDatasetTests : IDisposable
{
    public GdalDatasetTests()
    {
        GdalInfo.RegisterAllDrivers();
        FilePath = Path.GetTempFileName();
        using FileStream sampleFile = File.OpenWrite(FilePath);
        sampleFile.Write(SampleData.Resources.Sample_0);
    }

    string FilePath { get; }

    public void Dispose()
    {
        File.Delete(FilePath);
    }

    [Fact]
    public void TestOpenDoesNotThrow()
    {
        Action action = () =>
        {
            using var dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly);
        };
        action.Should().NotThrow();
    }

    [Fact]
    public void TestRasterCount()
    {
        using var dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly);
        dataset!.RasterBands.Count.Should().Be(3);
    }

    [Fact]
    public void TestRasterXSize()
    {
        using var dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly);
        dataset!.RasterXSize.Should().Be(1203);
    }

    [Fact]
    public void TestRasterYSize()
    {
        using var dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly);
        dataset!.RasterYSize.Should().Be(1593);
    }
}
