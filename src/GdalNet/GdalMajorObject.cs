// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;

namespace MMKiwi.GdalNet;

public abstract partial class GdalMajorObject: IHasHandle<GdalInternalHandle>
{
    public string? Description
    {
        get => Interop.GDALGetDescription(this);
        set => Interop.GDALSetDescription(this, value);
    }

    public Dictionary<string, string> GetMetadata(string? domain = null)
        => Interop.GDALGetMetadata(this, domain);

    public string GetMetadataItem(string name, string? domain = null)
    {
        ArgumentNullException.ThrowIfNull(name);

        return Interop.GDALGetMetadataItem(this, name, domain);
    }

    public void SetMetadata(Dictionary<string, string>? metadata, string? domain = null)
        => Interop.GDALSetMetadata(this, metadata, domain);

    public void SetMetadataItem(string name, string? value, string? domain = null)
    {
        ArgumentNullException.ThrowIfNull(name);

        Interop.GDALSetMetadataItem(this, name, value, domain);
    }

    public string[] MetadataDomainList => Interop.GDALGetMetadataDomainList(this);

    GdalInternalHandle IHasHandle<GdalInternalHandle>.Handle => Handle;

    private protected GdalInternalHandle Handle { get; }

    private protected GdalMajorObject(GdalInternalHandle handle)
    {
        Handle = handle;
    }
}