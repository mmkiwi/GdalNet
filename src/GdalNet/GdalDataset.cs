// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.Win32.SafeHandles;

namespace MMKiwi.GdalNet;

public sealed partial class GdalDataset: GdalMajorObject
{
    private GdalDataset()
    {
        OwnsHandle = true;
        RasterBands = new(this);
    }

    public static GdalDataset? Open(string fileName, GdalAccess access)
    {
        return Interop.GDALOpen(fileName, access);
    }

    public GdalBandCollection RasterBands { get; }
    public int RasterXSize => Interop.GDALGetRasterXSize(this);
    public int RasterYSize => Interop.GDALGetRasterYSize(this);
}

