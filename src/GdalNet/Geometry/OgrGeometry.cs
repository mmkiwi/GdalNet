// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet.Geometry;

[NativeMarshalling(typeof(GdalMarshaller<OgrGeometry, OgrGeometryHandle>))]
public abstract partial class OgrGeometry : IDisposable
{
    private bool _disposedValue;

    public static OgrGeometry CreateFromWkb(ReadOnlySpan<byte> wkb, OgrSpatialReference? spatialReference = null)
    {
        OgrApiH.OGR_G_CreateFromWkb(wkb, spatialReference, out OgrGeometry result, wkb.Length).ThrowIfError();
        return result;
    }

    public static OgrGeometry CreateFromWkt(string wkt, OgrSpatialReference? spatialReference = null)
    {
        CreateFromWktMarshal(wkt, spatialReference, out OgrGeometry result);
        return result;
    }

    private static unsafe void CreateFromWktMarshal(string wkt, OgrSpatialReference? spatialReference, out OgrGeometry geometry)
    {
        scoped Utf8StringMarshaller.ManagedToUnmanagedIn stringMarshaller = new();
        try
        {
            stringMarshaller.FromManaged(wkt, stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
            {
                byte* wktPointer = stringMarshaller.ToUnmanaged();
                OgrApiH.OGR_G_CreateFromWkt(ref wktPointer, spatialReference, out geometry).ThrowIfError();
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
            var result = OgrApiH.OGR_G_GetDimension(this);
            GdalError.ThrowIfError();
            return result;
        }
    }

    public int CoordinateDimension
    {
        get
        {
            var result = OgrApiH.OGR_G_CoordinateDimension(this);
            GdalError.ThrowIfError();
            return result;
        }
    }

    public bool Is3D
    {
        get
        {
            var result = OgrApiH.OGR_G_Is3D(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_G_Set3D(this, value);
            GdalError.ThrowIfError();
        }
    }

    public OgrGeometry Clone()
    {
        var result = OgrApiH.OGR_G_Clone(this);
        GdalError.ThrowIfError();
        return result;
    }

    public OgrEnvelope Envelope
    {
        get
        {
            OgrApiH.OGR_G_GetEnvelope(this, out OgrEnvelope? result);
            GdalError.ThrowIfError();
            return result;
        }
    }

    public OgrWkbGeometryType GeometryType
    {
        get
        {
            var result = OgrApiH.OGR_G_GetGeometryType(this);
            GdalError.ThrowIfError();
            return result;
        }
    }

    public OgrEnvelope3D Envelope3D
    {
        get
        {
            OgrEnvelope3D? result = null;
            OgrApiH.OGR_G_GetEnvelope3D(this, ref result);
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

    [StructLayout(LayoutKind.Sequential)]
    private protected struct OgrGeometryRaw
    {
        private readonly nint _vFuncTablePtr1;
        private readonly nint _srsPtr;
        public uint Flags { get; set; }
    }

    public bool Equals(OgrGeometry? other)
    {
        if (other is null)
            return false;

        ObjectDisposedException.ThrowIf(Handle.IsClosed, this);
        ObjectDisposedException.ThrowIf(other.Handle.IsClosed, other);

        if (ReferenceEquals(this, other))
            return true;

        var ogrIsEqual = OgrApiH.OGR_G_Equals(this, other);
        GdalError.ThrowIfError();
        return ogrIsEqual;
    }

    public override bool Equals(object? obj)
        => this.Equals(obj as OgrGeometry);

    public static bool operator ==(OgrGeometry? lhs, OgrGeometry? rhs)
        => lhs switch
        {
            null when rhs is null => true,
            null => false,
            _ => lhs.Equals(rhs)
        };

    public static bool operator !=(OgrGeometry? lhs, OgrGeometry? rhs)
        => !(lhs == rhs);

    public override int GetHashCode()
    {
        ObjectDisposedException.ThrowIf(Handle.IsClosed, this);
        return Handle.DangerousGetHandle().GetHashCode();
    }
}