// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Reflection;
using System.Runtime.InteropServices;

namespace MMKiwi.GdalNet.GdalIntegrationTests;

public sealed partial class IntegrationFixture : IDisposable
{
    public IntegrationFixture()
    {
        NativeLibrary.SetDllImportResolver(Assembly.GetCallingAssembly(), ResolveDll);
        NativeLibrary.SetDllImportResolver(typeof(GdalDataset).Assembly, ResolveDll);
        try
        {
            GpkgPath = $"{Path.GetRandomFileName()}.gpkg";
            File.WriteAllBytes(GpkgPath, SampleDataResources.PublicDomainGpkg);
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    
    private IntPtr ResolveDll(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
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
                if (NativeLibrary.TryLoad(dllPath, assembly, searchPath, out gdalPtr))
                {
                    return gdalPtr;
                }
                throw new Exception("Could not load GDAL library.");
            }
            if (!SetDllDirectoryW($"{dllPath}"))
                throw new Exception($"{Marshal.GetLastWin32Error()}");
        }

        // Try load gdal
        if (NativeLibrary.TryLoad("gdal", assembly, searchPath, out gdalPtr))
        {
            return gdalPtr;
        }

        // Try load gdald
        if (NativeLibrary.TryLoad("gdald", assembly, searchPath, out gdalPtr))
        {
            return gdalPtr;
        }

        throw new Exception("Could not load GDAL library.");
    }

    private nint gdalPtr;

    
    [LibraryImport("kernel32", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SetDllDirectoryW(string dllDirectory);
    
    public string GpkgPath { get; }

    public void Dispose()
    {
        IList<Exception> exceptions = [];
        if (TryDelete(GpkgPath) is { } ex1)
        {
            exceptions.Add(ex1);
        }
        NativeLibrary.Free(gdalPtr);
        if (exceptions.Any())
            throw (new AggregateException(exceptions.ToArray()));
    }

    private Exception? TryDelete(string path)
    {
        try
        {
            File.Delete(path);
            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}