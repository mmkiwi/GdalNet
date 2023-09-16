// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;

namespace MMKiwi.GdalNet.SampleData;

public record class TestFile
{
    public required byte[] Data { get; init; }
    public required ImmutableArray<TestDataset> Datasets {  get; init; }
}
