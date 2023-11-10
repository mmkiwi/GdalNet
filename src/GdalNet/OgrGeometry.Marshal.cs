// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal.In))]
public abstract partial class OgrGeometry
{
    internal static partial class Marshal
    {
        [CustomMarshaller(typeof(OgrGeometry), MarshalMode.Default, typeof(In))]
        internal static partial class In
        {
            public static nint ConvertToUnmanaged(OgrGeometry? handle) => handle is null ? 0 : handle.Handle;
        }


        [CustomMarshaller(typeof(OgrGeometry), MarshalMode.Default, typeof(DoesNotOwnHandle))]
        internal static partial class DoesNotOwnHandle
        {
            public static nint ConvertToUnmanaged(OgrGeometry? handle) => handle is null ? 0 : handle.Handle;
            public static OgrGeometry? ConvertToManaged(nint pointer)
            {
                return GetConcrete(pointer, false);
            }
        }

        private static OgrGeometry? GetConcrete(nint pointer, bool ownsHandle)
        {
            if (pointer == 0)
                return null;
            else {
                OgrWkbGeometryType type = Interop.OGR_G_GetGeometryType(pointer);
                return type switch
                {
                    OgrWkbGeometryType.Point => new OgrPoint(pointer, ownsHandle),//
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
                    _ => new UnknownGeometry(pointer, ownsHandle),
                };
            }
        }

        private class UnknownGeometry : OgrGeometry
        {
            public UnknownGeometry(nint pointer, bool ownsHandle) : base(pointer, ownsHandle)
            {
            }
        }

        [CustomMarshaller(typeof(OgrGeometry), MarshalMode.Default, typeof(OwnsHandle))]
        internal static partial class OwnsHandle
        {
            public static nint ConvertToUnmanaged(OgrGeometry? handle) => handle is null ? 0 : handle.Handle;
            public static OgrGeometry? ConvertToManaged(nint pointer)
            {
                return GetConcrete(pointer, true);
            }
        }
    }
}
