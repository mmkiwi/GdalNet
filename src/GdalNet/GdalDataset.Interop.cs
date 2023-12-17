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

public sealed partial class GdalDataset
{

    [CLSCompliant(false)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static new partial class Interop
    {
        static Interop() => GdalError.EnsureInitialize();

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint = nameof(GDALOpenEx))]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        private static partial GdalDatasetHandle.Owns _GDALOpenEx(string fileName,
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
        public static partial int GDALGetRasterXSize(GdalDatasetHandle dataset);

        [GdalWrapperMethod]
        public static partial int GDALGetRasterXSize(GdalDataset dataset);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial int GDALGetRasterYSize(GdalDatasetHandle dataset);

        [GdalWrapperMethod]
        public static partial int GDALGetRasterYSize(GdalDataset dataset);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial GdalRasterBandHandle GDALGetRasterBand(GdalDatasetHandle dataset, int bandId);
        [GdalWrapperMethod]
        public static partial GdalRasterBand GDALGetRasterBand(GdalDataset dataset, int bandId);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial int GDALDatasetGetLayerCount(GdalDatasetHandle dataset);
        [GdalWrapperMethod]
        public static partial int GDALDatasetGetLayerCount(GdalDataset dataset);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial OgrLayerHandle GDALDatasetGetLayerByName(GdalDatasetHandle dataset, string layer);

        [GdalWrapperMethod]
        public static partial OgrLayer? GDALDatasetGetLayerByName(GdalDataset dataset, string layer);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial OgrLayerHandle GDALDatasetGetLayer(GdalDatasetHandle dataset, int layerId);
        [GdalWrapperMethod]
        public static partial OgrLayer GDALDatasetGetLayer(GdalDataset dataset, int layerId);

    }
}

