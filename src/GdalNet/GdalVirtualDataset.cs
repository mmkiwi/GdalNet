// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Buffers;

namespace MMKiwi.GdalNet;

public sealed partial class GdalVirtualDataset : GdalSafeHandle
{
    public GdalVirtualDataset(nint pointer, bool ownsHandle) : base(pointer, ownsHandle)
    {
    }

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
}