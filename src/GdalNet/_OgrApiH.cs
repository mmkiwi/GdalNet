// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[CLSCompliant(false)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal static partial class OgrApiH
{
    [LibraryImport("gdal")]
    public static partial int OGR_F_GetFieldCount(OgrFeature feature);

    [LibraryImport("gdal")]
    public static partial int OGR_F_GetFieldAsInteger(OgrFeature fieldDefinition, int index);

    [LibraryImport("gdal")]
    public static partial OgrError OGR_F_SetFID(OgrFeature fieldDefinition, long fid);

    [LibraryImport("gdal")]
    public static partial long OGR_F_GetFID(OgrFeature fieldDefinition);


    [LibraryImport("gdal")]
    public static partial long OGR_F_GetFieldAsInteger64(OgrFeature fieldDefinition, int index);


    [LibraryImport("gdal")]
    public static partial double OGR_F_GetFieldAsDouble(OgrFeature fieldDefinition, int index);


    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string OGR_F_GetFieldAsString(OgrFeature fieldDefinition, int index);


    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string OGR_F_GetFieldAsISO8601DateTime(OgrFeature fieldDefinition, int index);


    [LibraryImport("gdal")]
    [return: MarshalUsing(CountElementName = nameof(count))]
    public static partial int[] OGR_F_GetFieldAsIntegerList(OgrFeature fieldDefinition, int index, out int count);


    [LibraryImport("gdal")]
    [return: MarshalUsing(CountElementName = nameof(count))]
    public static partial long[] OGR_F_GetFieldAsInteger64List(OgrFeature fieldDefinition, int index,
        out int count);


    [LibraryImport("gdal")]
    [return: MarshalUsing(CountElementName = nameof(count))]
    public static partial double[] OGR_F_GetFieldAsDoubleList(OgrFeature fieldDefinition, int index,
        out int count);


    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(CStringArrayMarshal))]
    public static partial string[] OGR_F_GetFieldAsStringList(OgrFeature fieldDefinition, int index);


    [LibraryImport("gdal")]
    [return: MarshalUsing(CountElementName = nameof(count))]
    public static partial byte[] OGR_F_GetFieldAsBinary(OgrFeature fieldDefinition, int index, out int count);

    [LibraryImport("gdal")]
    public static partial OgrFieldDefinition OGR_F_GetFieldDefnRef(OgrFeature feature, int index);

    [LibraryImport("gdal")]
    public static partial OgrFieldType OGR_Fld_GetType(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    public static partial void OGR_Fld_SetType(OgrFieldDefinition fieldDefinition, OgrFieldType subType);

    [LibraryImport("gdal")]
    public static partial OgrFieldSubType OGR_Fld_GetSubType(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    public static partial void OGR_Fld_SetSubType(OgrFieldDefinition fieldDefinition, OgrFieldSubType subType);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void OGR_Fld_SetName(OgrFieldDefinition fieldDefinition, string? name);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string OGR_Fld_GetNameRef(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void OGR_Fld_SetAlternativeName(OgrFieldDefinition fieldDefinition, string? name);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string OGR_Fld_GetAlternativeNameRef(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    public static partial int OGR_Fld_GetWidth(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    public static partial void OGR_Fld_SetWidth(OgrFieldDefinition fieldDefinition, int width);

    [LibraryImport("gdal")]
    public static partial int OGR_Fld_GetPrecision(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    public static partial void OGR_Fld_SetPrecision(OgrFieldDefinition fieldDefinition, int width);

    [LibraryImport("gdal")]
    public static partial OgrJustification OGR_Fld_GetJustify(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    public static partial void OGR_Fld_SetJustify(OgrFieldDefinition fieldDefinition, OgrJustification justification);

    [LibraryImport("gdal")]
    public static partial OgrTimeZoneFlag OGR_Fld_GetTZFlag(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    public static partial void OGR_Fld_SetTZFlag(OgrFieldDefinition fieldDefinition, OgrTimeZoneFlag justification);

    [LibraryImport("gdal")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool OGR_Fld_IsIgnored(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    public static partial void OGR_Fld_SetIgnored(OgrFieldDefinition fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

    [LibraryImport("gdal")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool OGR_Fld_IsNullable(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    public static partial void OGR_Fld_SetNullable(OgrFieldDefinition fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

    [LibraryImport("gdal")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool OGR_Fld_IsUnique(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    public static partial void OGR_Fld_SetUnique(OgrFieldDefinition fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string? OGR_Fld_GetDefault(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void OGR_Fld_SetDefault(OgrFieldDefinition fieldDefinition, string? name);

    [LibraryImport("gdal")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool OGR_Fld_IsDefaultDriverSpecific(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string OGR_Fld_GetDomainName(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void OGR_Fld_SetDomainName(OgrFieldDefinition fieldDefinition, string? name);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string OGR_Fld_GetComment(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void OGR_Fld_SetComment(OgrFieldDefinition fieldDefinition, string? name);

    [LibraryImport("gdal")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool OGR_AreTypeSubTypeCompatible(OgrFieldDefinition fieldDefinition);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string OGR_FldDomain_GetName(OgrFieldDomain domain);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string OGR_FldDomain_GetDescription(OgrFieldDomain domain);
    [LibraryImport("gdal")]
    public static partial OgrFieldDomainType OGR_FldDomain_GetDomainType(OgrFieldDomain domain);

    [LibraryImport("gdal")]
    public static partial OgrFieldType OGR_FldDomain_GetFieldType(OgrFieldDomain domain);

    [LibraryImport("gdal")]
    public static partial OgrFieldSubType OGR_FldDomain_GetFieldSubType(OgrFieldDomain domain);

    [LibraryImport("gdal")]
    public static partial OgrFieldDomainSplitPolicy OGR_FldDomain_GetSplitPolicy(OgrFieldDomain domain);

    [LibraryImport("gdal")]
    public static partial void OGR_FldDomain_SetSplitPolicy(OgrFieldDomain domain, OgrFieldDomainSplitPolicy value);

    [LibraryImport("gdal")]
    public static partial OgrFieldDomainMergePolicy OGR_FldDomain_GetMergePolicy(OgrFieldDomain domain);

    [LibraryImport("gdal")]
    public static partial void OGR_FldDomain_SetMergePolicy(OgrFieldDomain domain, OgrFieldDomainMergePolicy value);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [return:MarshalUsing(typeof(GdalOwnsMarshaller<OgrCodedFieldDomain, OgrCodedFieldDomainHandle>))]
    public static partial OgrCodedFieldDomain OGR_CodedFldDomain_Create(string name, string? description, OgrFieldType fieldType, OgrFieldSubType fieldSubType, nint enumeration);
    
    [LibraryImport("gdal")]
    public static partial OgrError OGR_G_CreateFromWkb(ReadOnlySpan<byte> wkbData, OgrSpatialReference spatialReference, 
        [MarshalUsing(typeof(GdalOwnsMarshaller<OgrGeometry,OgrGeometryHandle>))] out OgrGeometry geometry, int nBytes);
    [LibraryImport("gdal")]
    public unsafe static partial OgrError OGR_G_CreateFromWkt(ref byte* wkbData, OgrSpatialReference spatialReference, 
        [MarshalUsing(typeof(GdalOwnsMarshaller<OgrGeometry,OgrGeometryHandle>))] out OgrGeometry geometry);
    [LibraryImport("gdal")]
    public unsafe static partial OgrError OGR_G_CreateFromFgf(out byte* wkbData, OgrSpatialReference spatialReference, 
        [MarshalUsing(typeof(GdalOwnsMarshaller<OgrGeometry,OgrGeometryHandle>))] out OgrGeometry geometry, int nBytes, out int nBytesConsumed);
    
    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [return:MarshalUsing(typeof(GdalOwnsMarshaller<OgrGeometry, OgrGeometryHandle>))]
    public static partial OgrGeometry OGR_G_CreateFromGML(string gmlData);
    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [return:MarshalUsing(typeof(GdalOwnsMarshaller<OgrGeometry, OgrGeometryHandle>))]
    public static partial OgrGeometry OGR_G_CreateGeometryFromJson(string jsonData);
    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [return:MarshalUsing(typeof(GdalOwnsMarshaller<OgrGeometry, OgrGeometryHandle>))]
    public static partial OgrGeometry OGR_G_CreateGeometryFromEsriJson(string jsonData);
    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [return:MarshalUsing(typeof(GdalOwnsMarshaller<OgrGeometry, OgrGeometryHandle>))]
    public static partial OgrGeometry OGR_G_CreateGeometry(OgrWkbGeometryType geometryType);
    
    [LibraryImport("gdal")]
    public unsafe static partial OgrWkbGeometryType OGR_G_GetGeometryType(OgrGeometry geometry);
    
    [LibraryImport("gdal")]
    public unsafe static partial OgrWkbGeometryType OGR_G_GetGeometryType(OgrGeometryHandle geometry);
    
    [LibraryImport("gdal")]
    public static partial int OGR_G_GetDimension(OgrGeometry geometry);
    [LibraryImport("gdal")]
    public static partial int OGR_G_CoordinateDimension(OgrGeometry geometry);
    [LibraryImport("gdal")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool OGR_G_Is3D(OgrGeometry geometry);
    [LibraryImport("gdal")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool OGR_G_IsMeasured(OgrGeometry geometry);
    [LibraryImport("gdal")]
    public static partial void OGR_G_Set3D(OgrGeometry geometry, [MarshalAs(UnmanagedType.Bool)] bool is3d);
    [LibraryImport("gdal")]
    public static partial void OGR_G_SetMeasured(OgrGeometry geometry, [MarshalAs(UnmanagedType.Bool)] bool isMeasured);
    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(GdalOwnsMarshaller<OgrGeometry,OgrGeometryHandle>))] 
    public static partial OgrGeometry OGR_G_Clone(OgrGeometry geometry);
    [LibraryImport("gdal")]
    public static partial void OGR_G_GetEnvelope(OgrGeometry geometry, [NotNull] out OgrEnvelope? envelope);
    [LibraryImport("gdal")]
    public static partial void OGR_G_GetEnvelope3D(OgrGeometry geometry, [NotNull] ref OgrEnvelope3D? envelope);
    [LibraryImport("gdal")]
    public static partial double OGR_G_GetX(OgrGeometry geometry, int index);
    [LibraryImport("gdal")]
    public static partial double OGR_G_GetY(OgrGeometry geometry, int index);
    [LibraryImport("gdal")]
    public static partial double OGR_G_GetZ(OgrGeometry geometry, int index);
    [LibraryImport("gdal")]
    public static partial double OGR_G_GetM(OgrGeometry geometry, int index);
    [LibraryImport("gdal")]
    public static partial void OGR_G_GetPoint(OgrGeometry geometry, int index, out double x, out double y, out double z);
    [LibraryImport("gdal")]
    public static partial void OGR_G_SetPoint(OgrGeometry geometry, int index, double x, double y, double z);
    [LibraryImport("gdal")]
    public static partial void OGR_G_GetPointZM(OgrGeometry geometry, int index, out double x, out double y, out double z, out double m);
    [LibraryImport("gdal")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool OGR_G_Equals(OgrGeometry geom1, OgrGeometry geom2);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string OGR_L_GetName(OgrLayer layer);

    [LibraryImport("gdal")]
    public static partial long OGR_L_GetFeatureCount(OgrLayer layer, [MarshalAs(UnmanagedType.Bool)] bool force);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(GdalOwnsMarshaller<OgrFeature, OgrFeatureHandle>))]
    public static partial OgrFeature OGR_L_GetNextFeature(OgrLayer layer);

    [LibraryImport("gdal")]
    public static partial void OGR_L_ResetReading(OgrLayer layer);

    [LibraryImport("gdal")]
    public static partial OgrError OGR_L_SetNextByIndex(OgrLayer layer, long index);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(GdalOwnsMarshaller<OgrFeature, OgrFeatureHandle>))]
    public static partial OgrFeature OGR_L_GetFeature(OgrLayer layer, long index);

    [LibraryImport("gdal")]
    public static partial OgrWkbGeometryType OGR_L_GetGeomType(OgrLayer layer);

    [LibraryImport("gdal")]
    public static partial nint OGR_L_GetGeometryTypes(OgrLayer layer, int geomField, int flags, out int entryCount, nint progressFunc, nint progressData);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(GdalDoesntOwnMarshaller<OgrGeometry, OgrGeometryHandle>))]
    public static partial OgrGeometry OGR_L_GetSpatialFilter(OgrLayerHandle layer);

    [LibraryImport("gdal")]
    public static partial void OGR_L_SetSpatialFilter(OgrGeometry geometry);
    [LibraryImport("gdal")]
    public static partial void OGR_L_SetSpatialFilterEx(OgrGeometry geometry);
    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    public static partial OgrError OGR_L_SetAttributeFilter(OgrLayerHandle layer, string? query);
    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    public static partial OgrError OGR_L_SetFeature(OgrFeatureHandle feature);
    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    public static partial OgrError OGR_L_CreateFeature(OgrFeatureHandle feature);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    public static partial OgrError OGR_L_DeleteFeature(OgrLayer layer, long featureId);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    public static partial OgrError OGR_L_UpsertFeature(OgrFeatureHandle feature);

    [LibraryImport("gdal")]
    public static partial OgrError OGR_L_UpdateFeature(OgrLayer layer,
        OgrFeatureHandle feature,
        int updatedFieldsCount,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), In]
        int[] updatedFieldsIndexes,
        int updateGeomFieldCount,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4), In]
        int[] updatedGeomFieldIndexes,
        [MarshalAs(UnmanagedType.Bool)] bool updateStyleString);


    [LibraryImport("gdal")]
    public unsafe static partial void OGR_G_DestroyGeometry(nint geometry);

    [LibraryImport("gdal")]
    public static partial void OGR_F_Destroy(nint feature);
    
    [LibraryImport("gdal")]
    public static partial void OGR_FldDomain_Destroy(nint domain);
}