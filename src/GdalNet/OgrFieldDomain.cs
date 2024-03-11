// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<OgrFieldDomain,OgrFieldDomainHandle>))]
public class OgrFieldDomain: IDisposable, IConstructableWrapper<OgrFieldDomain, OgrFieldDomainHandle>, IHasHandle<OgrFieldDomainHandle>
{
    private bool _disposedValue;

    public string Name
    {
        get
        {
            var result = OgrApiH.OGR_FldDomain_GetName(this);
            GdalError.ThrowIfError();
            return result;
        }
    }
    public string Description
    {
        get
        {
            var result = OgrApiH.OGR_FldDomain_GetDescription(this);
            GdalError.ThrowIfError();
            return result;
        }
    }
    public OgrFieldDomainType DomainType
    {
        get
        {
            var result = OgrApiH.OGR_FldDomain_GetDomainType(this);
            GdalError.ThrowIfError();
            return result;
        }
    }
    public OgrFieldType FieldType
    {
        get
        {
            var result = OgrApiH.OGR_FldDomain_GetFieldType(this);
            GdalError.ThrowIfError();
            return result;
        }
    }
    public OgrFieldSubType FieldSubType
    {
        get
        {
            var result = OgrApiH.OGR_FldDomain_GetFieldSubType(this);
            GdalError.ThrowIfError();
            return result;
        }
    }
    public OgrFieldDomainSplitPolicy SplitPolicy
    {
        get
        {
            var result = OgrApiH.OGR_FldDomain_GetSplitPolicy(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_FldDomain_SetSplitPolicy(this, value);
            GdalError.ThrowIfError();
        }
    }
    public OgrFieldDomainMergePolicy MergePolicy
    {
        get
        {
            var result = OgrApiH.OGR_FldDomain_GetMergePolicy(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_FldDomain_SetMergePolicy(this, value);
            GdalError.ThrowIfError();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Handle.Dispose();
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
    static OgrFieldDomain IConstructableWrapper<OgrFieldDomain, OgrFieldDomainHandle>.Construct(OgrFieldDomainHandle handle) => new(handle);
    OgrFieldDomainHandle IHasHandle<OgrFieldDomainHandle>.Handle => Handle;
    internal OgrFieldDomainHandle Handle { get; }
    private protected OgrFieldDomain(OgrFieldDomainHandle handle) => Handle = handle;
}
