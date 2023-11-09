// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalError.ThrowMarshal))]
public enum OgrError
{
    None = 0,
    NotEnoughData = 1,
    NotEnoughMemory = 2,
    UnsupportedGeometryType = 3,
    UnsupportedOperation = 4,
    CorruptData = 5,
    Falure = 6,
    UnsupportedSRS = 7,
    InvalidHandle = 8,
    NonExistingFeature = 9
}