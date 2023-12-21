// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;

namespace MMKiwi.GdalNet;

[CLSCompliant(false)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal unsafe static partial class OgrCoreH
{
    [LibraryImport("gdal", EntryPoint = "OGRMalloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint OGRMalloc(ulong _0);

    [LibraryImport("gdal", EntryPoint = "OGRCalloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint OGRCalloc(ulong _0, ulong _1);

    [LibraryImport("gdal", EntryPoint = "OGRRealloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint OGRRealloc(nint _0, ulong _1);

    [LibraryImport("ogr", EntryPoint = "OGRStrdup", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial sbyte* OGRStrdup(string _0);

    [LibraryImport("gdal", EntryPoint = "OGRFree")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void OGRFree(nint _0);

    [LibraryImport("gdal", EntryPoint = "OGRGeometryTypeToName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint OGRGeometryTypeToName(OgrWkbGeometryType eType);

    [LibraryImport("gdal", EntryPoint = "OGRMergeGeometryTypes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGRMergeGeometryTypes(OgrWkbGeometryType eMain,
        OgrWkbGeometryType eExtra);

    [LibraryImport("gdal", EntryPoint = "OGRMergeGeometryTypesEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGRMergeGeometryTypesEx(OgrWkbGeometryType eMain, OgrWkbGeometryType eExtra, int bAllowPromotingToCurves);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_Flatten")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_Flatten(OgrWkbGeometryType eType);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_SetZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_SetZ(OgrWkbGeometryType eType);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_SetM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_SetM(OgrWkbGeometryType eType);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_SetModifier")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_SetModifier(OgrWkbGeometryType eType,
        int bSetZ,
        int bSetM);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_HasZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_HasZ(OgrWkbGeometryType eType);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_HasM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_HasM(OgrWkbGeometryType eType);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_IsSubClassOf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_IsSubClassOf(OgrWkbGeometryType eType,
        OgrWkbGeometryType eSuperType);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_IsCurve")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_IsCurve(OgrWkbGeometryType _0);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_IsSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_IsSurface(OgrWkbGeometryType _0);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_IsNonLinear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_IsNonLinear(OgrWkbGeometryType _0);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_GetCollection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_GetCollection(OgrWkbGeometryType eType);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_GetCurve")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_GetCurve(OgrWkbGeometryType eType);

    [LibraryImport("gdal", EntryPoint = "OGR_GT_GetLinear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_GetLinear(OgrWkbGeometryType eType);

    [LibraryImport("ogr", EntryPoint = "OGR_GET_MS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GET_MS(float fSec);

    [LibraryImport("gdal", EntryPoint = "OGRParseDate", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGRParseDate(string pszInput,
        nint psOutput,
        int nOptions);

    [LibraryImport("gdal", EntryPoint = "GDALVersionInfo", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint GDALVersionInfo(string _0);

    [LibraryImport("gdal", EntryPoint = "GDALCheckVersion", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int GDALCheckVersion(int nVersionMajor,
        int nVersionMinor,
        string pszCallingComponentName);
}