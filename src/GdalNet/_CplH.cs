// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;

using MMKiwi.GdalNet.Error;

namespace MMKiwi.GdalNet;

[CLSCompliant(false)]
internal static partial class CplH
{
    [GdalEnforceErrorHandling(true)]
    internal unsafe static partial class String
    {
        [LibraryImport(GdalH.GdalDll)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        [GdalEnforceErrorHandling(false)]
        public static partial void CSLDestroy(byte** unmanaged);
    }
}