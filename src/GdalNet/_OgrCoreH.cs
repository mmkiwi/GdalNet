// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;

using MMKiwi.GdalNet.Error;

namespace MMKiwi.GdalNet;

[CLSCompliant(false)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[GdalEnforceErrorHandling]
internal static partial class OgrCoreH
{
    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGRGeometryTypeToName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint OGRGeometryTypeToName(OgrWkbGeometryType eType);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGRMergeGeometryTypes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGRMergeGeometryTypes(OgrWkbGeometryType eMain, OgrWkbGeometryType eExtra);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGRMergeGeometryTypesEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGRMergeGeometryTypesEx(OgrWkbGeometryType eMain, OgrWkbGeometryType eExtra, int bAllowPromotingToCurves);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_Flatten")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_Flatten(OgrWkbGeometryType eType);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_SetZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_SetZ(OgrWkbGeometryType eType);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_SetM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_SetM(OgrWkbGeometryType eType);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_SetModifier")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_SetModifier(OgrWkbGeometryType eType,
        int bSetZ,
        int bSetM);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_HasZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_HasZ(OgrWkbGeometryType eType);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_HasM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_HasM(OgrWkbGeometryType eType);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_IsSubClassOf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_IsSubClassOf(OgrWkbGeometryType eType,
        OgrWkbGeometryType eSuperType);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_IsCurve")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_IsCurve(OgrWkbGeometryType _0);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_IsSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_IsSurface(OgrWkbGeometryType _0);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_IsNonLinear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GT_IsNonLinear(OgrWkbGeometryType _0);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_GetCollection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_GetCollection(OgrWkbGeometryType eType);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_GetCurve")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_GetCurve(OgrWkbGeometryType eType);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGR_GT_GetLinear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial OgrWkbGeometryType OGR_GT_GetLinear(OgrWkbGeometryType eType);

    [LibraryImport("ogr", EntryPoint = "OGR_GET_MS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGR_GET_MS(float fSec);

    [LibraryImport(GdalH.GdalDll, EntryPoint = "OGRParseDate", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int OGRParseDate(string pszInput,
        nint psOutput,
        int nOptions);

}