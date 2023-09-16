// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Data;

using MMKiwi.GdalNet.SampleData;

namespace MMKiwi.GdalNet.UnitTests;

public abstract class DatasetTestBase
{
    public DatasetTestBase()
    {
        GdalInfo.RegisterAllDrivers();
    }

    protected DatasetInfo GetDataset(int index) => TestData.Datasets[index];

    public static IEnumerable<object[]> Datasets => Enumerable.Range(0, TestData.DatasetCount).Select(x => new object[] { x });
}