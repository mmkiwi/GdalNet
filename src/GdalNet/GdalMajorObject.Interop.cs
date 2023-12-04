// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Marshallers;
using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

public partial class GdalMajorObject
{
    [CLSCompliant(false)]
    internal static partial class Interop
    {
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
        private static partial GdalCplErr GDALSetMetadata(GdalInternalHandle obj, [MarshalUsing(typeof(CStringArrayMarshal))] IReadOnlyDictionary<string,string>? metadata, string? domain);

        [GdalWrapperMethod]
        public static partial GdalCplErr GDALSetMetadata(GdalMajorObject obj, IReadOnlyDictionary<string, string>? metadata, string? domain);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        private static partial GdalCplErr GDALSetMetadataItem(GdalInternalHandle obj, string name, string? value, string? domain);

        [GdalWrapperMethod]
        public static partial GdalCplErr GDALSetMetadataItem(GdalMajorObject obj, string name, string? value, string? domain);

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
    }
}