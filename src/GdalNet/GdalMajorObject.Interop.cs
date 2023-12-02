// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public partial class GdalMajorObject
{
    [CLSCompliant(false)]
    internal static partial class Interop
    {
        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string? GDALGetDescription(SafeHandle obj);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial void GDALSetDescription(SafeHandle obj, string? description);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        [return: MarshalUsing(typeof(CStringArrayMarshal))]
        public static partial Dictionary<string, string> GDALGetMetadata(SafeHandle obj, string? domain);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial GdalCplErr GDALSetMetadata(SafeHandle obj, [MarshalUsing(typeof(CStringArrayMarshal))] IReadOnlyDictionary<string,string>? metadata, string? domain);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial GdalCplErr GDALSetMetadataItem(SafeHandle obj, string name, string? value, string? domain);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        [return: MarshalUsing(typeof(CStringArrayMarshal))]
        public static partial string[] GDALGetMetadataDomainList(SafeHandle obj);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string GDALGetMetadataItem(SafeHandle obj, string name, string? domain);
    }
}