﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Buffers;
using System.Data;

using Microsoft.Win32.SafeHandles;

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public sealed partial class GdalDataset : GdalMajorObject
{
    private GdalDataset(nint pointer, bool ownsHandle):base(pointer,ownsHandle)
    {
        RasterBands = new(this);
        Layers = new(this);
    }

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

    public GdalBandCollection RasterBands { get; }
    public OgrLayerCollection Layers { get; }
    public int RasterXSize => Interop.GDALGetRasterXSize(this);
    public int RasterYSize => Interop.GDALGetRasterYSize(this);
}

public sealed partial class GdalVirtualDataset : GdalSafeHandle, IConstructibleHandle<GdalVirtualDataset>
{

    public MemoryHandle MemoryHandle { get; private set; }
    public GdalDataset Dataset { get; private set; } = null!;

    public unsafe static GdalVirtualDataset Open(Memory<byte> buffer,
                                                 GdalOpenSettings? openSettings = null,
                                                 IEnumerable<string>? allowedDrivers = null,
                                                 IReadOnlyDictionary<string, string>? openOptions = null,
                                                 IEnumerable<string>? siblingFiles = null)
    {
        GdalOpenSettings openFlags = openSettings ?? new();

        string fileName = $"/vsimem/datasource_{Guid.NewGuid()}";
        var pin = buffer.Pin();
        GdalVirtualDataset virtualDataset = Interop.VSIFileFromMemBuffer(fileName, (byte*)pin.Pointer, buffer.Length, false);
        if (virtualDataset is null)
        {
            GdalError.ThrowIfError();
        }

        virtualDataset!.MemoryHandle = pin;

        virtualDataset.Dataset = GdalDataset.Interop.GDALOpenEx(fileName, openFlags.Flags, allowedDrivers, openOptions, siblingFiles)!;
        if (virtualDataset.Dataset is null)
        {
            GdalError.ThrowIfError();
        }
        return virtualDataset;
    }

    protected override bool ReleaseHandle()
    {
        Interop.VSIFCloseL(this);
        return true;

    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            MemoryHandle.Dispose();
            this.Dataset.Dispose();
        }
    }

    public GdalVirtualDataset(nint pointer, bool ownsHandle): base(pointer, ownsHandle)
    {
    }

    static GdalVirtualDataset IConstructibleHandle<GdalVirtualDataset>.Construct(nint pointer, bool ownsHandle) 
        => new(pointer, ownsHandle);
}