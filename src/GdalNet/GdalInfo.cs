// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public static partial class GdalInfo
{
    private static readonly Lazy<Version> s_version = new(() =>
    {
        var version = Interop.GDALVersionInfo("RELEASE_NAME"u8);
        return Version.Parse(version);
    });
    public static Version Version => s_version.Value;

    private static readonly Lazy<DateOnly> s_releaseDate = new(() =>
        {
            var date = Interop.GDALVersionInfo("RELEASE_DATE"u8);
            return DateOnly.ParseExact(date, "yyyyMMdd");
        });
    public static DateOnly ReleaseDate => s_releaseDate.Value;

    private static readonly Lazy<string> s_buildInfo = new(() => Interop.GDALVersionInfo("BUILD_INFO"u8));
    private static bool s_isRegistered;
    private static readonly object s_reentrant_lock = new();

    public static string BuildInfo => s_buildInfo.Value;

    public static void RegisterAllDrivers()
    {
        lock (s_reentrant_lock)
        {
            if (!s_isRegistered)
            {
                Interop.GDALAllRegister();
                // GDALAllRegister is not re-entrant safe
                s_isRegistered = true;
            }
        }
    }
}
