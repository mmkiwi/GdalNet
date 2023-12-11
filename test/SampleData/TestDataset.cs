// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.SampleData;

public record TestDataset
{
    public IReadOnlyDictionary<string, string>? Options { get; init; }
    public string? SubPath { get; init; }
    public required int RasterCount { get; init; }
    public required int RasterXSize { get; init; }
    public required int RasterYSize { get; init; }
}
