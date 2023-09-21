// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal<GdalDriver>))]
public partial class GdalDriver : GdalHandle, IConstructibleHandle<GdalDriver>
{
    private GdalDriver(nint pointer) => SetHandle(pointer);
    public static GdalDriver Construct(nint pointer) => new(pointer);
}
