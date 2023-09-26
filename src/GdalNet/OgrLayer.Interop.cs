// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public partial class OgrLayer : GdalHandle, IConstructibleHandle<OgrLayer>
{
    private OgrLayer(nint pointer) : base(pointer)
    {
        Features = new OgrFeatureCollection(this);
    }

    static OgrLayer IConstructibleHandle<OgrLayer>.Construct(nint pointer, bool ownsHandle)
    {
        ThrowIfOwnsHandle(ownsHandle, nameof(OgrLayer));
        return new(pointer);
    }

    internal static partial class Interop
    {
        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_L_GetName([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(MarshalOwnsHandle<OgrFeature>))]
        public static partial OgrFeature? OGR_L_GetNextFeature([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer);

        [LibraryImport("gdal")]
        public static partial void OGR_L_ResetReading([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer);

        [LibraryImport("gdal")]
        public static partial OgrError OGR_L_SetNextByIndex([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer, long index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(MarshalOwnsHandle<OgrFeature>))]
        public static partial OgrFeature? OGR_L_GetFeature([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer, long index);

        [LibraryImport("gdal")]
        public static partial OgrWkbGeometryType OGR_L_GetGeomType([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer);

        [LibraryImport("gdal")]
        public static partial nint OGR_L_GetGeometryTypes([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer, int geomField, int flags, out int entryCount, nint progressFunc, nint progressData);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(MarshalDoesNotOwnHandle<OgrGeometry>))]
        public static partial OgrGeometry? OGR_L_GetSpatialFilter([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer);

        [LibraryImport("gdal")]
        public static partial void OGR_L_SetSpatialFilter([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer, [MarshalUsing(typeof(MarshalIn<OgrGeometry>))] OgrGeometry? geometry);

        [LibraryImport("gdal")]
        public static partial void OGR_L_SetSpatialFilterEx([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer, int geomField, [MarshalUsing(typeof(MarshalIn<OgrGeometry>))] OgrGeometry? geometry);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial OgrError OGR_L_SetAttributeFilter([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer, string? query);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial OgrError OGR_L_SetFeature([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer, [MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature feature);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial OgrError OGR_L_CreateFeature([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer, [MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature feature);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial OgrError OGR_L_DeleteFeature([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer, long featureId);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial OgrError OGR_L_UpsertFeature([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer, [MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature feature);

        [LibraryImport("gdal")]
        public static partial OgrError OGR_L_UpdateFeature([MarshalUsing(typeof(MarshalIn<OgrLayer>))] OgrLayer layer,
                                                           [MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature feature,
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
