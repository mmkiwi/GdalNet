// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.Win32.SafeHandles;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal<GdalRasterBand>))]
public sealed partial class GdalRasterBand: GdalHandle
{
    private GdalRasterBand()
    {
    }

    public string[] Categories => Interop.GDALGetRasterCategoryNames(this);
    public GdalDataType DataType => Interop.GDALGetRasterDataType(this);

}

