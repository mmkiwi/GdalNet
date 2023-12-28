// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Buffers;
using System.Reflection.Metadata;

using MMKiwi.CBindingSG;
using MMKiwi.GdalNet.Handles;


namespace MMKiwi.GdalNet;

[CbsgGenerateWrapper]
public sealed partial class GdalVirtualDataset: IDisposable, IConstructableWrapper<GdalVirtualDataset, GdalVirtualDatasetHandle>, IHasHandle<GdalVirtualDatasetHandle>
{
    private bool _disposedValue;

    private MemoryHandle MemoryHandle { get; set; }
    public GdalDataset Dataset { get; private set; } = null!;

    public unsafe static GdalVirtualDataset Open(Memory<byte> buffer,
                                                 GdalOpenSettings? openSettings = null,
                                                 IEnumerable<string>? allowedDrivers = null,
                                                 IReadOnlyDictionary<string, string>? openOptions = null,
                                                 IEnumerable<string>? siblingFiles = null)
    {
        GdalOpenSettings openFlags = openSettings ?? new GdalOpenSettings();

        string fileName = $"/vsimem/datasource_{Guid.NewGuid()}";
        using var pin = buffer.Pin();
        GdalVirtualDataset? virtualDataset = GdalH.VSIFileFromMemBuffer(fileName, (byte*)pin.Pointer, buffer.Length, false);
        
        if (virtualDataset is null)
        {
            GdalError.ThrowIfError();
            throw new InvalidOperationException("Could not create virtual dataset");
        }

        virtualDataset.MemoryHandle.Dispose();
        virtualDataset.MemoryHandle = pin;

        GdalDataset? dataset =
            GdalH.GDALOpenEx(fileName, openFlags.Flags, allowedDrivers, openOptions, siblingFiles);

        virtualDataset.Dataset = dataset!;
        if (dataset is null)
        {
            GdalError.ThrowIfError();
        }
        return virtualDataset;
    }

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Handle.Dispose();
                MemoryHandle.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
    }
}