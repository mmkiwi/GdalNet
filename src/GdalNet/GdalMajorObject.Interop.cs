// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[SourceGenerators.GenerateGdalMarshal]
public partial class GdalMajorObject
{
    [CLSCompliant(false)]
    internal static partial class Interop
    {
        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string? GDALGetDescription(GdalMajorObject obj);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial void GDALSetDescription(GdalMajorObject obj, string? description);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalUsing(typeof(CStringArrayMarshal))]
        public static partial Dictionary<string, string> GDALGetMetadata(GdalMajorObject obj, string? domain);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial GdalCplErr GDALSetMetadata(GdalMajorObject obj, [MarshalUsing(typeof(CStringArrayMarshal))] IReadOnlyDictionary<string,string>? metadata, string? domain);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial GdalCplErr GDALSetMetadataItem(GdalMajorObject obj, string name, string? value, string? domain);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalUsing(typeof(CStringArrayMarshal))]
        public static partial string[] GDALGetMetadataDomainList(GdalMajorObject obj);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string GDALGetMetadataItem(GdalMajorObject obj, string name, string? domain);
    }
}