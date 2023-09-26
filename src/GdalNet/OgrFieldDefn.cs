// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public partial class OgrFieldDefn : GdalHandle, IConstructibleHandle<OgrFieldDefn>
{
    private OgrFieldDefn(nint pointer) : base(pointer) { }
    public static OgrFieldDefn Construct(nint pointer, bool ownsHandle)
    {
        ThrowIfOwnsHandle(ownsHandle, nameof(OgrFieldDefn));
        return new(pointer);
    }
}