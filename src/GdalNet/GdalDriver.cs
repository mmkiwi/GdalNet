// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<GdalDriver, GdalDriverHandle>))]
public class GdalDriver : IConstructableWrapper<GdalDriver, GdalDriverHandle>, IHasHandle<GdalDriverHandle>
{
    private GdalDriver(GdalDriverHandle handle) => Handle = handle;
    internal GdalDriverHandle Handle { get; }

    static GdalDriver IConstructableWrapper<GdalDriver, GdalDriverHandle>.Construct(GdalDriverHandle handle)
        => new(handle);
    
    GdalDriverHandle IHasHandle<GdalDriverHandle>.Handle => Handle;

    public GdalDataset? Create(string path, int width = 0, int height = 0, int bands = 0, GdalDataType dataType = GdalDataType.Unknown,
        string[]? options = null)
    {
        var result = GdalH.GDALCreate(this, path, width, height, bands, dataType, options);
        GdalError.ThrowIfError();
        return result;
    }
}
