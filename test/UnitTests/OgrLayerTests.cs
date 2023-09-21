﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet;
using MMKiwi.GdalNet.SampleData;
using MMKiwi.GdalNet.UnitTests;

namespace MMKiwi.GdalNet.UnitTests;

//[Collection("GDAL")]
public class OgrLayerTests: LayerTestBase
{
    [Theory]
    [MemberData(nameof(Layers))]
    public void TestLayerGetName(int index)
    {
        var layerInfo = GetLayer(index);
        using var virtualDataset = GdalVirtualDataset.Open(layerInfo.File.Data);
        var dataset = virtualDataset.Dataset;
        var layer = dataset.Layers[layerInfo.Layer.Name];
        layer.Name.Should().Be(layerInfo.Layer.Name);
    }

    [Theory]
    [MemberData(nameof(Layers))]
    public void TestLayerGetIndex(int index)
    {
        var layerInfo = GetLayer(index);
        using var virtualDataset = GdalVirtualDataset.Open(layerInfo.File.Data);
        var dataset = virtualDataset.Dataset;
        var layer = dataset.Layers[layerInfo.Layer.Index];
        layer.Name.Should().Be(layerInfo.Layer.Name);
    }
}

public class LayerTestBase: DatasetTestBase
{
    protected LayerInfo GetLayer(int index) => TestData.Layers[index];

    public static IEnumerable<object[]> Layers => Enumerable.Range(0, TestData.LayerCount).Select(x => new object[] { x });
}