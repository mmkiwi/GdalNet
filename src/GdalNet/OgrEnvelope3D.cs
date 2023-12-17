// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(OgrEnvelope3dMarshaller))]
public record OgrEnvelope3D(double MinX, double MaxX, double MinY, double MaxY, double MinZ, double MaxZ)
{
    public OgrEnvelope3D() : this(double.NegativeInfinity,
                                  double.PositiveInfinity,
                                  double.NegativeInfinity,
                                  double.PositiveInfinity,
                                  double.NegativeInfinity,
                                  double.PositiveInfinity) { }

    public bool IsEmpty => double.IsNegativeInfinity(MinX);

    public OgrEnvelope3D Merge(OgrEnvelope3D other)
        => new(Math.Min(MinX, other.MinX),
               Math.Max(MaxX, other.MaxX),
               Math.Min(MinY, other.MinY),
               Math.Max(MaxY, other.MaxY),
               Math.Min(MinZ, other.MinZ),
               Math.Max(MaxZ, other.MaxZ));

    public OgrEnvelope3D Merge(OgrEnvelope other)
        => new(Math.Min(MinX, other.MinX),
               Math.Max(MaxX, other.MaxX),
               Math.Min(MinY, other.MinY),
               Math.Max(MaxY, other.MaxY),
               MinZ,
               MaxZ);

    public OgrEnvelope3D Intersect(OgrEnvelope3D sOther)
    {
        if (Intersects(sOther))
        {
            if (IsEmpty)
            {
                return sOther;
            }

            return new(MinX: Math.Min(MinX, sOther.MinX),
                MaxX: Math.Max(MaxX, sOther.MaxX),
                MinY: Math.Max(MinY, sOther.MinY),
                MaxY: Math.Min(MaxY, sOther.MaxY),
                MinZ: Math.Max(MinZ, sOther.MinZ),
                MaxZ: Math.Min(MaxZ, sOther.MaxZ));
        }

        return new();
    }

    public bool Intersects(OgrEnvelope3D other)
        => MinX <= other.MaxX && MaxX >= other.MinX &&
           MinY <= other.MaxY && MaxY >= other.MinY &&
           MinZ <= other.MaxZ && MaxZ >= other.MinZ;

    public bool Contains(OgrEnvelope3D other)
        => MinX <= other.MinX && MinY <= other.MinY &&
           MaxX >= other.MaxX && MaxY >= other.MaxY &&
           MinZ <= other.MinZ && MaxZ >= other.MaxZ;

    [CustomMarshaller(typeof(OgrEnvelope3D), MarshalMode.Default, typeof(OgrEnvelope3dMarshaller))]
    internal static class OgrEnvelope3dMarshaller
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct OGREnvelopeUnmanaged3d
        {
            public double MinX;
            public double MaxX;
            public double MinY;
            public double MaxY;
            public double MinZ;
            public double MaxZ;
        }

        public static OGREnvelopeUnmanaged3d ConvertToUnmanaged(OgrEnvelope3D? managed)
            => managed is null ? default :
            new OGREnvelopeUnmanaged3d
            {
                MinX = managed.MinX,
                MaxX = managed.MaxX,
                MinY = managed.MinY,
                MaxY = managed.MaxY,
                MinZ = managed.MinZ,
                MaxZ = managed.MaxZ
            };

        public static OgrEnvelope3D ConvertToManaged(OGREnvelopeUnmanaged3d unmanaged)
            => new(unmanaged.MinX, unmanaged.MinY, unmanaged.MaxX, unmanaged.MaxY, unmanaged.MinZ, unmanaged.MaxZ);
    }
}