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
        private static partial string[] GDALGetRasterCategoryNames(GdalRasterBandHandle rasterBand);
        [GdalWrapperMethod]
        public static partial string[] GDALGetRasterCategoryNames(GdalRasterBand rasterBand);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        private static partial GdalDataType GDALGetRasterDataType(GdalRasterBandHandle rasterBand);
        [GdalWrapperMethod]
        public static partial GdalDataType GDALGetRasterDataType(GdalRasterBand rasterBand);
    }
}
