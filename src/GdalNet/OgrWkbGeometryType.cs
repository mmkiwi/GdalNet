// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public enum OgrWkbGeometryType
{
    /// <summary>
    /// unknown type, non-standard
    /// </summary>
    Unknown = 0b0,

    /// <summary>
    /// 0-dimensional geometric object, standard
    /// </summary>
    Point = 0b1,

    ///<summary>
    ///1-dimensional geometric object with linear interpolation between Points, standard
    ///</summary>
    LineString = 0b10,

    ///<summary>
    ///  planar 2-dimensional geometric object defined
    ///  by 1 exterior boundary and 0 or more interior
    ///  boundaries, standard
    ///</summary>
    Polygon = 0b11,

    ///<summary>
    ///GeometryCollection of Points, standard
    ///</summary>
    MultiPoint = 0b100,

    ///<summary>
    ///GeometryCollection of LineStrings, standard
    ///</summary>
    MultiLineString = 0b101,

    /// <summary>
    /// GeometryCollection of Polygons, standard 
    /// </summary>
    MultiPolygon = 0b110,

    /// <summary>
    ///  geometric object that is a collection of 1 or more geometric objects, standard
    /// </summary>
    GeometryCollection = 0b111,

    /// <summary>
    /// one or more circular arc segments connected end to end, ISO SQL/MM Part 3.
    /// GDAL &gt;= 2.0
    /// </summary>
    CircularString = 0b1000,

    /// <summary>
    /// sequence of contiguous curves, ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    CompoundCurve = 0b1001,

    /// <summary>
    /// planar surface, defined by 1 exterior boundary and zero or more interior
    /// boundaries, that are curves. ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    CurvePolygon = 0b1010,

    /// <summary>
    /// GeometryCollection of Curves, ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    MultiCurve = 0b1011,

    /// <summary>
    /// GeometryCollection of Surfaces, ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    MultiSurface = 0b1100,

    /// <summary>
    /// Curve (abstract type). ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    Curve = 0b1101,

    /// <summary>
    ///  Surface (abstract type). ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    Surface = 0b1110,

    /// <summary>
    /// a contiguous collection of polygons, which share common boundary
    /// segments, ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    PolyhedralSurface = 0b1111,

    /// <summary>
    /// a PolyhedralSurface consisting only of Triangle patches
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TIN = 0b10000,

    /// <summary>
    /// a Triangle. ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    Triangle = 0b10001,

    /// <summary>
    /// non-standard, for pure attribute records
    /// </summary>
    None = 0b1100100,

    /// <summary>
    /// non-standard, just for createGeometry() 
    /// </summary>
    LinearRing = 0b1100101,

    /// <summary>
    /// CircularString with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    CircularStringZ = 0b1111110000,

    /// <summary>
    /// CompoundCurve with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    CompoundCurveZ = 0b1111110001,

    /// <summary>
    /// CurvePolygon with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    CurvePolygonZ = 0b1111110010,

    /// <summary>
    /// MultiCurve with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.0 
    /// </summary>
    MultiCurveZ = 0b1111110011,

    /// <summary>
    /// MultiSurface with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.0
    /// </summary>
    MultiSurfaceZ = 0b1111110100,

    /// <summary>
    /// Curve with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CurveZ = 0b1111110101,

    /// <summary>
    /// Surface with Z component. ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    SurfaceZ = 0b1111110110,

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    PolyhedralSurfaceZ = 0b1111110111,

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TINZ = 0b1111111000,

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TriangleZ = 0b1111111001,

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    PointM = 0b11111010001,              
    
    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    LineStringM = 0b11111010010,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    PolygonM = 0b11111010011,            

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiPointM = 0b11111010100,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiLineStringM = 0b11111010101,    

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiPolygonM = 0b11111010110,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    GeometryCollectionM = 0b11111010111, 

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CircularStringM = 0b11111011000,     

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CompoundCurveM = 0b11111011001,      

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CurvePolygonM = 0b11111011010,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiCurveM = 0b11111011011,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiSurfaceM = 0b11111011100,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CurveM = 0b11111011101,              

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    SurfaceM = 0b11111011110,            

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    PolyhedralSurfaceM = 0b11111011111,  
    
    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TINM = 0b11111100000,                

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TriangleM = 0b11111100001,           

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    PointZM = 0b101110111001,              

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    LineStringZM = 0b101110111010,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    PolygonZM = 0b101110111011,            

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiPointZM = 0b101110111100,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiLineStringZM = 0b101110111101,    

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiPolygonZM = 0b101110111110,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    GeometryCollectionZM = 0b101110111111, 

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CircularStringZM = 0b101111000000,     

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CompoundCurveZM = 0b101111000001,      

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CurvePolygonZM = 0b101111000010,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiCurveZM = 0b101111000011,         

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    MultiSurfaceZM = 0b101111000100,       

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    CurveZM = 0b101111000101,              

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.1
    /// </summary>
    SurfaceZM = 0b101111000110,            

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    PolyhedralSurfaceZM = 0b101111000111,  

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TINZM = 0b101111001000,                

    /// <summary>
    /// ISO SQL/MM Part 3. GDAL &gt;= 2.3
    /// </summary>
    TriangleZM = 0b101111001001,

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    Point25D = -0b1111111111111111111111111111111,

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    LineString25D = -0b1111111111111111111111111111110,        

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    Polygon25D = -0b1111111111111111111111111111101,           

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    MultiPoint25D = -0b1111111111111111111111111111100,        

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    MultiLineString25D = -0b1111111111111111111111111111011,   

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    MultiPolygon25D = -0b1111111111111111111111111111010,      

    /// <summary>
    /// 2.5D extension as per 99-402
    /// </summary>
    GeometryCollection25D = -0b1111111111111111111111111111001 
}