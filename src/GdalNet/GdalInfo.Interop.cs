// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public static partial class GdalInfo
{
    [CLSCompliant(false)]
    internal unsafe static partial class Interop
    {
        static Interop() => GdalError.EnsureInitialize();

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        internal static partial string GDALVersionInfo(ReadOnlySpan<byte> requestType);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        internal static partial void GDALAllRegister();
    }
}

