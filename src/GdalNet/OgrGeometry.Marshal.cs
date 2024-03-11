// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet;

public abstract partial class OgrGeometry : IHasHandle<OgrGeometryHandle>, IConstructableWrapper<OgrGeometry, OgrGeometryHandle>
{
    private protected OgrGeometry(OgrGeometryHandle handle)
    {
        Handle = handle;
    }

    internal OgrGeometryHandle Handle { get; }
    OgrGeometryHandle IHasHandle<OgrGeometryHandle>.Handle => Handle;
    
    static OgrGeometry IConstructableWrapper<OgrGeometry, OgrGeometryHandle>.Construct(OgrGeometryHandle handle)
    {
        OgrWkbGeometryType type = OgrApiH.OGR_G_GetGeometryType(handle);
        GdalError.ThrowIfError();
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
}
