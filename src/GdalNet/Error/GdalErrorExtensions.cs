// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet.Error;
internal static class GdalErrorExtensions
{
    public static void ThrowIfError(this GdalCplErr error)
    {
        GdalError.ThrowIfError(error);
    }

    public static void ThrowIfError(this OgrError error)
    {
        GdalError.ThrowIfError(error);
    }
}
