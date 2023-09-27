// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[SourceGenerators.GenerateGdalMarshal]
public sealed partial class GdalRasterBand: GdalHandle
{
    [CLSCompliant(false)]
    internal static partial class Interop
    {
        static Interop() => GdalError.EnsureInitialize();

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalUsing(typeof(CStringArrayMarshal))]
        public static partial string[] GDALGetRasterCategoryNames(GdalRasterBand rasterBand);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial GdalDataType GDALGetRasterDataType(GdalRasterBand rasterBand);
    }
}
