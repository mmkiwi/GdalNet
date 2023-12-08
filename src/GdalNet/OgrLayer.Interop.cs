// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;
public partial class OgrLayer
{
    internal static partial class Interop
    {
        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string OGR_L_GetName(OgrLayer.MarshalHandle layer);

        [GdalWrapperMethod]
        public static partial string OGR_L_GetName(OgrLayer layer);

        [LibraryImport("gdal")]
        private static partial OgrFeature.MarshalHandle.Owns OGR_L_GetNextFeature(OgrLayer.MarshalHandle layer);
        [GdalWrapperMethod]
        public static partial OgrFeature OGR_L_GetNextFeature(OgrLayer layer);

        [LibraryImport("gdal")]
        private static partial void OGR_L_ResetReading(OgrLayer.MarshalHandle layer);
        [GdalWrapperMethod]
        public static partial void OGR_L_ResetReading(OgrLayer layer);

        [LibraryImport("gdal")]
        private static partial OgrError OGR_L_SetNextByIndex(OgrLayer.MarshalHandle layer, long index);
        [GdalWrapperMethod]
        private static partial OgrError OGR_L_SetNextByIndex(OgrLayer layer, long index);

        [LibraryImport("gdal")]
        private static partial OgrFeature.MarshalHandle.Owns OGR_L_GetFeature(OgrLayer.MarshalHandle layer, long index);
        [GdalWrapperMethod]
        public static partial OgrFeature? OGR_L_GetFeature(OgrLayer layer, long index);

        [LibraryImport("gdal")]
        private static partial OgrWkbGeometryType OGR_L_GetGeomType(OgrLayer.MarshalHandle layer);
        [GdalWrapperMethod]
        public static partial OgrWkbGeometryType OGR_L_GetGeomType(OgrLayer layer);

        [LibraryImport("gdal")]
        private static partial nint OGR_L_GetGeometryTypes(OgrLayer.MarshalHandle layer, int geomField, int flags, out int entryCount, nint progressFunc, nint progressData);
        [GdalWrapperMethod]
        public static partial nint OGR_L_GetGeometryTypes(OgrLayer layer, int geomField, int flags, out int entryCount, nint progressFunc, nint progressData);

        [LibraryImport("gdal")]
        private static partial OgrGeometry.MarshalHandle.DoesntOwn OGR_L_GetSpatialFilter(OgrLayer.MarshalHandle layer);

        [GdalWrapperMethod]
        public static partial OgrGeometry? OGR_L_GetSpatialFilter(OgrLayer layer);

        [LibraryImport("gdal")]
        private static partial void OGR_L_SetSpatialFilter(OgrGeometry.MarshalHandle geometry);
        [GdalWrapperMethod]
        public static partial void OGR_L_SetSpatialFilter(OgrGeometry? geometry);

        [LibraryImport("gdal")]
        private static partial void OGR_L_SetSpatialFilterEx(OgrGeometry.MarshalHandle geometry);
        [GdalWrapperMethod]
        public static partial void OGR_L_SetSpatialFilterEx(OgrGeometry? geometry);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial OgrError OGR_L_SetAttributeFilter(OgrLayer.MarshalHandle layer, string? query);
        [GdalWrapperMethod]
        public static partial OgrError OGR_L_SetAttributeFilter(OgrLayer layer, string? query);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial OgrError OGR_L_SetFeature(OgrFeature.MarshalHandle feature);
        [GdalWrapperMethod]
        public static partial OgrError OGR_L_SetFeature(OgrFeature feature);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial OgrError OGR_L_CreateFeature(OgrFeature.MarshalHandle feature);
        [GdalWrapperMethod]
        public static partial OgrError OGR_L_CreateFeature(OgrFeature feature);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial OgrError OGR_L_DeleteFeature(OgrLayer.MarshalHandle layer, long featureId);
        [GdalWrapperMethod]
        public static partial OgrError OGR_L_DeleteFeature(OgrLayer layer, long featureId);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial OgrError OGR_L_UpsertFeature(OgrFeature.MarshalHandle feature);
        [GdalWrapperMethod]
        public static partial OgrError OGR_L_UpsertFeature(OgrFeature feature);

        [LibraryImport("gdal")]
        private static partial OgrError OGR_L_UpdateFeature(OgrLayer.MarshalHandle layer,
                                                           OgrFeature.MarshalHandle feature,
                                                           int updatedFieldsCount,
                                                           [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=2), In]
                                                           int[] updatedFieldsIndexes,
                                                           int updateGeomFieldCount,
                                                           [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=4), In]
                                                           int[] updatedGeomFieldIndexes,
                                                           [MarshalAs(UnmanagedType.Bool)]
                                                           bool updateStyleString);

        [GdalWrapperMethod]
        public static partial OgrError OGR_L_UpdateFeature(OgrLayer layer,
                                                   OgrFeature feature,
                                                   int updatedFieldsCount,
                                                   int[] updatedFieldsIndexes,
                                                   int updateGeomFieldCount,
                                                   int[] updatedGeomFieldIndexes,
                                                   bool updateStyleString);
    }
}
