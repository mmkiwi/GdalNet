// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshallerNeverOwns<GdalRasterBand,GdalRasterBandHandle>))]
public class GdalRasterBand: IConstructableWrapper<GdalRasterBand, GdalRasterBandHandle>, IHasHandle<GdalRasterBandHandle>
{
    private GdalRasterBand(GdalRasterBandHandle handle)
    {
        Handle = handle;
    }
    private GdalRasterBandHandle Handle { get; }
    public string[] Categories
    {
        get
        {
            var result = GdalH.GDALGetRasterCategoryNames(this);
            GdalError.ThrowIfError();
            return result;
        }
    }

    public GdalDataType DataType
    {
        get
        {
            var result = GdalH.GDALGetRasterDataType(this);
            GdalError.ThrowIfError();
            return result;
        }
    }

    static GdalRasterBand IConstructableWrapper<GdalRasterBand, GdalRasterBandHandle>.Construct(GdalRasterBandHandle handle) => new(handle);
    GdalRasterBandHandle IHasHandle<GdalRasterBandHandle>.Handle => Handle;
}