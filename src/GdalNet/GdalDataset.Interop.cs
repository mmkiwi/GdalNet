// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public sealed partial class GdalDataset: IConstructibleHandle<GdalDataset>
{
    public static GdalDataset Construct(nint pointer, bool ownsHandle) => new(pointer, ownsHandle);

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
        public static partial GdalCplErr GDALClose([MarshalUsing(typeof(MarshalIn<GdalDataset>))] GdalDataset dataset);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return:MarshalUsing(typeof(MarshalOwnsHandle<GdalDataset>))]
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
        public static partial int GDALGetRasterCount([MarshalUsing(typeof(MarshalIn<GdalDataset>))] GdalDataset dataset);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial int GDALGetRasterXSize([MarshalUsing(typeof(MarshalIn<GdalDataset>))] GdalDataset dataset);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial int GDALGetRasterYSize([MarshalUsing(typeof(MarshalIn<GdalDataset>))] GdalDataset dataset);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalUsing(typeof(MarshalDoesNotOwnHandle<GdalRasterBand>))]
        public static partial GdalRasterBand? GDALGetRasterBand([MarshalUsing(typeof(MarshalIn<GdalDataset>))] GdalDataset dataset, int bandId);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial int GDALDatasetGetLayerCount([MarshalUsing(typeof(MarshalIn<GdalDataset>))] GdalDataset dataset);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalUsing(typeof(MarshalDoesNotOwnHandle<OgrLayer>))]
        public static partial OgrLayer? GDALDatasetGetLayerByName([MarshalUsing(typeof(MarshalIn<GdalDataset>))] GdalDataset dataset, string layer);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalUsing(typeof(MarshalDoesNotOwnHandle<OgrLayer>))]
        public static partial OgrLayer? GDALDatasetGetLayer([MarshalUsing(typeof(MarshalIn<GdalDataset>))] GdalDataset dataset, int layerId);
    }
}

