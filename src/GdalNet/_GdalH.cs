// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.InteropAttributes;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[CLSCompliant(false)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal static partial class GdalH
{
    static GdalH() => GdalError.EnsureInitialize();

    [LibraryImport("gdal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    private static partial string? GDALGetDescription(GdalInternalHandle obj);

    [GdalWrapperMethod]
    public static partial string? GDALGetDescription(GdalMajorObject majorObject);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private static partial void GDALSetDescription(GdalInternalHandle obj, string? description);

    [GdalWrapperMethod]
    public static partial void GDALSetDescription(GdalMajorObject obj, string? description);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(CStringArrayMarshal))]
    private static partial Dictionary<string, string> GDALGetMetadata(GdalInternalHandle obj, string? domain);

    [GdalWrapperMethod]
    public static partial Dictionary<string, string> GDALGetMetadata(GdalMajorObject obj, string? domain);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(ThrowMarshal))]
    private static partial GdalCplErr GDALSetMetadata(GdalInternalHandle obj,
        [MarshalUsing(typeof(CStringArrayMarshal))]
        IReadOnlyDictionary<string, string>? metadata, string? domain);

    [GdalWrapperMethod]
    public static partial GdalCplErr GDALSetMetadata(GdalMajorObject obj, IReadOnlyDictionary<string, string>? metadata,
        string? domain);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(ThrowMarshal))]
    private static partial GdalCplErr GDALSetMetadataItem(GdalInternalHandle obj, string name, string? value,
        string? domain);

    [GdalWrapperMethod]
    public static partial GdalCplErr GDALSetMetadataItem(GdalMajorObject obj, string name, string? value,
        string? domain);

    [LibraryImport("gdal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(CStringArrayMarshal))]
    private static partial string[] GDALGetMetadataDomainList(GdalInternalHandle obj);

    [GdalWrapperMethod]
    public static partial string[] GDALGetMetadataDomainList(GdalMajorObject obj);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    private static partial string GDALGetMetadataItem(GdalInternalHandle obj, string name, string? domain);

    [GdalWrapperMethod]
    public static partial string GDALGetMetadataItem(GdalMajorObject obj, string name, string? domain);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(GDALOpenEx))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial GdalDatasetHandle.Owns _GDALOpenEx(string fileName,
        GdalOpenFlags openFlags,
        [MarshalUsing(typeof(CStringArrayMarshal))]
        IEnumerable<string>? allowedDrivers,
        [MarshalUsing(typeof(CStringArrayMarshal))]
        IReadOnlyDictionary<string, string>? openOptions,
        [MarshalUsing(typeof(CStringArrayMarshal))]
        IEnumerable<string>? siblingFiles);

    [GdalWrapperMethod(MethodName = nameof(_GDALOpenEx))]
    public static partial GdalDataset? GDALOpenEx(
        string fileName,
        GdalOpenFlags openFlags,
        IEnumerable<string>? allowedDrivers,
        IReadOnlyDictionary<string, string>? openOptions,
        IEnumerable<string>? siblingFiles);

    [LibraryImport("gdal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private static partial int GDALGetRasterCount(GdalDatasetHandle dataset);

    [GdalWrapperMethod]
    public static partial int GDALGetRasterCount(GdalDataset dataset);

    [LibraryImport("gdal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private static partial int GDALGetRasterXSize(GdalDatasetHandle dataset);

    [GdalWrapperMethod]
    public static partial int GDALGetRasterXSize(GdalDataset dataset);

    [LibraryImport("gdal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private static partial int GDALGetRasterYSize(GdalDatasetHandle dataset);

    [GdalWrapperMethod]
    public static partial int GDALGetRasterYSize(GdalDataset dataset);

    [LibraryImport("gdal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private static partial GdalRasterBandHandle GDALGetRasterBand(GdalDatasetHandle dataset, int bandId);

    [GdalWrapperMethod]
    public static partial GdalRasterBand GDALGetRasterBand(GdalDataset dataset, int bandId);

    [LibraryImport("gdal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private static partial int GDALDatasetGetLayerCount(GdalDatasetHandle dataset);

    [GdalWrapperMethod]
    public static partial int GDALDatasetGetLayerCount(GdalDataset dataset);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private static partial OgrLayerHandle GDALDatasetGetLayerByName(GdalDatasetHandle dataset, string layer);

    [GdalWrapperMethod]
    public static partial OgrLayer? GDALDatasetGetLayerByName(GdalDataset dataset, string layer);

    [LibraryImport("gdal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private static partial OgrLayerHandle GDALDatasetGetLayer(GdalDatasetHandle dataset, int layerId);

    [GdalWrapperMethod]
    public static partial OgrLayer GDALDatasetGetLayer(GdalDataset dataset, int layerId);

    [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(VSIFileFromMemBuffer))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private unsafe static partial GdalVirtualDatasetHandle.Owns _VSIFileFromMemBuffer(string fileName, byte* buffer, long buffSize, [MarshalAs(UnmanagedType.Bool)] bool takeOwnership);

    [GdalWrapperMethod(MethodName = nameof(_VSIFileFromMemBuffer))]
    public unsafe static partial GdalVirtualDataset? VSIFileFromMemBuffer(string fileName, byte* buffer, long buffSize, bool takeOwnership);

    [LibraryImport("gdal", EntryPoint = nameof(GDALVersionInfo))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    private static partial string _GDALVersionInfo(ReadOnlySpan<byte> requestType);

    [GdalWrapperMethod(MethodName = nameof(_GDALVersionInfo))]
    public static partial string GDALVersionInfo(ReadOnlySpan<byte> requestType);

    [LibraryImport("gdal", EntryPoint = nameof(GDALAllRegister))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private static partial void _GDALAllRegister();

    [GdalWrapperMethod(MethodName = nameof(_GDALAllRegister))]
    public static partial void GDALAllRegister();
    
    
    [LibraryImport("gdal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(CStringArrayMarshal))]
    private static partial string[] GDALGetRasterCategoryNames(GdalRasterBandHandle rasterBand);
    [GdalWrapperMethod]
    public static partial string[] GDALGetRasterCategoryNames(GdalRasterBand rasterBand);

    [LibraryImport("gdal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    private static partial GdalDataType GDALGetRasterDataType(GdalRasterBandHandle rasterBand);
    [GdalWrapperMethod]
    public static partial GdalDataType GDALGetRasterDataType(GdalRasterBand rasterBand);
}