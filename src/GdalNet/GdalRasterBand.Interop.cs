// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;
public sealed partial class GdalRasterBand
{
    [CLSCompliant(false)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static partial class Interop
    {
        static Interop() => GdalError.EnsureInitialize();

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        [return: MarshalUsing(typeof(CStringArrayMarshal))]
        private static partial string[] GDALGetRasterCategoryNames(MarshalHandle rasterBand);
        [GdalWrapperMethod]
        public static partial string[] GDALGetRasterCategoryNames(GdalRasterBand rasterBand);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        private static partial GdalDataType GDALGetRasterDataType(MarshalHandle rasterBand);
        [GdalWrapperMethod]
        public static partial GdalDataType GDALGetRasterDataType(GdalRasterBand rasterBand);
    }
}
