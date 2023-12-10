// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public sealed partial class GdalDataset : GdalMajorObject
{
    private GdalDataset(MarshalHandle handle) : base(handle)
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