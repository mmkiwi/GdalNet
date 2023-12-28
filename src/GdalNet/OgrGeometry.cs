// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Handles;

namespace MMKiwi.GdalNet;
public abstract partial class OgrGeometry: IDisposable
{
    private bool _disposedValue;

    public static OgrGeometry CreateFromWkb(ReadOnlySpan<byte> wkb, OgrSpatialReference? spatialReference = null)
    {
        var err = OgrApiH.OGR_G_CreateFromWkb(wkb, spatialReference, out OgrGeometry result, wkb.Length);
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
                var result = OgrApiH.OGR_G_CreateFromWkt(ref wktPointer, spatialReference, out geometry);
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
        get => OgrApiH.OGR_G_GetDimension(this);
    }

    public int CoordinateDimension
    {
        get => OgrApiH.OGR_G_CoordinateDimension(this);
    }

    public bool Is3D
    {
        get => OgrApiH.OGR_G_Is3D(this);
        set => OgrApiH.OGR_G_Set3D(this, value);
    }

    public OgrGeometry Clone()
        => OgrApiH.OGR_G_Clone(this);

    public OgrEnvelope Envelope
    {
        get
        {
            OgrApiH.OGR_G_GetEnvelope(this, out OgrEnvelope? result);
            return result;
        }
    }

    public OgrWkbGeometryType GeometryType
    {
        get =>OgrApiH.OGR_G_GetGeometryType(this);
    }

    public OgrEnvelope3D Envelope3D
    {
        get
        {
            OgrEnvelope3D? result = null;
            OgrApiH.OGR_G_GetEnvelope3D(this, ref result);
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
        private nint _vFuncTablePtr1;
        private nint _srsPtr;
        public uint Flags { get; set; }
    }

    public bool Equals(OgrGeometry? other)
    {
        if (other is null)
            return false;

        ObjectDisposedException.ThrowIf(Handle.IsClosed, this);
        ObjectDisposedException.ThrowIf(other.Handle.IsClosed, this);

        return ReferenceEquals(this, other) || OgrApiH.OGR_G_Equals(this, other);
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