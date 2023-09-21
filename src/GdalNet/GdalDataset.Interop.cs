// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal<GdalDataset>))]
public sealed partial class GdalDataset: IConstructibleHandle<GdalDataset>
{
    private GdalDataset(nint pointer): this() => SetHandle(pointer);
    public static GdalDataset Construct(nint pointer) => new(pointer);

    protected override bool ReleaseHandle()
    {
        var errCode = Interop.GDALClose(this);
        return errCode == GdalCplErr.None;
    }

    [CLSCompliant(false)]
    internal static new partial class Interop
    {
        static Interop() => GdalError.EnsureInitialize();

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial GdalCplErr GDALClose(GdalDataset dataset);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial GdalDataset? GDALOpenEx(string fileName,
                                                      GdalOpenFlags openFlags,
                                                      [MarshalUsing(typeof(CStringArrayMarshal))]
                                                      IEnumerable<string>? allowedDrivers,
                                                      [MarshalUsing(typeof(CStringArrayMarshal))]
                                                      IReadOnlyDictionary<string, string>? openOptions,
                                                      [MarshalUsing(typeof(CStringArrayMarshal))]
                                                      IEnumerable<string>? siblingFiles);


        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial int GDALGetRasterCount(GdalDataset dataset);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial int GDALGetRasterXSize(GdalDataset dataset);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial int GDALGetRasterYSize(GdalDataset dataset);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial GdalRasterBand? GDALGetRasterBand(GdalDataset dataset, int bandId);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial int GDALDatasetGetLayerCount(GdalDataset dataset);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial OgrLayer? GDALDatasetGetLayerByName(GdalDataset dataset, string layer);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial OgrLayer? GDALDatasetGetLayer(GdalDataset dataset, int layerId);
    }
}

