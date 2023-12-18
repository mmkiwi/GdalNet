// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.SampleData;

namespace MMKiwi.GdalNet.UnitTests;

[Collection("Gdal DLL")]
public abstract class DatasetTestBase
{
    protected DatasetTestBase(GdalDllFixture fixture)
    {
        _ = fixture;
        GdalInfo.RegisterAllDrivers();
    }

    protected static DatasetInfo GetDataset(int index) => TestData.Datasets[index];

    public static IEnumerable<object[]> Datasets => Enumerable.Range(0, TestData.DatasetCount).Select(x => new object[] { x });
}