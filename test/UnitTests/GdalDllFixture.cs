// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Reflection;
using System.Runtime.InteropServices;

using MMKiwi.GdalNet.UnitTests;

namespace MMKiwi.GdalNet.UnitTests;

public sealed partial class GdalDllFixture : IDisposable
{
    public GdalDllFixture()
    {
        NativeLibrary.SetDllImportResolver(Assembly.GetCallingAssembly(), ResolveDll);
        NativeLibrary.SetDllImportResolver(typeof(GdalDataset).Assembly, ResolveDll);
        NativeLibrary.SetDllImportResolver(typeof(Handles.GdalDatasetHandle).Assembly, ResolveDll);
    }

    private IntPtr ResolveDll(string libraryName, Assembly assembly, DllImportSearchPath? searchpath)
    {
        if (libraryName != "gdal")
            return 0;

        if (gdalPtr != 0)
            return gdalPtr;

        string envName = $"GDAL_LIBPATH_{(Environment.Is64BitProcess ? "X64" : "X86")}";

        if (Environment.GetEnvironmentVariable(envName) is { } dllPath)
        {
            if (File.Exists(dllPath))
            {
                if (NativeLibrary.TryLoad(dllPath, assembly, searchpath, out gdalPtr))
                {
                    return gdalPtr;
                }
                else
                {
                    throw new Exception($"Could not load GDAL library.");
                }
            }
            else if (!SetDllDirectoryW($"{dllPath}"))
                throw new Exception($"{Marshal.GetLastWin32Error()}");
        }

        // Try load gdal
        if (NativeLibrary.TryLoad("gdal", assembly, searchpath, out gdalPtr))
        {
            return gdalPtr;
        }

        // Try load gdald
        if (NativeLibrary.TryLoad("gdald", assembly, searchpath, out gdalPtr))
        {
            return gdalPtr;
        }

        throw new Exception($"Could not load GDAL library.");
    }

    private nint gdalPtr;

    public void Dispose()
    {
        NativeLibrary.Free(gdalPtr);
    }

    [LibraryImport("kernel32", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SetDllDirectoryW(string dllDirectory);
}

[CollectionDefinition("Gdal DLL")]
public class GdalDllCollection : ICollectionFixture<GdalDllFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}