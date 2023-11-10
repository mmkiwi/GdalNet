// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;
public abstract partial class OgrGeometry
{
    internal partial class Interop
    {
        [LibraryImport("gdal")]
        public unsafe static partial OgrError OGR_G_CreateFromWkb(ReadOnlySpan<byte> wkbData, OgrSpatialReference? spatialReference, out OgrGeometry geometry, int nBytes);

        [LibraryImport("gdal")]
        public unsafe static partial OgrError OGR_G_CreateFromWkt(out byte* wkbData, OgrSpatialReference? spatialReference, out OgrGeometry geometry);

        [LibraryImport("gdal")]
        public unsafe static partial OgrError OGR_G_CreateFromFgf(out byte* wkbData, OgrSpatialReference? spatialReference, out OgrGeometry geometry, int nBytes, out int nBytesConsumed);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public unsafe static partial OgrGeometry OGR_G_CreateFromGML(string gmlData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public unsafe static partial OgrGeometry OGR_G_CreateGeometryFromJson(string jsonData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public unsafe static partial OgrGeometry OGR_G_CreateGeometryFromEsriJson(string jsonData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public unsafe static partial OgrGeometry OGR_G_CreateGeometry(OgrWkbGeometryType geometryType);

        [LibraryImport("gdal")]
        public unsafe static partial void OGR_G_DestroyGeometry(OgrGeometry geometry);

        [LibraryImport("gdal")]
        public unsafe static partial OgrWkbGeometryType OGR_G_GetGeometryType(OgrGeometry geometry);

        [LibraryImport("gdal")]
        public unsafe static partial OgrWkbGeometryType OGR_G_GetGeometryType(nint geometry);
    }
}
