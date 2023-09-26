// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public partial class OgrFeature : GdalSafeHandle, IConstructibleHandle<OgrFeature>
{
    private OgrFeature(nint pointer, bool ownsHandle) : base(pointer, ownsHandle) { }
    public static OgrFeature Construct(nint pointer, bool ownsHandle) => new(pointer, ownsHandle);

    protected override bool ReleaseHandle()
    {
        if (Handle != nint.Zero)
        {
            Interop.OGR_F_Destroy(this);
            return true;
        }
        else return false;
    }
}
