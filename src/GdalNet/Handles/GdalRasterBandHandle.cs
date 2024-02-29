// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet.Handles;

internal sealed class GdalRasterBandHandle : GdalInternalHandle.NeverOwns, IConstructableHandleNeverOwns<GdalRasterBandHandle>
{
    public static GdalRasterBandHandle Construct() => new();
}