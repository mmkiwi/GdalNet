// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;

namespace MMKiwi.GdalNet;

[CLSCompliant(false)]
internal static partial class CplH
{
    internal unsafe static partial class String
    {
        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial void CSLDestroy(byte** unmanaged);
    }
}