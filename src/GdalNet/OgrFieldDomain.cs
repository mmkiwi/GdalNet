﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;


namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<OgrFieldDomain,OgrFieldDomainHandle>))]
public class OgrFieldDomain: IDisposable, IConstructableWrapper<OgrFieldDomain, OgrFieldDomainHandle>, IHasHandle<OgrFieldDomainHandle>
{
    private bool _disposedValue;

    public string Name => OgrApiH.OGR_FldDomain_GetName(this);
    public string Description => OgrApiH.OGR_FldDomain_GetDescription(this);
    public OgrFieldDomainType DomainType => OgrApiH.OGR_FldDomain_GetDomainType(this);
    public OgrFieldType FieldType => OgrApiH.OGR_FldDomain_GetFieldType(this);
    public OgrFieldSubType FieldSubType => OgrApiH.OGR_FldDomain_GetFieldSubType(this);
    public OgrFieldDomainSplitPolicy SplitPolicy
    {
        get => OgrApiH.OGR_FldDomain_GetSplitPolicy(this);
        set => OgrApiH.OGR_FldDomain_SetSplitPolicy(this, value);
    }
    public OgrFieldDomainMergePolicy MergePolicy
    {
        get => OgrApiH.OGR_FldDomain_GetMergePolicy(this);
        set => OgrApiH.OGR_FldDomain_SetMergePolicy(this, value);
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
