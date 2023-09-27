// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public enum OgrFieldType
{
    ///<summary>Simple 32bit integer</summary>
    Integer = 0,
    /// <summary>
    /// List of 32bit integers
    /// </summary>
    IntegerList = 1,
    /// <summary>
    /// Double Precision floating point
    /// </summary>
    Double = 2,
    /// <summary>
    /// List of doubles
    /// </summary>
    DoubleList = 3,
    /// <summary>
    /// String of UTF-8 chars
    /// </summary>
    String = 4,
    /// <summary>
    /// Array of strings
    /// </summary>
    StringList = 5,
    /// <summary>
    /// Raw Binary data
    /// </summary>
    Binary = 8,
    /// <summary>
    /// Date
    /// </summary>
    Date = 9,
    /// <summary>
    /// Time
    /// </summary>
    Time = 10,
    /// <summary>
    /// Date and Time
    /// </summary>
    DateTime = 11,
    /// <summary>
    /// Single 64bit integer
    /// </summary>
    Integer64 = 12,
    /// <summary>
    /// List of 64bit integers
    /// </summary>
    Integer64List = 13
}
