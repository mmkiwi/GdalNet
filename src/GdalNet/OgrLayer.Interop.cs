// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public partial class OgrLayer
{
    protected override bool ReleaseHandle() => true;

    internal static partial class Interop
    {
        [LibraryImport("gdal")]
        [return:MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_L_GetName(OgrLayer layer);
    }
}