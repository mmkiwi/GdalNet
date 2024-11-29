// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet.Geometry;

[NativeMarshalling(typeof(GdalMarshaller<OgrCurve, OgrGeometryHandle>))]
public class OgrCurve : OgrGeometry
{
    internal OgrCurve(OgrGeometryHandle handle) : base(handle)
    {

    }

    public double Length
    {
        get
        {
            var result = OgrApiH.OGR_G_Length(this);
            GdalError.ThrowIfError();
            return result;
        }
    }

    public OgrCurvePointCollection Points => new(this);
    public OgrPoint? GetValue(double distance)
    {
        var result = OgrApiH.OGR_G_Value(this, distance);
        GdalError.ThrowIfError();
        return result switch
        {
            OgrPoint point => point,
            null => null,
            _ => throw new GdalException($"Unexpected return type (expected Point, received {result.GeometryType}")
        };
    }


}

public class OgrCurvePointCollection : IList<OgrPoint>
{
    public OgrCurvePointCollection(OgrCurve curve)
    {
        Curve = curve;
    }
    public OgrCurve Curve { get; }
    public IEnumerator<OgrPoint> GetEnumerator()
    {
        bool isZ = Curve.Is3D;
        bool isM = ((int)Curve.GeometryType & 0x4) > 0;
        return new CurveEnumerator(Curve, Count, isZ, isM);
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public void Add(OgrPoint item)
    {

    }
    public void Clear()
    {
        throw new NotImplementedException();
    }
    public bool Contains(OgrPoint item)
    {
        throw new NotImplementedException();
    }
    public void CopyTo(OgrPoint[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }
    public bool Remove(OgrPoint item)
    {
        throw new NotImplementedException();
    }
    public int Count
    {
        get
        {
            var result = OgrApiH.OGR_G_GetPointCount(Curve);
            GdalError.ThrowIfError();
            return result;
        }
    }
    public bool IsReadOnly => throw new NotImplementedException();
    public int IndexOf(OgrPoint item)
    {
        throw new NotImplementedException();
    }
    public void Insert(int index, OgrPoint item)
    {
        throw new NotImplementedException();
    }
    public void RemoveAt(int index)
    {
        throw new NotSupportedException();
    }
    public OgrPoint this[int index]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    private sealed class CurveEnumerator : IEnumerator<OgrPoint>
    {
        public CurveEnumerator(OgrCurve curve, int count, bool isZ, bool isM)
        {
            Curve = curve;
            Count = count;
            IsZ = isZ;
            IsM = isM;
        }
        public OgrCurve Curve { get; }

        public int CurrentIndex { get; set; } = 0;
        public int Count { get; }
        public bool IsZ { get; }
        public bool IsM { get; }

        public void Dispose() { }
        public bool MoveNext()
        {
            return CurrentIndex >= 0 && ++CurrentIndex >= Count;
        }
        public void Reset()
        {
            CurrentIndex = 0;
        }
        public OgrPoint Current
        {
            get
            {
                if (CurrentIndex < 0)
                    throw new InvalidOperationException("Cannot have index below 0");
                if (CurrentIndex >= Count)
                    throw new InvalidOperationException("Cannot have index above count");
                OgrApiH.OGR_G_GetPointZM(Curve, CurrentIndex, out double x, out double y, out double z, out double m);
                GdalError.ThrowIfError();
                return (IsZ, IsM) switch
                {
                    (false, false) => OgrPoint.Create(x, y),
                    (true, true) => OgrPoint.CreateZM(x, y, z, m),
                    (true, false) => OgrPoint.CreateZ(x, y, z),
                    (false, true) => OgrPoint.CreateM(x, y, m)
                };
            }
        }
        object IEnumerator.Current => Current;
    }
}