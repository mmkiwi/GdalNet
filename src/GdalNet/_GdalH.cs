// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[CLSCompliant(false)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[GdalEnforceErrorHandling]
internal static partial class GdalH
{
    public const string GdalDll = "gdal";

    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string? GDALGetDescription(GdalMajorObject obj);

    [LibraryImport(GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial void GDALSetDescription(GdalMajorObject obj, string? description);

    [LibraryImport(GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(CStringArrayMarshal))]
    public static partial Dictionary<string, string> GDALGetMetadata(GdalMajorObject obj, string? domain);
    
    [LibraryImport(GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial GdalCplErr GDALSetMetadata(GdalMajorObject obj,
        [MarshalUsing(typeof(CStringArrayMarshal))]
        IReadOnlyDictionary<string, string>? metadata, string? domain);

    [LibraryImport(GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial GdalCplErr GDALSetMetadataItem(GdalMajorObject obj, string name, string? value,
        string? domain);
    
    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(CStringArrayMarshal))]
    public static partial string[] GDALGetMetadataDomainList(GdalMajorObject obj);
    
    [LibraryImport(GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string GDALGetMetadataItem(GdalMajorObject obj, string name, string? domain);
    
    [LibraryImport(GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return:MarshalUsing(typeof(GdalOwnsMarshaller<GdalDataset,GdalDatasetHandle>))]
    public static partial GdalDataset? GDALOpenEx(string fileName,
        GdalOpenFlags openFlags,
        [MarshalUsing(typeof(CStringArrayMarshal))]
        IEnumerable<string>? allowedDrivers,
        [MarshalUsing(typeof(CStringArrayMarshal))]
        IReadOnlyDictionary<string, string>? openOptions,
        [MarshalUsing(typeof(CStringArrayMarshal))]
        IEnumerable<string>? siblingFiles);
    
    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial int GDALGetRasterCount(GdalDataset dataset);
    
    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial int GDALGetRasterXSize(GdalDataset dataset);
    
    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial int GDALGetRasterYSize(GdalDataset dataset);

    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial GdalRasterBand GDALGetRasterBand(GdalDataset dataset, int bandId);
    
    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial int GDALDatasetGetLayerCount(GdalDataset dataset);

    [LibraryImport(GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial OgrLayer? GDALDatasetGetLayerByName(GdalDataset dataset, string layer);
    
    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial OgrLayer? GDALDatasetGetLayer(GdalDataset dataset, int layerId);
    
    [LibraryImport(GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return:MarshalUsing(typeof(GdalOwnsMarshaller<GdalVirtualDataset, GdalVirtualDatasetHandle>))]
    public unsafe static partial GdalVirtualDataset  VSIFileFromMemBuffer(string fileName, byte* buffer, long buffSize, [MarshalAs(UnmanagedType.Bool)] bool takeOwnership);
    
    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(Utf8StringNoFree))]
    public static partial string GDALVersionInfo(ReadOnlySpan<byte> requestType);
    
    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial void GDALAllRegister();

    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [return: MarshalUsing(typeof(CStringArrayMarshal))]
    public static partial string[] GDALGetRasterCategoryNames(GdalRasterBand rasterBand);

    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial GdalDataType GDALGetRasterDataType(GdalRasterBand rasterBand);
    
    [LibraryImport(GdalDll)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [GdalEnforceErrorHandling(false)]
    public static partial GdalCplErr GDALClose(nint dataset);
    
    [LibraryImport(GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [GdalEnforceErrorHandling(false)]
    public static partial int VSIFCloseL(nint dataset);
}