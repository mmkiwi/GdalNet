// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public enum OgrFieldSubType
{
    /// <summary>
    /// No subtype. This is the default value.
    /// </summary>
    None = 0,
    /// <summary>
    /// Boolean integer. Only valid for <see cref="OgrFieldType.Integer"/> and <see cref="OgrFieldType.IntegerList"/>.
    /// </summary>
    Boolean = 1,
    /// <summary>
    /// Signed 16-bit integer. Only valid for <see cref="OgrFieldType.Integer"/> and <see cref="OgrFieldType.IntegerList"/>.
    /// </summary>
    Int16 = 2,
    /// <summary>
    /// Single precision (32 bit) floating point. Only valid for <see cref="OgrFieldType.Double"/> and <see cref="OgrFieldType.DoubleList"/>.
    /// </summary>
    Float32 = 3,
    /// <summary>
    /// JSON content. Only valid for <see cref="OgrFieldType.String"/>.
    /// </summary>
    JSON = 4,
    /// <summary>
    /// UUID string representation. Only valid for <see cref="OgrFieldType.String"/>.
    /// </summary>
    UUID = 5
}