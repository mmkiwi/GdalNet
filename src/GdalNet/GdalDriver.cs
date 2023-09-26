// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;

namespace MMKiwi.GdalNet;

public partial class GdalDriver : GdalHandle, IConstructibleHandle<GdalDriver>
{
    private GdalDriver(nint pointer) : base(pointer) { }
    public static GdalDriver Construct(nint pointer, bool ownsHandle)
    {
        ThrowIfOwnsHandle(ownsHandle, nameof(GdalDriver));
        return new(pointer);
    }
}
