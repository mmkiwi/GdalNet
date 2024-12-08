// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public enum OgrFeatureValidation
{
    Null = 0b1,
    GeometryType = 0b10,
    Width = 0b100,
    AllowNullWhenDefault = 0b1000,
    All = 0b1111
}