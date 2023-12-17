// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.UnitTests;

//[Collection("GDAL")]
public class GdalInfoTests
{
    [Fact]
    public void CanGetVersion()
    {
        GdalInfo.Version.Should().BeGreaterThanOrEqualTo(new(3, 7, 0));
    }

    [Fact]
    public void CanGetReleaseDate()
    {
        var getReleaseDate = () => GdalInfo.ReleaseDate;
        getReleaseDate.Should().NotThrow();
    }

    [Fact]
    public void ReleaseDateIsValid()
    {
        GdalInfo.ReleaseDate.Should().HaveLength(8).And.Match(s => Int32.Parse(s) > 20230000);
    }

    [Fact]
    public void CanGetBuildInfo()
    {
        string version = GdalInfo.BuildInfo;
        version.Should().NotBeNullOrEmpty();
    }
    /*
    [GeneratedRegex("^(0|[1-9]\\d*)\\.(0|[1-9]\\d*)\\.(0|[1-9]\\d*)(?:-((?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\\.(?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\\+([0-9a-zA-Z-]+(?:\\.[0-9a-zA-Z-]+)*))?$")]
    private partial Regex VersionRegex();

    [GeneratedRegex(@"^\d{8}$")]
    private partial Regex DateRegex();*/
}
