// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(OgrEnvelopeMarshaller))]
public record class OgrEnvelope(double MinX, double MaxX, double MinY, double MaxY)
{
    public OgrEnvelope() : this(double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity) { }

    public bool IsEmpty => MinX == double.NegativeInfinity;

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
            else
            {
                return new(MinX: Math.Min(MinX, sOther.MinX),
                           MaxX: Math.Max(MaxX, sOther.MaxX),
                           MinY: Math.Max(MinY, sOther.MinY),
                           MaxY: Math.Min(MaxY, sOther.MaxY));

            }
        }
        else
        {
            return new();
        }
    }

    public bool Intersects(OgrEnvelope other)
        => MinX <= other.MaxX && MaxX >= other.MinX &&
           MinY <= other.MaxY && MaxY >= other.MinY;

    public bool Contains(OgrEnvelope other) 
        => MinX <= other.MinX && MinY <= other.MinY &&
           MaxX >= other.MaxX && MaxY >= other.MaxY;

    [CustomMarshaller(typeof(OgrEnvelope), MarshalMode.Default, typeof(OgrEnvelopeMarshaller))]
    internal static unsafe class OgrEnvelopeMarshaller
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct OGREnvelopeUnmanaged
        {
            public readonly double MinX { get; init; }
            public readonly double MaxX { get; init; }
            public readonly double MinY { get; init; }
            public readonly double MaxY { get; init; }
        }

        public static OGREnvelopeUnmanaged ConvertToUnmanaged(OgrEnvelope managed)
            => new OGREnvelopeUnmanaged
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