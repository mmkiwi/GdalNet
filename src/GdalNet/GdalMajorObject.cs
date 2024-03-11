// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<GdalMajorObject,GdalInternalHandle>))]
public abstract class GdalMajorObject: IHasHandle<GdalInternalHandle>, IDisposable
{
    private bool _disposedValue;

    public string? Description
    {
        get
        {
            string? result = GdalH.GDALGetDescription(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            GdalH.GDALSetDescription(this, value);
            GdalError.ThrowIfError();
        }
    }

    public Dictionary<string, string> GetMetadata(string? domain = null)
    {
        var result = GdalH.GDALGetMetadata(this, domain);
        GdalError.ThrowIfError();
        return result;
    }

    public string GetMetadataItem(string name, string? domain = null)
    {
        ArgumentNullException.ThrowIfNull(name);

        var result = GdalH.GDALGetMetadataItem(this, name, domain);
        GdalError.ThrowIfError();
        return result;
    }

    public void SetMetadata(Dictionary<string, string>? metadata, string? domain = null)
        => GdalH.GDALSetMetadata(this, metadata, domain).ThrowIfError();

    public void SetMetadataItem(string name, string? value, string? domain = null)
    {
        ArgumentNullException.ThrowIfNull(name);

        GdalH.GDALSetMetadataItem(this, name, value, domain).ThrowIfError();
    }

    public string[] MetadataDomainList
    {
        get
        {
            string[] result = GdalH.GDALGetMetadataDomainList(this);
            GdalError.ThrowIfError();
            return result;
        }
    }

    protected virtual void Dispose(bool disposing)
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

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private protected GdalMajorObject(GdalInternalHandle handle)
    {
        Handle = handle;
    }
    
    internal GdalInternalHandle Handle { get; }
    
    [ExcludeFromCodeCoverage]
    GdalInternalHandle IHasHandle<GdalInternalHandle>.Handle => Handle;
}