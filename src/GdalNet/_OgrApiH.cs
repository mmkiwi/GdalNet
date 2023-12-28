// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.CBindingSG;
using MMKiwi.GdalNet.Handles;

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[CLSCompliant(false)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal static partial class OgrApiH
{
    [LibraryImport("gdal")]
    private static partial int OGR_F_GetFieldCount(OgrFeatureHandle feature);

    [CbsgWrapperMethod]
    public static partial int OGR_F_GetFieldCount(OgrFeature feature);

    [LibraryImport("gdal")]
    private static partial int OGR_F_GetFieldAsInteger(OgrFeatureHandle fieldDefinition, int index);

    [CbsgWrapperMethod]
    public static partial int OGR_F_GetFieldAsInteger(OgrFeature fieldDefinition, int index);

    [LibraryImport("gdal")]
    private static partial long OGR_F_GetFieldAsInteger64(OgrFeatureHandle fieldDefinition, int index);

    [CbsgWrapperMethod]
    public static partial long OGR_F_GetFieldAsInteger64(OgrFeature fieldDefinition, int index);

    [LibraryImport("gdal")]
    private static partial double OGR_F_GetFieldAsDouble(OgrFeatureHandle fieldDefinition, int index);

    [CbsgWrapperMethod]
    public static partial double OGR_F_GetFieldAsDouble(OgrFeature fieldDefinition, int index);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    private static partial string OGR_F_GetFieldAsString(OgrFeatureHandle fieldDefinition, int index);

    [CbsgWrapperMethod]
    public static partial string OGR_F_GetFieldAsString(OgrFeature fieldDefinition, int index);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    private static partial string OGR_F_GetFieldAsISO8601DateTime(OgrFeatureHandle fieldDefinition, int index);

    [CbsgWrapperMethod]
    public static partial string OGR_F_GetFieldAsISO8601DateTime(OgrFeature fieldDefinition, int index);

    [LibraryImport("gdal")]
    [return: MarshalUsing(CountElementName = nameof(count))]
    private static partial int[]  OGR_F_GetFieldAsIntegerList(OgrFeatureHandle fieldDefinition, int index, out int count);

    [CbsgWrapperMethod]
    public static partial int[] OGR_F_GetFieldAsIntegerList(OgrFeature fieldDefinition, int index, out int count);

    [LibraryImport("gdal")]
    [return: MarshalUsing(CountElementName = nameof(count))]
    private static partial long[] OGR_F_GetFieldAsInteger64List(OgrFeatureHandle fieldDefinition, int index,
        out int count);

    [CbsgWrapperMethod]
    public static partial long[] OGR_F_GetFieldAsInteger64List(OgrFeature fieldDefinition, int index, out int count);

    [LibraryImport("gdal")]
    [return: MarshalUsing(CountElementName = nameof(count))]
    private static partial double[] OGR_F_GetFieldAsDoubleList(OgrFeatureHandle fieldDefinition, int index,
        out int count);

    [CbsgWrapperMethod]
    public static partial double[] OGR_F_GetFieldAsDoubleList(OgrFeature fieldDefinition, int index, out int count);

    [LibraryImport("gdal")]
    [return: MarshalUsing(typeof(CStringArrayMarshal))]
    private static partial string[] OGR_F_GetFieldAsStringList(OgrFeatureHandle fieldDefinition, int index);

    [CbsgWrapperMethod]
    public static partial string[] OGR_F_GetFieldAsStringList(OgrFeature fieldDefinition, int index);

    [LibraryImport("gdal")]
    [return: MarshalUsing(CountElementName = nameof(count))]
    private static partial byte[] OGR_F_GetFieldAsBinary(OgrFeatureHandle fieldDefinition, int index, out int count);

    [CbsgWrapperMethod]
    public static partial byte[] OGR_F_GetFieldAsBinary(OgrFeature fieldDefinition, int index, out int count);

    [LibraryImport("gdal")]
    private static partial OgrFieldDefinitionHandle OGR_F_GetFieldDefnRef(OgrFeatureHandle feature, int index);

    [CbsgWrapperMethod]
    public static partial OgrFieldDefinition OGR_F_GetFieldDefnRef(OgrFeature feature, int index);
    
     [LibraryImport("gdal")]
        public static partial OgrFieldType OGR_Fld_GetType(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetType(OgrFieldDefinitionHandle fieldDefinition, OgrFieldType subType);

        [LibraryImport("gdal")]
        public static partial OgrFieldSubType OGR_Fld_GetSubType(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetSubType(OgrFieldDefinitionHandle fieldDefinition, OgrFieldSubType subType);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetName(OgrFieldDefinitionHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetNameRef(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetAlternativeName(OgrFieldDefinitionHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetAlternativeNameRef(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial int OGR_Fld_GetWidth(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetWidth(OgrFieldDefinitionHandle fieldDefinition, int width);

        [LibraryImport("gdal")]
        public static partial int OGR_Fld_GetPrecision(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetPrecision(OgrFieldDefinitionHandle fieldDefinition, int width);

        [LibraryImport("gdal")]
        public static partial OgrJustification OGR_Fld_GetJustify(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetJustify(OgrFieldDefinitionHandle fieldDefinition, OgrJustification justification);

        [LibraryImport("gdal")]
        public static partial OgrTimeZoneFlag OGR_Fld_GetTZFlag(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetTZFlag(OgrFieldDefinitionHandle fieldDefinition, OgrTimeZoneFlag justification);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsIgnored(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetIgnored(OgrFieldDefinitionHandle fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsNullable(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetNullable(OgrFieldDefinitionHandle fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsUnique(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetUnique(OgrFieldDefinitionHandle fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string? OGR_Fld_GetDefault(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetDefault(OgrFieldDefinitionHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsDefaultDriverSpecific(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetDomainName(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetDomainName(OgrFieldDefinitionHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetComment(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetComment(OgrFieldDefinitionHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_AreTypeSubTypeCompatible(OgrFieldDefinitionHandle fieldDefinition);
        
        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string OGR_FldDomain_GetName(OgrFieldDomainHandle domain);
        [CbsgWrapperMethod]
        public static partial string OGR_FldDomain_GetName(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string OGR_FldDomain_GetDescription(OgrFieldDomainHandle domain);
        [CbsgWrapperMethod]
        public static partial string OGR_FldDomain_GetDescription(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial OgrFieldDomainType OGR_FldDomain_GetDomainType(OgrFieldDomainHandle domain);
        [CbsgWrapperMethod]
        public static partial OgrFieldDomainType OGR_FldDomain_GetDomainType(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial OgrFieldType OGR_FldDomain_GetFieldType(OgrFieldDomainHandle domain);
        [CbsgWrapperMethod]
        public static partial OgrFieldType OGR_FldDomain_GetFieldType(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial OgrFieldSubType OGR_FldDomain_GetFieldSubType(OgrFieldDomainHandle domain);
        [CbsgWrapperMethod]
        public static partial OgrFieldSubType OGR_FldDomain_GetFieldSubType(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial OgrFieldDomainSplitPolicy OGR_FldDomain_GetSplitPolicy(OgrFieldDomainHandle domain);
        [CbsgWrapperMethod]
        public static partial OgrFieldDomainSplitPolicy OGR_FldDomain_GetSplitPolicy(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial void OGR_FldDomain_SetSplitPolicy(OgrFieldDomainHandle domain, OgrFieldDomainSplitPolicy value);
        [CbsgWrapperMethod]
        public static partial void OGR_FldDomain_SetSplitPolicy(OgrFieldDomain domain, OgrFieldDomainSplitPolicy value);

        [LibraryImport("gdal")]
        private static partial OgrFieldDomainMergePolicy OGR_FldDomain_GetMergePolicy(OgrFieldDomainHandle domain);
        [CbsgWrapperMethod]
        public static partial OgrFieldDomainMergePolicy OGR_FldDomain_GetMergePolicy(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial void OGR_FldDomain_SetMergePolicy(OgrFieldDomainHandle domain, OgrFieldDomainMergePolicy value);
        [CbsgWrapperMethod]
        public static partial void OGR_FldDomain_SetMergePolicy(OgrFieldDomain domain, OgrFieldDomainMergePolicy value);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8,EntryPoint = nameof(OGR_CodedFldDomain_Create))]
        private static partial OgrCodedFieldDomainHandle.Owns _OGR_CodedFldDomain_Create(string name, string? description, OgrFieldType fieldType, OgrFieldSubType fieldSubType, nint enumeration);
        [CbsgWrapperMethod(MethodName = nameof(_OGR_CodedFldDomain_Create))]
        public static partial OgrCodedFieldDomainHandle.Owns OGR_CodedFldDomain_Create(string name, string? description, OgrFieldType fieldType, OgrFieldSubType fieldSubType, nint enumeration);
        
        
        [LibraryImport("gdal")]
        private static partial OgrError OGR_G_CreateFromWkb(ReadOnlySpan<byte> wkbData, OgrSpatialReferenceHandle spatialReference, out OgrGeometryHandle.Owns geometry, int nBytes);
        
        [CbsgWrapperMethod]
        public static partial OgrError OGR_G_CreateFromWkb(ReadOnlySpan<byte> wkbData, OgrSpatialReference? spatialReference, out OgrGeometry geometry, int nBytes);

        [LibraryImport("gdal")]
        private unsafe static partial OgrError OGR_G_CreateFromWkt(ref byte* wkbData, OgrSpatialReferenceHandle spatialReference, out OgrGeometryHandle.Owns geometry);
        [CbsgWrapperMethod]
        public unsafe static partial OgrError OGR_G_CreateFromWkt(ref byte* wkbData, OgrSpatialReference? spatialReference, out OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial OgrError OGR_G_CreateFromFgf(out byte* wkbData, OgrSpatialReferenceHandle spatialReference, out OgrGeometryHandle.Owns geometry, int nBytes, out int nBytesConsumed);
        [CbsgWrapperMethod]
        public unsafe static partial OgrError OGR_G_CreateFromFgf(out byte* wkbData, OgrSpatialReference? spatialReference, out OgrGeometry geometry, int nBytes, out int nBytesConsumed);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(OGR_G_CreateFromGML))]
        private unsafe static partial OgrGeometryHandle.Owns _OGR_G_CreateFromGML(string gmlData);
        [CbsgWrapperMethod(MethodName = nameof(_OGR_G_CreateFromGML))]
        public static partial OgrGeometry OGR_G_CreateFromGML(string gmlData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(OGR_G_CreateGeometryFromJson))]
        private unsafe static partial OgrGeometryHandle.Owns _OGR_G_CreateGeometryFromJson(string jsonData);
        [CbsgWrapperMethod(MethodName = nameof(_OGR_G_CreateGeometryFromJson))]
        public static partial OgrGeometry OGR_G_CreateGeometryFromJson(string jsonData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(OGR_G_CreateGeometryFromEsriJson))]
        private unsafe static partial OgrGeometryHandle.Owns _OGR_G_CreateGeometryFromEsriJson(string jsonData);
        [CbsgWrapperMethod(MethodName = nameof(_OGR_G_CreateGeometryFromEsriJson))]
        public static partial OgrGeometry OGR_G_CreateGeometryFromEsriJson(string jsonData);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(OGR_G_CreateGeometry))]
        private unsafe static partial OgrGeometryHandle.Owns _OGR_G_CreateGeometry(OgrWkbGeometryType geometryType);
        [CbsgWrapperMethod(MethodName = nameof(_OGR_G_CreateGeometry))]
        public static partial OgrGeometry OGR_G_CreateGeometry(OgrWkbGeometryType geometryType);

        [LibraryImport("gdal")]
        public unsafe static partial OgrWkbGeometryType OGR_G_GetGeometryType(OgrGeometryHandle geometry);
        [CbsgWrapperMethod]
        public static partial OgrWkbGeometryType OGR_G_GetGeometryType(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial int OGR_G_GetDimension(OgrGeometryHandle geometry);
        [CbsgWrapperMethod]
        public static partial int OGR_G_GetDimension(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial int OGR_G_CoordinateDimension(OgrGeometryHandle geometry);
        [CbsgWrapperMethod]
        public static partial int OGR_G_CoordinateDimension(OgrGeometry geometry);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private unsafe static partial bool OGR_G_Is3D(OgrGeometryHandle geometry);
        [CbsgWrapperMethod]
        public static partial bool OGR_G_Is3D(OgrGeometry geometry);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private unsafe static partial bool OGR_G_IsMeasured(OgrGeometryHandle geometry);
        [CbsgWrapperMethod]
        public static partial bool OGR_G_IsMeasured(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_Set3D(OgrGeometryHandle geometry, [MarshalAs(UnmanagedType.Bool)] bool is3d);
        [CbsgWrapperMethod]
        public static partial void OGR_G_Set3D(OgrGeometry geometry, bool is3d);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_SetMeasured(OgrGeometryHandle geometry, [MarshalAs(UnmanagedType.Bool)] bool isMeasured);
        [CbsgWrapperMethod]
        public static partial void OGR_G_SetMeasured(OgrGeometry geometry, bool isMeasured);

        [LibraryImport("gdal")]
        private unsafe static partial OgrGeometryHandle.Owns OGR_G_Clone(OgrGeometryHandle geometry);
        [CbsgWrapperMethod]
        public static partial OgrGeometry OGR_G_Clone(OgrGeometry geometry);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_GetEnvelope(OgrGeometryHandle geometry, [NotNull] out OgrEnvelope? envelope);
        [CbsgWrapperMethod]
        public static partial void OGR_G_GetEnvelope(OgrGeometry geometry, [NotNull] out OgrEnvelope? envelope);

        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_GetEnvelope3D(OgrGeometryHandle geometry, [NotNull] ref OgrEnvelope3D? envelope);
        [CbsgWrapperMethod]
        public static partial void OGR_G_GetEnvelope3D(OgrGeometry geometry, [NotNull] ref OgrEnvelope3D? envelope);
        
        [LibraryImport("gdal")]
        private unsafe static partial double OGR_G_GetX(OgrGeometryHandle geometry, int index);
        [CbsgWrapperMethod]
        public static partial double OGR_G_GetX(OgrGeometry geometry, int index);
        
        [LibraryImport("gdal")]
        private unsafe static partial double OGR_G_GetY(OgrGeometryHandle geometry, int index);
        [CbsgWrapperMethod]
        public static partial double OGR_G_GetY(OgrGeometry geometry, int index);
        
        [LibraryImport("gdal")]
        private unsafe static partial double OGR_G_GetZ(OgrGeometryHandle geometry, int index);
        [CbsgWrapperMethod]
        public static partial double OGR_G_GetZ(OgrGeometry geometry, int index);
        
        [LibraryImport("gdal")]
        private unsafe static partial double OGR_G_GetM(OgrGeometryHandle geometry, int index);
        [CbsgWrapperMethod]
        public static partial double OGR_G_GetM(OgrGeometry geometry, int index);
        
        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_GetPoint(OgrGeometryHandle geometry, int index, out double x, out double y, out double z);
        [CbsgWrapperMethod]
        public static partial void OGR_G_GetPoint(OgrGeometry geometry, int index, out double x, out double y, out double z);
        
        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_SetPoint(OgrGeometryHandle geometry, int index, double x, double y, double z);
        [CbsgWrapperMethod]
        public static partial void OGR_G_SetPoint(OgrGeometry geometry, int index, double x, double y, double z);
        
        [LibraryImport("gdal")]
        private unsafe static partial void OGR_G_GetPointZM(OgrGeometryHandle geometry, int index, out double x, out double y, out double z, out double m);
        [CbsgWrapperMethod]
        public static partial void OGR_G_GetPointZM(OgrGeometry geometry, int index, out double x, out double y, out double z, out double m);

        [LibraryImport("gdal")]
        [return:MarshalAs(UnmanagedType.Bool)]
        private unsafe static partial bool OGR_G_Equals(OgrGeometryHandle geom1, OgrGeometryHandle geom2);
        [CbsgWrapperMethod]
        public static partial bool OGR_G_Equals(OgrGeometry geom1, OgrGeometry geom2);
        
        
        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string OGR_L_GetName(OgrLayerHandle layer);

        [CbsgWrapperMethod]
        public static partial string OGR_L_GetName(OgrLayer layer);

        [LibraryImport("gdal")]
        private static partial OgrFeatureHandle.Owns OGR_L_GetNextFeature(OgrLayerHandle layer);
        [CbsgWrapperMethod]
        public static partial OgrFeature? OGR_L_GetNextFeature(OgrLayer layer);

        [LibraryImport("gdal")]
        private static partial void OGR_L_ResetReading(OgrLayerHandle layer);
        [CbsgWrapperMethod]
        public static partial void OGR_L_ResetReading(OgrLayer layer);

        [LibraryImport("gdal")]
        private static partial OgrError OGR_L_SetNextByIndex(OgrLayerHandle layer, long index);
        [CbsgWrapperMethod]
        public static partial OgrError OGR_L_SetNextByIndex(OgrLayer layer, long index);

        [LibraryImport("gdal")]
        private static partial OgrFeatureHandle.Owns OGR_L_GetFeature(OgrLayerHandle layer, long index);
        [CbsgWrapperMethod]
        public static partial OgrFeature? OGR_L_GetFeature(OgrLayer layer, long index);

        [LibraryImport("gdal")]
        private static partial OgrWkbGeometryType OGR_L_GetGeomType(OgrLayerHandle layer);
        [CbsgWrapperMethod]
        public static partial OgrWkbGeometryType OGR_L_GetGeomType(OgrLayer layer);

        [LibraryImport("gdal")]
        private static partial nint OGR_L_GetGeometryTypes(OgrLayerHandle layer, int geomField, int flags, out int entryCount, nint progressFunc, nint progressData);
        [CbsgWrapperMethod]
        public static partial nint OGR_L_GetGeometryTypes(OgrLayer layer, int geomField, int flags, out int entryCount, nint progressFunc, nint progressData);

        [LibraryImport("gdal")]
        private static partial OgrGeometryHandle.DoesntOwn OGR_L_GetSpatialFilter(OgrLayerHandle layer);

        [CbsgWrapperMethod]
        public static partial OgrGeometry? OGR_L_GetSpatialFilter(OgrLayer layer);

        [LibraryImport("gdal")]
        private static partial void OGR_L_SetSpatialFilter(OgrGeometryHandle geometry);
        [CbsgWrapperMethod]
        public static partial void OGR_L_SetSpatialFilter(OgrGeometry? geometry);

        [LibraryImport("gdal")]
        private static partial void OGR_L_SetSpatialFilterEx(OgrGeometryHandle geometry);
        [CbsgWrapperMethod]
        public static partial void OGR_L_SetSpatialFilterEx(OgrGeometry? geometry);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial OgrError OGR_L_SetAttributeFilter(OgrLayerHandle layer, string? query);
        [CbsgWrapperMethod]
        public static partial OgrError OGR_L_SetAttributeFilter(OgrLayer layer, string? query);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial OgrError OGR_L_SetFeature(OgrFeatureHandle feature);
        [CbsgWrapperMethod]
        public static partial OgrError OGR_L_SetFeature(OgrFeature feature);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial OgrError OGR_L_CreateFeature(OgrFeatureHandle feature);
        [CbsgWrapperMethod]
        public static partial OgrError OGR_L_CreateFeature(OgrFeature feature);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial OgrError OGR_L_DeleteFeature(OgrLayerHandle layer, long featureId);
        [CbsgWrapperMethod]
        public static partial OgrError OGR_L_DeleteFeature(OgrLayer layer, long featureId);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial OgrError OGR_L_UpsertFeature(OgrFeatureHandle feature);
        [CbsgWrapperMethod]
        public static partial OgrError OGR_L_UpsertFeature(OgrFeature feature);

        [LibraryImport("gdal")]
        private static partial OgrError OGR_L_UpdateFeature(OgrLayerHandle layer,
                                                           OgrFeatureHandle feature,
                                                           int updatedFieldsCount,
                                                           [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=2), In]
                                                           int[] updatedFieldsIndexes,
                                                           int updateGeomFieldCount,
                                                           [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=4), In]
                                                           int[] updatedGeomFieldIndexes,
                                                           [MarshalAs(UnmanagedType.Bool)]
                                                           bool updateStyleString);

        [CbsgWrapperMethod]
        public static partial OgrError OGR_L_UpdateFeature(OgrLayer layer,
                                                   OgrFeature feature,
                                                   int updatedFieldsCount,
                                                   int[] updatedFieldsIndexes,
                                                   int updateGeomFieldCount,
                                                   int[] updatedGeomFieldIndexes,
                                                   bool updateStyleString);

}