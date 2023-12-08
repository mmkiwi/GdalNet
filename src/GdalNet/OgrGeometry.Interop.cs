// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;

namespace MMKiwi.GdalNet;
public abstract partial class OgrGeometry
{
    internal partial class Interop
    {
        [LibraryImport("gdal")]
        public unsafe static partial OgrError OGR_G_CreateFromWkb(ReadOnlySpan<byte> wkbData, OgrSpatialReference? spatialReference, [MarshalUsing(typeof(OgrGeometry.MarshallerOutOwns))] out OgrGeometry geometry, int nBytes);

        [LibraryImport("gdal")]
        public unsafe static partial OgrError OGR_G_CreateFromWkt(out byte* wkbData, OgrSpatialReference? spatialReference, [MarshalUsing(typeof(OgrGeometry.MarshallerOutOwns))] out OgrGeometry geometry);

        [LibraryImport("gdal")]
        public unsafe static partial OgrError OGR_G_CreateFromFgf(out byte* wkbData, OgrSpatialReference? spatialReference, [MarshalUsing(typeof(OgrGeometry.MarshallerOutOwns))] out OgrGeometry geometry, int nBytes, out int nBytesConsumed);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [return: MarshalUsing(typeof(MarshallerOutOwns))]
        public unsafe static partial OgrGeometry OGR_G_CreateFromGML(string gmlData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [return: MarshalUsing(typeof(MarshallerOutOwns))]
        public unsafe static partial OgrGeometry OGR_G_CreateGeometryFromJson(string jsonData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [return: MarshalUsing(typeof(MarshallerOutOwns))]
        public unsafe static partial OgrGeometry OGR_G_CreateGeometryFromEsriJson(string jsonData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [return: MarshalUsing(typeof(MarshallerOutOwns))]
        public unsafe static partial OgrGeometry OGR_G_CreateGeometry(OgrWkbGeometryType geometryType);

        [LibraryImport("gdal")]
        public unsafe static partial void OGR_G_DestroyGeometry(nint geometry);

        [LibraryImport("gdal")]
        public unsafe static partial OgrWkbGeometryType OGR_G_GetGeometryType(OgrGeometry geometry);

        [LibraryImport("gdal")]
        public unsafe static partial OgrWkbGeometryType OGR_G_GetGeometryType(MarshalHandle geometry);

        [LibraryImport("gdal")]
        public unsafe static partial int OGR_G_GetDimension(OgrGeometry geometry);

        [LibraryImport("gdal")]
        public unsafe static partial int OGR_G_CoordinateDimension(OgrGeometry geometry);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public unsafe static partial bool OGR_G_Is3D(OgrGeometry geometry);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public unsafe static partial bool OGR_G_IsMeasured(OgrGeometry geometry);

        [LibraryImport("gdal")]
        public unsafe static partial void OGR_G_Set3D(OgrGeometry geometry, [MarshalAs(UnmanagedType.Bool)] bool is3d);

        [LibraryImport("gdal")]
        public unsafe static partial void OGR_G_SetMeasured(OgrGeometry geometry, [MarshalAs(UnmanagedType.Bool)] bool isMeasured);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(MarshallerOutOwns))]
        public unsafe static partial OgrGeometry OGR_G_Clone(OgrGeometry geometry);

        [LibraryImport("gdal")]
        public unsafe static partial void OGR_G_GetEnvelope(OgrGeometry geometry, [NotNull] ref OgrEnvelope? envelope);

        [LibraryImport("gdal")]
        public unsafe static partial void OGR_G_GetEnvelope3D(OgrGeometry geometry, [NotNull] ref OgrEnvelope3D? envelope);

    }
}
