// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public enum OgrWkbGeometryType
{
    Unknown = 0, /**< unknown type, non-standard */

    Point = 1,      /**< 0-dimensional geometric object, standard  */
    LineString = 2, /**< 1-dimensional geometric object with linear
                        *   interpolation between Points, standard  */
    Polygon = 3,    /**< planar 2-dimensional geometric object defined
                        *   by 1 exterior boundary and 0 or more interior
                        *   boundaries, standard  */
    MultiPoint = 4, /**< GeometryCollection of Points, standard  */
    MultiLineString =
        5,               /**< GeometryCollection of LineStrings, standard  */
    MultiPolygon = 6, /**< GeometryCollection of Polygons, standard  */
    GeometryCollection = 7, /**< geometric object that is a collection of 1
                                    or more geometric objects, standard  */

    CircularString = 8, /**< one or more circular arc segments connected end
                            * to end, ISO SQL/MM Part 3. GDAL &gt;= 2.0 */
    CompoundCurve = 9, /**< sequence of contiguous curves, ISO SQL/MM Part 3.
                             GDAL &gt;= 2.0 */
    CurvePolygon = 10, /**< planar surface, defined by 1 exterior boundary
                           *   and zero or more interior boundaries, that are
                           * curves. ISO SQL/MM Part 3. GDAL &gt;= 2.0 */
    MultiCurve = 11,   /**< GeometryCollection of Curves, ISO SQL/MM Part 3.
                             GDAL &gt;= 2.0 */
    MultiSurface = 12, /**< GeometryCollection of Surfaces, ISO SQL/MM
                             Part 3. GDAL &gt;= 2.0 */
    Curve =
        13, /**< Curve (abstract type). ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    Surface =
        14, /**< Surface (abstract type). ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    PolyhedralSurface =
        15,      /**< a contiguous collection of polygons, which share common
                  * boundary segments,      ISO SQL/MM Part 3. GDAL &gt;= 2.3 */
    TIN = 16, /**< a PolyhedralSurface consisting only of Triangle patches
                  *    ISO SQL/MM Part 3. GDAL &gt;= 2.3 */
    Triangle = 17, /**< a Triangle. ISO SQL/MM Part 3. GDAL &gt;= 2.3 */

    None = 100,       /**< non-standard, for pure attribute records */
    LinearRing = 101, /**< non-standard, just for createGeometry() */

    CircularStringZ = 1008, /**< CircularString with Z component. ISO
                                  SQL/MM Part 3. GDAL &gt;= 2.0 */
    CompoundCurveZ = 1009, /**< CompoundCurve with Z component. ISO SQL/MM
                                 Part 3. GDAL &gt;= 2.0 */
    CurvePolygonZ = 1010,  /**< CurvePolygon with Z component. ISO SQL/MM
                                 Part 3. GDAL &gt;= 2.0 */
    MultiCurveZ = 1011,    /**< MultiCurve with Z component. ISO SQL/MM
                                 Part 3. GDAL &gt;= 2.0 */
    MultiSurfaceZ = 1012,  /**< MultiSurface with Z component. ISO SQL/MM
                                 Part 3. GDAL &gt;= 2.0 */
    CurveZ = 1013,   /**< Curve with Z component. ISO SQL/MM Part 3. GDAL
                           &gt;= 2.1 */
    SurfaceZ = 1014, /**< Surface with Z component. ISO SQL/MM Part 3.
                           GDAL &gt;= 2.1 */
    PolyhedralSurfaceZ = 1015, /**< ISO SQL/MM Part 3. GDAL &gt;= 2.3 */
    TINZ = 1016,               /**< ISO SQL/MM Part 3. GDAL &gt;= 2.3 */
    TriangleZ = 1017,          /**< ISO SQL/MM Part 3. GDAL &gt;= 2.3 */

    PointM = 2001,              /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    LineStringM = 2002,         /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    PolygonM = 2003,            /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    MultiPointM = 2004,         /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    MultiLineStringM = 2005,    /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    MultiPolygonM = 2006,       /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    GeometryCollectionM = 2007, /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    CircularStringM = 2008,     /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    CompoundCurveM = 2009,      /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    CurvePolygonM = 2010,       /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    MultiCurveM = 2011,         /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    MultiSurfaceM = 2012,       /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    CurveM = 2013,              /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    SurfaceM = 2014,            /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    PolyhedralSurfaceM = 2015,  /**< ISO SQL/MM Part 3. GDAL &gt;= 2.3 */
    TINM = 2016,                /**< ISO SQL/MM Part 3. GDAL &gt;= 2.3 */
    TriangleM = 2017,           /**< ISO SQL/MM Part 3. GDAL &gt;= 2.3 */

    PointZM = 3001,              /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    LineStringZM = 3002,         /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    PolygonZM = 3003,            /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    MultiPointZM = 3004,         /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    MultiLineStringZM = 3005,    /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    MultiPolygonZM = 3006,       /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    GeometryCollectionZM = 3007, /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    CircularStringZM = 3008,     /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    CompoundCurveZM = 3009,      /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    CurvePolygonZM = 3010,       /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    MultiCurveZM = 3011,         /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    MultiSurfaceZM = 3012,       /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    CurveZM = 3013,              /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    SurfaceZM = 3014,            /**< ISO SQL/MM Part 3. GDAL &gt;= 2.1 */
    PolyhedralSurfaceZM = 3015,  /**< ISO SQL/MM Part 3. GDAL &gt;= 2.3 */
    TINZM = 3016,                /**< ISO SQL/MM Part 3. GDAL &gt;= 2.3 */
    TriangleZM = 3017,           /**< ISO SQL/MM Part 3. GDAL &gt;= 2.3 */

    Point25D = -2147483647,             /**< 2.5D extension as per 99-402 */
    LineString25D = -2147483646,        /**< 2.5D extension as per 99-402 */
    Polygon25D = -2147483645,           /**< 2.5D extension as per 99-402 */
    MultiPoint25D = -2147483644,        /**< 2.5D extension as per 99-402 */
    MultiLineString25D = -2147483643,   /**< 2.5D extension as per 99-402 */
    MultiPolygon25D = -2147483642,      /**< 2.5D extension as per 99-402 */
    GeometryCollection25D = -2147483641 /**< 2.5D extension as per 99-402 */

}

public static class OgrWkbGeometryTypeExtensions
{
    const uint wkb25DBitInternalUse = 0x80000000;
    public static OgrWkbGeometryType Flatten(this OgrWkbGeometryType geom)
    {
        uint eType = (uint)geom & (~wkb25DBitInternalUse);
        return eType switch
        {
            >= 1000 and < 2000 => (OgrWkbGeometryType)(eType - 1000),
            >= 2000 and < 3000 => (OgrWkbGeometryType)(eType - 2000),
            >= 3000 and < 4000 => (OgrWkbGeometryType)(eType - 3000),
            _ => geom
        };
    }

    public static bool HasZ(this OgrWkbGeometryType geom)
    {
        uint eType = (uint)geom;
        return (eType & wkb25DBitInternalUse) != 0 ||
               eType >= 1000 && eType < 2000 ||
               eType >= 3000 && eType < 4000;
    }

    public static bool HasM(this OgrWkbGeometryType geom)
        => (uint)geom switch
        {
            >= 2000 and < 3000 => true,
            >= 3000 and < 4000 => true,
            _ => false
        };
}