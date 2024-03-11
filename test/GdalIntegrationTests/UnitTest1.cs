using System.Text.Json;

namespace MMKiwi.GdalNet.GdalIntegrationTests;


[Collection("OGR collection")]
public class OgrIntegrationTests
{
    public OgrIntegrationTests(IntegrationFixture fixture)
    {
        Fixture = fixture;
    }
    public IntegrationFixture Fixture { get; }

    [Fact]
    public async Task OgrTest()
    {
        GdalInfo.RegisterAllDrivers();
        using GdalDataset gdalDataset = GdalDataset.Open(Fixture.GpkgPath);
        SimpleGeoJson.Root? geoJson;
        using (MemoryStream ms = new(SampleDataResources.PublicDomainGeojson))
        {
            geoJson = await JsonSerializer.DeserializeAsync(ms, SimpleGeoJson.Context.Default.Root) ?? throw new InvalidOperationException("Could not load geojson");
        }

        var layer = gdalDataset.Layers["tl_2022_10_prisecroads"];

        foreach (OgrFeature feature in layer.Features)
        {
            var fid = feature.Fid;
            var truthFeature = geoJson.Features.First(f => f.Properties["fid"].GetInt64() == fid);

            foreach ((var property, var value) in truthFeature.Properties)
            {
                throw new NotImplementedException();
            }
        }

    }

}

[CollectionDefinition("OGR collection")]
public class DatabaseCollection : ICollectionFixture<IntegrationFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
