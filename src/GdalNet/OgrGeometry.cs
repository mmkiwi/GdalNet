// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;
public abstract partial class OgrGeometry : GdalSafeHandle
{
    protected OgrGeometry(nint pointer, bool ownsHandle) : base(pointer, ownsHandle)
    {
    }

    protected override bool ReleaseHandle()
    {
        Interop.OGR_G_DestroyGeometry(this);
        GdalError.ThrowIfError();
        return true;
    }

    public static OgrGeometry CreateFromWkb(ReadOnlySpan<byte> wkb, OgrSpatialReference? spatialReference = null)
    {
        var err = Interop.OGR_G_CreateFromWkb(wkb, spatialReference, out OgrGeometry result, wkb.Length);
        GdalError.ThrowIfError(err);
        return result;
    }

    public static OgrGeometry CreateFromWkt(string wkt, OgrSpatialReference? spatialReference = null)
    {

        var err = CreateFromWktMarshal(wkt, spatialReference, out OgrGeometry result);
        GdalError.ThrowIfError(err);
        return result;
    }

    private static unsafe OgrError CreateFromWktMarshal(string wkt, OgrSpatialReference? spatialReference, out OgrGeometry geometry)
    {
        scoped Utf8StringMarshaller.ManagedToUnmanagedIn stringMarshaller = new();
        try
        {
            stringMarshaller.FromManaged(wkt, stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
            {
                byte* wktPointer = stringMarshaller.ToUnmanaged();
                return Interop.OGR_G_CreateFromWkt(out wktPointer, spatialReference, out geometry);
            }
        }
        finally
        {
            stringMarshaller.Free();
        }

    }
}
