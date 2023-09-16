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
        using var dataset = GdalDataset.Interop.GDALOpenEx("DOESNOTEXIST", new GdalOpenSettings().Flags, null, null, null);
        GdalError.LastError.Should().BeEquivalentTo(new
        {
            ErrorNum = GdalError.ErrorCodes.OpenFailed,
            Severity = GdalCplErr.Failure
        });

    }
}
