using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MMKiwi.GdalNet.GdalIntegrationTests;

public static partial class SimpleGeoJson
{

    public class Root
    {
        public required string Type { get; init; }
        public required string Name { get; init; }
        public required Crs Crs { get; init; }
        public required ImmutableList<Feature> Features { get; init; }
    }

    public class Crs
    {
        public required string Type { get; init; }
        public required CrsProperties Properties { get; init; }
    }

    public class CrsProperties
    {
        public required string Name { get; set; }
    }

    public class Feature
    {
        public required string Type { get; init; }
        public required ImmutableDictionary<string,JsonElement> Properties { get; init; }
        public required Geometry Geometry { get; init; }
    }

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(PointGeometry), typeDiscriminator: "Point")]
    [JsonDerivedType(typeof(LineStringGeometry), typeDiscriminator: "LineString")]
    [JsonDerivedType(typeof(PolygonGeometry), typeDiscriminator: "Polygon")]
    [JsonDerivedType(typeof(MultiPointGeometry), typeDiscriminator: "MultiPoint")]
    [JsonDerivedType(typeof(MultiPolygonGeometry), typeDiscriminator: "MultiPolygon")]
    [JsonDerivedType(typeof(MultiLineStringGeometry), typeDiscriminator: "MultiLineString")]
    [JsonDerivedType(typeof(GeometryCollection), typeDiscriminator: "GeometryCollection")]
    public abstract class Geometry
    {

    }


    public class PointGeometry: Geometry
    {
        public required double[] Coordinates { get; init; }
    }

    public class LineStringGeometry : Geometry
    {
        public required double[][] Coordinates { get; init; }
    }

    public class PolygonGeometry : Geometry
    {
        public required double[][][] Coordinates { get; init; }
    }

    public class MultiPointGeometry : Geometry
    {
        public required double[][] Coordinates { get; init; }
    }

    public class MultiPolygonGeometry : Geometry
    {
        public required double[][][][] Coordinates { get; init; }
    }

    public class MultiLineStringGeometry : Geometry
    {
        public required double[][][] Coordinates { get; init; }
    }

    public class GeometryCollection : Geometry
    {
        public required Geometry[] Geometries { get; init; }
    }


    [JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    [JsonSerializable(typeof(Root))]
    internal partial class Context : JsonSerializerContext
    {
    }
}