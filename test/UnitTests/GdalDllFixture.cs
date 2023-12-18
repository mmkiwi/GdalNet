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
        var assembly = Assembly.GetCallingAssembly();

        string envName = $"GDAL_LIBPATH_{(Environment.Is64BitProcess ? "X64" : "X86")}";

        if (Environment.GetEnvironmentVariable(envName) is { } dllPath)
        {
            if (!SetDllDirectoryW($"{dllPath}"))
                throw new Exception($"{Marshal.GetLastWin32Error()}");
        }

        // Try load gdal
        if (NativeLibrary.TryLoad("gdal", assembly, default, out nint try2))
        {
            dllPointer = try2;
            return;
        }

        // Try load gdald
        if (NativeLibrary.TryLoad("gdald", assembly, default, out nint try3))
        {
            dllPointer = try3;
            return;
        }
    }

    private nint dllPointer;


    public void Dispose()
    {

        
        if(dllPointer != 0)
            NativeLibrary.Free(dllPointer);
        

    }

    [LibraryImport("kernel32", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [return:MarshalAs(UnmanagedType.Bool)]
    private static partial bool SetDllDirectoryW(string dllDirectory);
    
    
    
}

[CollectionDefinition("Gdal DLL")]
public class GdalDllCollection : ICollectionFixture<GdalDllFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}