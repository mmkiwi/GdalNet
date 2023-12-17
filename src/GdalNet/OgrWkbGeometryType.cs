// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public enum OgrWkbGeometryType
{
    /// <summary>
    /// unknown type, non-standard
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// 0-dimensional geometric object, standard
    /// </summary>
    Point = 1,

    ///<summary>
    ///1-dimensional geometric object with linear interpolation between Points, standard
    ///</summary>
    LineString = 2,

    ///<summary>
    ///  planar 2-dimensional geometric object defined
    ///  by 1 exterior boundary and 0 or more interior
    ///  boundaries, standard
    ///</summary>
    Polygon = 3,

    ///<summary>
    ///GeometryCollection of Points, standard
    ///</summary>
    MultiPoint = 4,

    ///<summary>
    ///GeometryCollection of LineStrings, standard
    ///</summary>
    MultiLineString = 5,

    /// <summary>
    /// GeometryCollection of Polygons, standard 
    /// </summary>
    MultiPolygon = 6,

    /// <summary>
    ///  geometric object that is a collection of 1 or more geometric objects, standard
    /// </summary>
    GeometryCollection = 7,

    /// <summary>
    /// one or more circular arc segments connected end to end, ISO SQL/MM Part 3.
    /// GDAL &gt;= 2.0
    /// </summary>
    CircularString = 8,

    /// <summary>
    /// sequence of contiguous curves, ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    CompoundCurve = 9,

    /// <summary>
    /// planar surface, defined by 1 exterior boundary and zero or more interior
    /// boundaries, that are curves. ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    CurvePolygon = 10,

    /// <summary>
    /// GeometryCollection of Curves, ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    MultiCurve = 11,

    /// <summary>
    /// GeometryCollection of Surfaces, ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    MultiSurface = 12,

    /// <summary>
    /// Curve (abstract type). ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    Curve = 13,

    /// <summary>
    ///  Surface (abstract type). ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    Surface = 14,

    /// <summary>
    /// a contiguous collection of polygons, which share common boundary
    /// segments, ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    PolyhedralSurface = 15,

    /// <summary>
    /// a PolyhedralSurface consisting only of Triangle patches
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TIN = 16,

    /// <summary>
    /// a Triangle. ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    Triangle = 17,

    /// <summary>
    /// non-standard, for pure attribute records
    /// </summary>
    None = 100,

    /// <summary>
    /// non-standard, just for createGeometry() 
    /// </summary>
    LinearRing = 101,

    /// <summary>
    /// CircularString with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    CircularStringZ = 1008,

    /// <summary>
    /// CompoundCurve with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    CompoundCurveZ = 1009,

    /// <summary>
    /// CurvePolygon with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    CurvePolygonZ = 1010,

    /// <summary>
    /// MultiCurve with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.0 
    /// </summary>
    MultiCurveZ = 1011,

    /// <summary>
    /// MultiSurface with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    MultiSurfaceZ = 1012,

    /// <summary>
    /// Curve with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CurveZ = 1013,

    /// <summary>
    /// Surface with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    SurfaceZ = 1014,

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    PolyhedralSurfaceZ = 1015,

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TINZ = 1016,

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TriangleZ = 1017,

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    PointM = 2001,              
    
    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    LineStringM = 2002,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    PolygonM = 2003,            

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiPointM = 2004,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiLineStringM = 2005,    

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiPolygonM = 2006,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    GeometryCollectionM = 2007, 

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CircularStringM = 2008,     

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CompoundCurveM = 2009,      

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CurvePolygonM = 2010,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiCurveM = 2011,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiSurfaceM = 2012,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CurveM = 2013,              

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    SurfaceM = 2014,            

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    PolyhedralSurfaceM = 2015,  
    
    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TINM = 2016,                

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TriangleM = 2017,           

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    PointZM = 3001,              

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    LineStringZM = 3002,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    PolygonZM = 3003,            

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiPointZM = 3004,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiLineStringZM = 3005,    

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiPolygonZM = 3006,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    GeometryCollectionZM = 3007, 

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CircularStringZM = 3008,     

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CompoundCurveZM = 3009,      

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CurvePolygonZM = 3010,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiCurveZM = 3011,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiSurfaceZM = 3012,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CurveZM = 3013,              

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    SurfaceZM = 3014,            

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    PolyhedralSurfaceZM = 3015,  

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TINZM = 3016,                

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TriangleZM = 3017,

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    Point25D = -2147483647,

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    LineString25D = -2147483646,        

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    Polygon25D = -2147483645,           

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    MultiPoint25D = -2147483644,        

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    MultiLineString25D = -2147483643,   

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    MultiPolygon25D = -2147483642,      

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    GeometryCollection25D = -2147483641 
}

public static class OgrWkbGeometryTypeExtensions
{
    const uint Wkb25DBitInternalUse = 0x80000000;
    public static OgrWkbGeometryType Flatten(this OgrWkbGeometryType geom)
    {
        uint eType = (uint)geom & (~Wkb25DBitInternalUse);
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
        return (eType & Wkb25DBitInternalUse) != 0 ||
               eType is >= 1000 and < 2000 ||
               eType is >= 3000 and < 4000;
    }

    public static bool HasM(this OgrWkbGeometryType geom)
        => (uint)geom switch
        {
            >= 2000 and < 3000 => true,
            >= 3000 and < 4000 => true,
            _ => false
        };
}