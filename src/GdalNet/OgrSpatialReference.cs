// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<OgrSpatialReference, OgrSpatialReferenceHandle>))]
public sealed class OgrSpatialReference : IConstructableWrapper<OgrSpatialReference, OgrSpatialReferenceHandle>, IHasHandle<OgrSpatialReferenceHandle>, IDisposable
{
    private OgrSpatialReference(OgrSpatialReferenceHandle handle) => Handle = handle;
    internal OgrSpatialReferenceHandle Handle { get; }

    static OgrSpatialReference IConstructableWrapper<OgrSpatialReference, OgrSpatialReferenceHandle>.Construct(OgrSpatialReferenceHandle handle)
        => new(handle);
    OgrSpatialReferenceHandle IHasHandle<OgrSpatialReferenceHandle>.Handle => Handle;

    public static OgrSpatialReference Create()
    {
        OgrSpatialReference res = OgrSrsApiH.OSRNewSpatialReference(null);
        GdalError.ThrowIfError();
        return res;
    }

    public void ImportFromEPSG(int epsg)
    {
        OgrSrsApiH.OSRImportFromEPSG(this, epsg).ThrowIfError();
    }
    
    public string? Wkt
    {
        get
        {
            OgrSrsApiH.OSRExportToWkt(this, out string? wkt).ThrowIfError();
            return wkt;
        }
        set
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            OgrSrsApiH.OSRImportFromWkt(this, value).ThrowIfError();
        }
    }

    public string Name
    {
        get
        {
            var result = OgrSrsApiH.OSRGetName(this);
            GdalError.ThrowIfError();
            return result ?? string.Empty;
        }
    }

    public void Dispose() => Handle.Dispose();
}