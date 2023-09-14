// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public enum GdalDataType
{
    /*! Unknown or unspecified type */
    Unknown = 0,
    /*! Eight bit unsigned integer */
    Byte = 1,
    /*! 8-bit signed integer (GDAL >= 3.7) */
    Int8 = 14,
    /*! Sixteen bit unsigned integer */
    UInt16 = 2,
    /*! Sixteen bit signed integer */
    Int16 = 3,
    /*! Thirty two bit unsigned integer */
    UInt32 = 4,
    /*! Thirty two bit signed integer */
    Int32 = 5,
    /*! 64 bit unsigned integer (GDAL >= 3.5)*/
    UInt64 = 12,
    /*! 64 bit signed integer  (GDAL >= 3.5)*/
    Int64 = 13,
    /*! Thirty two bit floating point */
    Float32 = 6,
    /*! Sixty four bit floating point */
    Float64 = 7,
    /*! Complex Int16 */
    CInt16 = 8,
    /*! Complex Int32 */
    CInt32 = 9,
    /* TODO?(#6879): GDT_CInt64 */
    /*! Complex Float32 */
    CFloat32 = 10,
    /*! Complex Float64 */
    CFloat64 = 11,
    TypeCount = 15 /* maximum type # + 1 */
}

