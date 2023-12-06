// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.Contracts;
using System.Reflection;

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper(ConstructorVisibility = MemberVisibility.PrivateProtected)]
public abstract partial class OgrGeometry : IHasHandle<OgrGeometry.MarshalHandle>, IConstructableWrapper<OgrGeometry, OgrGeometry.MarshalHandle>
{
    internal abstract class MarshalHandle : GdalInternalHandle, IConstructableHandle<MarshalHandle>
    {
        private MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        static MarshalHandle IConstructableHandle<MarshalHandle>.Construct(bool ownsHandle) => ownsHandle ? new Owns() : new DoesntOwn();

        protected override GdalCplErr? ReleaseHandleCore()
        {
            Interop.OGR_G_DestroyGeometry(handle);
        }

        public sealed class Owns : MarshalHandle { public Owns() : base(true) { } }
        public sealed class DoesntOwn : MarshalHandle { public DoesntOwn() : base(true) { } }
    }

    static OgrGeometry IConstructableWrapper<OgrGeometry, MarshalHandle>.Construct(MarshalHandle handle)
    {
        OgrWkbGeometryType type = Interop.OGR_G_GetGeometryType(handle);
        return type switch
        {
            OgrWkbGeometryType.Point => new OgrPoint(handle),//
            OgrWkbGeometryType.LineString => throw new NotImplementedException(),//
            OgrWkbGeometryType.Polygon => throw new NotImplementedException(),//
            OgrWkbGeometryType.MultiPoint => throw new NotImplementedException(),//
            OgrWkbGeometryType.MultiLineString => throw new NotImplementedException(),//
            OgrWkbGeometryType.MultiPolygon => throw new NotImplementedException(),//
            OgrWkbGeometryType.GeometryCollection => throw new NotImplementedException(),//
            OgrWkbGeometryType.CircularString => throw new NotImplementedException(),//
            OgrWkbGeometryType.CompoundCurve => throw new NotImplementedException(),//
            OgrWkbGeometryType.CurvePolygon => throw new NotImplementedException(),//
            OgrWkbGeometryType.MultiCurve => throw new NotImplementedException(),//
            OgrWkbGeometryType.MultiSurface => throw new NotImplementedException(),//
            OgrWkbGeometryType.Curve => throw new NotImplementedException(),
            OgrWkbGeometryType.Surface => throw new NotImplementedException(),
            OgrWkbGeometryType.PolyhedralSurface => throw new NotImplementedException(),//
            OgrWkbGeometryType.TIN => throw new NotImplementedException(),//
            OgrWkbGeometryType.Triangle => throw new NotImplementedException(),//
            OgrWkbGeometryType.LinearRing => throw new NotImplementedException(),//
            _ => new UnknownGeometry(handle),
        };
    }

    [GdalGenerateWrapper]
    private partial class UnknownGeometry : OgrGeometry, IConstructableWrapper<UnknownGeometry, OgrGeometry.MarshalHandle>
    {
        public UnknownGeometry(MarshalHandle handle) : base(handle)
        {
        }
    }
}
