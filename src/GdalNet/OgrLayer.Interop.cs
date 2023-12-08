// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;
public partial class OgrLayer
{
    internal static partial class Interop
    {
        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_L_GetName(OgrLayer layer);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(GdalHandleMarshallerOutOwns<OgrFeature,OgrFeature.MarshalHandle>))]
        public static partial OgrFeature OGR_L_GetNextFeature(OgrLayer layer);

        [LibraryImport("gdal")]
        public static partial void OGR_L_ResetReading(OgrLayer layer);

        [LibraryImport("gdal")]
        public static partial OgrError OGR_L_SetNextByIndex(OgrLayer layer, long index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(GdalHandleMarshallerOutOwns<OgrFeature, OgrFeature.MarshalHandle>))]

        public static partial OgrFeature? OGR_L_GetFeature(OgrLayer layer, long index);

        [LibraryImport("gdal")]
        public static partial OgrWkbGeometryType OGR_L_GetGeomType(OgrLayer layer);

        [LibraryImport("gdal")]
        public static partial nint OGR_L_GetGeometryTypes(OgrLayer layer, int geomField, int flags, out int entryCount, nint progressFunc, nint progressData);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(OgrGeometry.MarshallerOutDoesntOwn))]
        public static partial OgrGeometry? OGR_L_GetSpatialFilter(OgrLayer layer);

        [LibraryImport("gdal")]
        public static partial void OGR_L_SetSpatialFilter(OgrGeometry? geometry);

        [LibraryImport("gdal")]
        public static partial void OGR_L_SetSpatialFilterEx(OgrGeometry? geometry);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial OgrError OGR_L_SetAttributeFilter(OgrLayer layer, string? query);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial OgrError OGR_L_SetFeature(OgrFeature.MarshalHandle feature);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial OgrError OGR_L_CreateFeature(OgrFeature.MarshalHandle feature);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial OgrError OGR_L_DeleteFeature(OgrLayer layer, long featureId);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial OgrError OGR_L_UpsertFeature(OgrFeature.MarshalHandle feature);

        [LibraryImport("gdal")]
        public static partial OgrError OGR_L_UpdateFeature(OgrLayer layer,
                                                           OgrFeature.MarshalHandle feature,
                                                           int updatedFieldsCount,
                                                           [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=2)]
                                                           int[] updatedFieldsIndexes,
                                                           int updateGeomFieldCount,
                                                           [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=4)]
                                                           int[] updatedGeomFieldIndexes,
                                                           [MarshalAs(UnmanagedType.Bool)]
                                                           bool updateStyleString);
    }
}
