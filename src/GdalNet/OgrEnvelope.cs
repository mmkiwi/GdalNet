// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(OgrEnvelopeMarshaller))]
public record OgrEnvelope(double MinX, double MaxX, double MinY, double MaxY)
{
    public OgrEnvelope() : this(double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity) { }

    public bool IsEmpty => double.IsNegativeInfinity(MinX);

    public OgrEnvelope Merge(OgrEnvelope other)
        => new(Math.Min(MinX, other.MinX),
               Math.Max(MaxX, other.MaxX),
               Math.Min(MinY, other.MinY),
               Math.Max(MaxY, other.MaxY));

    public OgrEnvelope Intersect(OgrEnvelope sOther)
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
                MaxY: Math.Min(MaxY, sOther.MaxY));
        }

        return new();
    }

    public bool Intersects(OgrEnvelope other)
        => MinX <= other.MaxX && MaxX >= other.MinX &&
           MinY <= other.MaxY && MaxY >= other.MinY;

    public bool Contains(OgrEnvelope other)
        => MinX <= other.MinX && MinY <= other.MinY &&
           MaxX >= other.MaxX && MaxY >= other.MaxY;

    [CustomMarshaller(typeof(OgrEnvelope), MarshalMode.Default, typeof(OgrEnvelopeMarshaller))]
    internal static class OgrEnvelopeMarshaller
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct OGREnvelopeUnmanaged
        {
            public double MinX;
            public double MaxX;
            public double MinY;
            public double MaxY;
        }

        public static OGREnvelopeUnmanaged ConvertToUnmanaged(OgrEnvelope? managed)
            => managed is null ? default :
            new OGREnvelopeUnmanaged
            {
                MinX = managed.MinX,
                MaxX = managed.MaxX,
                MinY = managed.MinY,
                MaxY = managed.MaxY
            };

        public static OgrEnvelope ConvertToManaged(OGREnvelopeUnmanaged unmanaged)
            => new(unmanaged.MinX, unmanaged.MinY, unmanaged.MaxX, unmanaged.MaxY);
    }
}
