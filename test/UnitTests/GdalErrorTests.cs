// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.UnitTests;

public class GdalErrorTests
{
    [Fact]
    public void ErrorIsRetrievable()
    {
        GdalInfo.RegisterAllDrivers();
        using var dataset = GdalDataset.Open("DOESNOTEXIST", GdalAccess.ReadOnly);
        GdalError.LastError.Should().BeEquivalentTo(new
        {
            ErrorNum = 4,
            Severity = GdalCplErr.Failure
        });

    }
}
