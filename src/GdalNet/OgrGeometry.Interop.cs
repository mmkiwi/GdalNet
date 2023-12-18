// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;
public abstract partial class OgrGeometry
{
    internal static partial class Interop
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
        public static partial OgrWkbGeometryType OGR_G_GetGeometryType(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial int OGR_G_GetDimension(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public static partial int OGR_G_GetDimension(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial int OGR_G_CoordinateDimension(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public static partial int OGR_G_CoordinateDimension(OgrGeometry geometry);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private unsafe static partial bool OGR_G_Is3D(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public static partial bool OGR_G_Is3D(OgrGeometry geometry);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private unsafe static partial bool OGR_G_IsMeasured(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public static partial bool OGR_G_IsMeasured(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_Set3D(OgrGeometryHandle geometry, [MarshalAs(UnmanagedType.Bool)] bool is3d);
        [GdalWrapperMethod]
        public static partial void OGR_G_Set3D(OgrGeometry geometry, bool is3d);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_SetMeasured(OgrGeometryHandle geometry, [MarshalAs(UnmanagedType.Bool)] bool isMeasured);
        [GdalWrapperMethod]
        public static partial void OGR_G_SetMeasured(OgrGeometry geometry, bool isMeasured);

        [LibraryImport("gdal")]
        private unsafe static partial OgrGeometryHandle.Owns OGR_G_Clone(OgrGeometryHandle geometry);
        [GdalWrapperMethod]
        public static partial OgrGeometry OGR_G_Clone(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_GetEnvelope(OgrGeometryHandle geometry, [NotNull] out OgrEnvelope? envelope);
        [GdalWrapperMethod]
        public static partial void OGR_G_GetEnvelope(OgrGeometry geometry, [NotNull] out OgrEnvelope? envelope);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_GetEnvelope3D(OgrGeometryHandle geometry, [NotNull] ref OgrEnvelope3D? envelope);
        [GdalWrapperMethod]
        public static partial void OGR_G_GetEnvelope3D(OgrGeometry geometry, [NotNull] ref OgrEnvelope3D? envelope);
        
        [LibraryImport("gdal")]
        private unsafe static partial double OGR_G_GetX(OgrGeometryHandle geometry, int index);
        [GdalWrapperMethod]
        public static partial double OGR_G_GetX(OgrGeometry geometry, int index);
        
        [LibraryImport("gdal")]
        private unsafe static partial double OGR_G_GetY(OgrGeometryHandle geometry, int index);
        [GdalWrapperMethod]
        public static partial double OGR_G_GetY(OgrGeometry geometry, int index);
        
        [LibraryImport("gdal")]
        private unsafe static partial double OGR_G_GetZ(OgrGeometryHandle geometry, int index);
        [GdalWrapperMethod]
        public static partial double OGR_G_GetZ(OgrGeometry geometry, int index);
        
        [LibraryImport("gdal")]
        private unsafe static partial double OGR_G_GetM(OgrGeometryHandle geometry, int index);
        [GdalWrapperMethod]
        public static partial double OGR_G_GetM(OgrGeometry geometry, int index);
        
        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_GetPoint(OgrGeometryHandle geometry, int index, out double x, out double y, out double z);
        [GdalWrapperMethod]
        public static partial void OGR_G_GetPoint(OgrGeometry geometry, int index, out double x, out double y, out double z);
        
        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_SetPoint(OgrGeometryHandle geometry, int index, double x, double y, double z);
        [GdalWrapperMethod]
        public static partial void OGR_G_SetPoint(OgrGeometry geometry, int index, double x, double y, double z);
        
        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_GetPointZM(OgrGeometryHandle geometry, int index, out double x, out double y, out double z, out double m);
        [GdalWrapperMethod]
        public static partial void OGR_G_GetPointZM(OgrGeometry geometry, int index, out double x, out double y, out double z, out double m);

        [LibraryImport("gdal")]
        [return:MarshalAs(UnmanagedType.Bool)]
        private unsafe static partial bool OGR_G_Equals(OgrGeometryHandle geom1, OgrGeometryHandle geom2);
        [GdalWrapperMethod]
        public static partial bool OGR_G_Equals(OgrGeometry geom1, OgrGeometry geom2);
    }
}
