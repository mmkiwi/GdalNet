// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public static class OgrWkbGeometryTypeExtensions
{
    const uint Wkb25DBitInternalUse = 0x80000000;
    public static OgrWkbGeometryType Flatten(this OgrWkbGeometryType geom)
    {
        uint eType = (uint)geom & (~Wkb25DBitInternalUse);
        return eType switch
        {
            >= 1000 and < 2000 => (OgrWkbGeometryType)(eType - 1000),
            >= 2000 and < 3000 => (OgrWkbGeometryType)(eType - 2000),
            >= 3000 and < 4000 => (OgrWkbGeometryType)(eType - 3000),
            _ => geom
        };
    }

    public static bool HasZ(this OgrWkbGeometryType geom)
    {
        uint eType = (uint)geom;
        return (eType & Wkb25DBitInternalUse) != 0 ||
               eType is >= 1000 and < 2000 ||
               eType is >= 3000 and < 4000;
    }

    public static bool HasM(this OgrWkbGeometryType geom)
        => (uint)geom switch
        {
            >= 2000 and < 3000 => true,
            >= 3000 and < 4000 => true,
            _ => false
        };
}