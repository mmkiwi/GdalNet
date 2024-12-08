// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public record GdalOpenSettings
{
    public GdalAccess Access { get; init; } = GdalAccess.ReadOnly;
    public bool RasterDrivers { get; init; } = true;
    public bool VectorDrivers { get; init; } = true;
    public bool GnmDrivers { get; init; } = true;
    public bool MultidimensionalRasterDrivers { get; init; } = false;

    public bool SharedAccess { get; init; } = true;
    public bool VerboseError { get; init; } = true;

    internal GdalOpenFlags Flags
        =>
            (GdalOpenFlags)Access |
            (RasterDrivers ? GdalOpenFlags.Raster : GdalOpenFlags.None) |
            (VectorDrivers ? GdalOpenFlags.Vector : GdalOpenFlags.None) |
            (GnmDrivers ? GdalOpenFlags.Gnm : GdalOpenFlags.None) |
            (MultidimensionalRasterDrivers ? GdalOpenFlags.MultidimensionalRaster : GdalOpenFlags.None) |
            (SharedAccess ? GdalOpenFlags.Shared : GdalOpenFlags.None) |
            (VerboseError ? GdalOpenFlags.VerboseError : GdalOpenFlags.None);
}