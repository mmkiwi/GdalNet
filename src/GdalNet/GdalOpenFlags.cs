// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[Flags]
internal enum GdalOpenFlags
{
    None = 0,
    Update = 0x01,
    Raster = 0x02,
    Vector = 0x04,
    Gnm = 0x08,
    MultidimensionalRaster = 0x10,
    Shared = 0x20,
    VerboseError = 0x40,
    Internal = 0x80
}