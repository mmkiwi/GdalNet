// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<OgrFeature, OgrFeatureHandle>))]
public sealed class OgrFeature : IDisposable, IConstructableWrapper<OgrFeature, OgrFeatureHandle>, IHasHandle<OgrFeatureHandle>
{
    private bool _disposedValue;

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                this.Handle.Dispose();
            }

            _disposedValue = true;
        }
    }

    public long Fid { get => OgrApiH.OGR_F_GetFID(this); set => OgrApiH.OGR_F_SetFID(this, value); }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
    }
    static OgrFeature IConstructableWrapper<OgrFeature, OgrFeatureHandle>.Construct(OgrFeatureHandle handle)
        => new(handle);
    OgrFeatureHandle IHasHandle<OgrFeatureHandle>.Handle => Handle;

    private OgrFeature(OgrFeatureHandle handle) => Handle = handle;
    
    internal OgrFeatureHandle Handle { get; }
}