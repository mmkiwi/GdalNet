// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper(ConstructorVisibility = MemberVisibility.PrivateProtected)]
public partial class OgrFieldDomain: IDisposable, IConstructableWrapper<OgrFieldDomain, OgrFieldDomainHandle>, IHasHandle<OgrFieldDomainHandle>
{
    private bool _disposedValue;

    public string Name => Interop.OGR_FldDomain_GetName(this);
    public string Description => Interop.OGR_FldDomain_GetDescription(this);
    public OgrFieldDomainType DomainType => Interop.OGR_FldDomain_GetDomainType(this);
    public OgrFieldType FieldType => Interop.OGR_FldDomain_GetFieldType(this);
    public OgrFieldSubType FieldSubType => Interop.OGR_FldDomain_GetFieldSubType(this);
    public OgrFieldDomainSplitPolicy SplitPolicy
    {
        get => Interop.OGR_FldDomain_GetSplitPolicy(this);
        set => Interop.OGR_FldDomain_SetSplitPolicy(this, value);
    }
    public OgrFieldDomainMergePolicy MergePolicy
    {
        get => Interop.OGR_FldDomain_GetMergePolicy(this);
        set => Interop.OGR_FldDomain_SetMergePolicy(this, value);
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
}
