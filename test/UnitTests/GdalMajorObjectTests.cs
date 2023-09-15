// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Data;

using FluentAssertions.Execution;

namespace MMKiwi.GdalNet.UnitTests;

public sealed class GdalMajorObjectTests : IDisposable
{
    public GdalMajorObjectTests()
    {
        GdalInfo.RegisterAllDrivers();
        FilePath = Path.GetTempFileName();
        using FileStream sampleFile = File.OpenWrite(FilePath);
        sampleFile.Write(SampleFiles.Sample_0);
    }

    string FilePath { get; }

    public void Dispose()
    {
        File.Delete(FilePath);
    }

    [Fact]
    public void DescriptionTest()
    {
        using var _ = new AssertionScope();
        using GdalMajorObject dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly)!;

        dataset.Description.Should().NotBeNull();
        dataset.Description = "Test 123";
        dataset.Description.Should().Be("Test 123");
    }

    [Fact]
    public void MetadataItemTest()
    {
        using var _ = new AssertionScope();
        using GdalMajorObject dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly)!;

        dataset.GetMetadataItem("Test1", "IntegrationTests").Should().BeNull();
        dataset.SetMetadataItem("Test1", "Value1", "IntegrationTests");
        dataset.GetMetadataItem("Test1", "IntegrationTests").Should().Be("Value1");
    }

    [Fact]
    public void MetadataItemNullDomainTest()
    {
        using var _ = new AssertionScope();
        using GdalMajorObject dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly)!;

        dataset.GetMetadataItem("Test1").Should().BeNull();
        dataset.SetMetadataItem("Test1", "Value1");
        dataset.GetMetadataItem("Test1").Should().Be("Value1");
    }

    [Fact]
    public void MetadataTest()
    {
        using var _ = new AssertionScope();
        using GdalMajorObject dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly)!;

        dataset.GetMetadata("IntegrationTests").Should().BeNull();
        dataset.SetMetadataItem("Test1", "Value1", "IntegrationTests");
        dataset.GetMetadata("IntegrationTests").Should().BeEquivalentTo(new Dictionary<string, string> { { "Test1", "Value1" } });
    }

    [Fact]
    public void MetadataNulLDomainTest()
    {
        using var _ = new AssertionScope();
        using GdalMajorObject dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly)!;

        dataset.GetMetadata().Should().BeNull();
        dataset.SetMetadataItem("Test1", "Value1");
        dataset.GetMetadata().Should().BeEquivalentTo(new Dictionary<string, string> { { "Test1", "Value1" } });
    }


    [Fact]
    public void MetadataEmptyDomainTest()
    {
        using var _ = new AssertionScope();
        using GdalMajorObject dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly)!;

        dataset.GetMetadata("").Should().BeNull();
        dataset.SetMetadataItem("Test1", "Value1", "");
        dataset.GetMetadata("").Should().BeEquivalentTo(new Dictionary<string, string> { { "Test1", "Value1" } });
    }

    [Fact]
    public void MetadataMultipleDomainsTest()
    {
        using var _ = new AssertionScope();
        using GdalMajorObject dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly)!;

        dataset.GetMetadata().Should().BeNull();
        dataset.SetMetadataItem("Test1", "Value1", "Domain1");
        dataset.SetMetadataItem("Test2", "Value2", "Domain2");
        dataset.GetMetadata("Domain1").Should().BeEquivalentTo(new Dictionary<string, string> { { "Test1", "Value1" } });
        dataset.GetMetadata("Domain2").Should().BeEquivalentTo(new Dictionary<string, string> { { "Test2", "Value2" } });
    }


    [Fact]
    public void MetadataDomainListTest()
    {
        using var _ = new AssertionScope();
        using GdalMajorObject dataset = GdalDataset.Open(FilePath, GdalAccess.ReadOnly)!;

        string[] originalDomains = dataset.MetadataDomainList;
        dataset.SetMetadataItem("Test1", "Value1", "Domain1");
        dataset.SetMetadataItem("Test2", "Value2", "Domain2");

        dataset.MetadataDomainList.Should().BeEquivalentTo(["Domain1", "Domain2", ..originalDomains]);
    }
}