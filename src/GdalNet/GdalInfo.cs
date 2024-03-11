// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Error;

namespace MMKiwi.GdalNet;

public static class GdalInfo
{
    private static readonly Lazy<Version> s_version = new(() =>
    {
        var version = GdalH.GDALVersionInfo("RELEASE_NAME"u8);
        GdalError.ThrowIfError();
        return Version.Parse(version);
    });
    public static Version Version => s_version.Value;

    private static readonly Lazy<string> s_releaseDate = new(() =>
    {
        string v = GdalH.GDALVersionInfo("RELEASE_DATE"u8);
        GdalError.ThrowIfError();
        return v;
    });
    public static string ReleaseDate => s_releaseDate.Value;

    private static readonly Lazy<string> s_buildInfo = new(() =>
    {
        string v = GdalH.GDALVersionInfo("BUILD_INFO"u8);
        GdalError.ThrowIfError();
        return v;
    });
    private static bool s_isRegistered;
    private static readonly object s_reentrantLock = new();

    public static string BuildInfo => s_buildInfo.Value;

    public static void RegisterAllDrivers()
    {
        lock (s_reentrantLock)
        {
            if (s_isRegistered)
            {
                return;
            }
            GdalH.GDALAllRegister();
            GdalError.ThrowIfError();
            // GDALAllRegister is not re-entrant safe
            s_isRegistered = true;
        }
    }
}
