// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;
public abstract partial class OgrGeometry: IDisposable
{
    private bool _disposedValue;

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
                var result = Interop.OGR_G_CreateFromWkt(ref wktPointer, spatialReference, out geometry);
                GdalError.ThrowIfError();
                return result;
            }
        }
        finally
        {
            stringMarshaller.Free();
        }
    }

    public int Dimension
    {
        get
        {
            var dimension = Interop.OGR_G_GetDimension(this);
            GdalError.ThrowIfError();
            return dimension;
        }
    }

    public int CoordinateDimension
    {
        get
        {
            int coordDimension = Interop.OGR_G_CoordinateDimension(this);
            GdalError.ThrowIfError();
            return coordDimension;
        }
    }

    public bool Is3D
    {
        get
        {
            bool is3D = Interop.OGR_G_Is3D(this);
            GdalError.ThrowIfError();
            return is3D;
        }
        set
        {
            Interop.OGR_G_Set3D(this, value);
            GdalError.ThrowIfError();
        }
    }

    public OgrGeometry Clone()
    {
        var clone = Interop.OGR_G_Clone(this);
        GdalError.ThrowIfError();
        return clone;
    }

    public OgrEnvelope Envelope
    {
        get
        {
            OgrEnvelope? result = null;
            Interop.OGR_G_GetEnvelope(this, ref result);
            GdalError.ThrowIfError();
            return result;
        }
    }

    public OgrWkbGeometryType GeometryType
    {
        get
        {
            OgrWkbGeometryType result = Interop.OGR_G_GetGeometryType(this);
            GdalError.ThrowIfError();
            return result;
        }
    }

    public OgrEnvelope3D Envelope3D
    {
        get
        {
            OgrEnvelope3D? result = null;
            Interop.OGR_G_GetEnvelope3D(this, ref result);
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
