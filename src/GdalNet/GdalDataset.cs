// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Data;

using Microsoft.Win32.SafeHandles;

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public sealed partial class GdalDataset : GdalMajorObject, IDisposable
{
    public static GdalDataset Open(string fileName,
                                    GdalOpenSettings? openSettings = null,
                                    IEnumerable<string>? allowedDrivers = null,
                                    IReadOnlyDictionary<string, string>? openOptions = null,
                                    IEnumerable<string>? siblingFiles = null)
    {
        GdalOpenSettings openFlags = openSettings ?? new();
        var dataset = Interop.GDALOpenEx(fileName, openFlags.Flags, allowedDrivers, openOptions, siblingFiles);
        GdalError.ThrowIfError();
        return dataset!;
    }

    public void Dispose()
    {
        Handle.Dispose();
    }

    public GdalBandCollection RasterBands { get; }
    public OgrLayerCollection Layers { get; }
    public int RasterXSize => Interop.GDALGetRasterXSize(this);
    public int RasterYSize => Interop.GDALGetRasterYSize(this);
}

internal interface IConstructibleWrapper<TRes, in THandle>
    where THandle : GdalInternalHandle
{
    public static abstract TRes Construct(THandle handle);
}

internal interface IHasHandle<THandle>
{
    public THandle Handle { get; }
}

internal interface IConstructibleHandle<THandle>
    where THandle:GdalInternalHandle
{
    static abstract THandle Construct(bool ownsHandle);
}