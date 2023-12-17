// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;
public abstract partial class OgrGeometry
{
    internal partial class Interop
    {
        [LibraryImport("gdal")]
        private static partial OgrError OGR_G_CreateFromWkb(ReadOnlySpan<byte> wkbData, OgrSpatialReferenceHandle spatialReference, out OgrGeometryHandle.Owns geometry, int nBytes);
        [GdalWrapperMethod]
        public static partial OgrError OGR_G_CreateFromWkb(ReadOnlySpan<byte> wkbData, OgrSpatialReference? spatialReference, out OgrGeometry geometry, int nBytes);

        [LibraryImport("gdal")]
        private unsafe static partial OgrError OGR_G_CreateFromWkt(ref byte* wkbData, OgrSpatialReferenceHandle spatialReference, out OgrGeometryHandle.Owns geometry);
        [GdalWrapperMethod]
        public unsafe static partial OgrError OGR_G_CreateFromWkt(ref byte* wkbData, OgrSpatialReference? spatialReference, out OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial OgrError OGR_G_CreateFromFgf(out byte* wkbData, OgrSpatialReferenceHandle spatialReference, out OgrGeometryHandle.Owns geometry, int nBytes, out int nBytesConsumed);
        [GdalWrapperMethod]
        public unsafe static partial OgrError OGR_G_CreateFromFgf(out byte* wkbData, OgrSpatialReference? spatialReference, out OgrGeometry geometry, int nBytes, out int nBytesConsumed);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(OGR_G_CreateFromGML))]
        private unsafe static partial OgrGeometryHandle.Owns _OGR_G_CreateFromGML(string gmlData);
        [GdalWrapperMethod(MethodName = nameof(_OGR_G_CreateFromGML))]
        public static partial OgrGeometry OGR_G_CreateFromGML(string gmlData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(OGR_G_CreateGeometryFromJson))]
        private unsafe static partial OgrGeometryHandle.Owns _OGR_G_CreateGeometryFromJson(string jsonData);
        [GdalWrapperMethod(MethodName = nameof(_OGR_G_CreateGeometryFromJson))]
        public static partial OgrGeometry OGR_G_CreateGeometryFromJson(string jsonData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(OGR_G_CreateGeometryFromEsriJson))]
        private unsafe static partial OgrGeometryHandle.Owns _OGR_G_CreateGeometryFromEsriJson(string jsonData);
        [GdalWrapperMethod(MethodName = nameof(_OGR_G_CreateGeometryFromEsriJson))]
        public static partial OgrGeometry OGR_G_CreateGeometryFromEsriJson(string jsonData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(OGR_G_CreateGeometry))]
        private unsafe static partial OgrGeometryHandle.Owns _OGR_G_CreateGeometry(OgrWkbGeometryType geometryType);
        [GdalWrapperMethod(MethodName = nameof(_OGR_G_CreateGeometry))]
        public static partial OgrGeometry OGR_G_CreateGeometry(OgrWkbGeometryType geometryType);

        [LibraryImport("gdal")]
        public unsafe static partial OgrWkbGeometryType OGR_G_GetGeometryType(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public unsafe static partial OgrWkbGeometryType OGR_G_GetGeometryType(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial int OGR_G_GetDimension(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public unsafe static partial int OGR_G_GetDimension(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial int OGR_G_CoordinateDimension(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public unsafe static partial int OGR_G_CoordinateDimension(OgrGeometry geometry);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private unsafe static partial bool OGR_G_Is3D(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public unsafe static partial bool OGR_G_Is3D(OgrGeometry geometry);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private unsafe static partial bool OGR_G_IsMeasured(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public unsafe static partial bool OGR_G_IsMeasured(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_Set3D(OgrGeometryHandle geometry, [MarshalAs(UnmanagedType.Bool)] bool is3d);
        [GdalWrapperMethod]
        public unsafe static partial void OGR_G_Set3D(OgrGeometry geometry, bool is3d);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_SetMeasured(OgrGeometryHandle geometry, [MarshalAs(UnmanagedType.Bool)] bool isMeasured);
        [GdalWrapperMethod]
        public unsafe static partial void OGR_G_SetMeasured(OgrGeometry geometry, bool isMeasured);

        [LibraryImport("gdal")]
        private unsafe static partial OgrGeometryHandle.Owns OGR_G_Clone(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public unsafe static partial OgrGeometry OGR_G_Clone(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_GetEnvelope(OgrGeometryHandle geometry, [NotNull] ref OgrEnvelope? envelope);
        [GdalWrapperMethod]
        public unsafe static partial void OGR_G_GetEnvelope(OgrGeometry geometry, [NotNull] ref OgrEnvelope? envelope);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_GetEnvelope3D(OgrGeometryHandle geometry, [NotNull] ref OgrEnvelope3D? envelope);
        [GdalWrapperMethod]
        public unsafe static partial void OGR_G_GetEnvelope3D(OgrGeometry geometry, [NotNull] ref OgrEnvelope3D? envelope);

    }
}
