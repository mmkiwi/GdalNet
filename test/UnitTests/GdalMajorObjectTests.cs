// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using FluentAssertions.Execution;

namespace MMKiwi.GdalNet.UnitTests;

[Collection("Gdal DLL")]
public sealed class GdalMajorObjectTests(GdalDllFixture fixture) : DatasetTestBase(fixture)
{
    [Theory]
    [MemberData(nameof(Datasets))]
    public void DescriptionTest(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;
        dataset.Description.Should().NotBeNull();
        dataset.Description = "Test 123";
        dataset.Description.Should().Be("Test 123");
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void MetadataItemTest(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.SetMetadataItem("Test1", "Value1", "IntegrationTests");
        dataset.GetMetadataItem("Test1", "IntegrationTests").Should().Be("Value1");
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void MetadataItemNullDomainTest(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.SetMetadataItem("Test1", "Value1");
        dataset.GetMetadataItem("Test1").Should().Be("Value1");
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void MetadataTest(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.SetMetadata(new Dictionary<string, string> { { "Test1", "Value1" } }, "IntegrationTests");
        dataset.GetMetadata("IntegrationTests").Should().Contain(new KeyValuePair<string, string>("Test1", "Value1"));
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void MetadataNulLDomainTest(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.SetMetadataItem("Test1", "Value1");
        dataset.GetMetadata().Should().Contain(new KeyValuePair<string, string>("Test1", "Value1"));
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void MetadataEmptyDomainTest(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.SetMetadataItem("Test1", "Value1", "");
        dataset.GetMetadata("").Should().ContainEquivalentOf(new KeyValuePair<string, string>("Test1", "Value1"));
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void SetMetadataThrowIfNull(int index)
    {
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        var action = () => dataset.SetMetadataItem(null!, "");
        action.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void SetMetadataItemThrowIfNull(int index)
    {
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        var action = () => dataset.SetMetadataItem(null!, "Value1", "");
        action.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void SetNotThrowOnNullValue(int index)
    {
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        var action = () => dataset.SetMetadataItem("test", null!, "");
        action.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void MetadataMultipleDomainsTest(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.SetMetadataItem("Test1", "Value1", "Domain1");
        dataset.SetMetadataItem("Test2", "Value2", "Domain2");
        dataset.GetMetadata("Domain1").Should().BeEquivalentTo(new Dictionary<string, string> { { "Test1", "Value1" } });
        dataset.GetMetadata("Domain2").Should().BeEquivalentTo(new Dictionary<string, string> { { "Test2", "Value2" } });
    }

    [Theory]
    [MemberData(nameof(Datasets))]
    public void MetadataDomainListTest(int index)
    {
        using var _ = new AssertionScope();
        var datasetInfo = GetDataset(index);
        using var virtualDataset = GdalVirtualDataset.Open(datasetInfo.File.Data, openOptions: datasetInfo.Dataset.Options);
        var dataset = virtualDataset.Dataset;

        dataset.SetMetadataItem("Test1", "Value1", "Domain1");
        dataset.SetMetadataItem("Test2", "Value2", "Domain2");

        dataset.MetadataDomainList.Should().Contain("Domain1");
        dataset.MetadataDomainList.Should().Contain("Domain2");
    }
}