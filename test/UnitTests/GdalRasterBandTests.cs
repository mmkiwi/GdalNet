// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet;

namespace MMKiwi.GdalNet.UnitTests;

public sealed class GdalRasterBandTests : IDisposable
{
    public GdalRasterBandTests()
    {
        GdalInfo.RegisterAllDrivers();
        FilePath = Path.GetTempFileName();
        using (FileStream sampleFile = File.OpenWrite(FilePath))
        {
            sampleFile.Write(SampleFiles.Sample_0);
        }
        GdalDataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly)!;
    }

    GdalDataset GdalDataset { get; }

    string FilePath { get; }

    public void Dispose()
    {
        GdalDataset.Dispose();
        File.Delete(FilePath);
    }

    [Fact]
    public void TestRasterIterate()
    {
        foreach (var band in GdalDataset.RasterBands)
            band.DataType.Should().Be(GdalDataType.Byte);
    }
}
