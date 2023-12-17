// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.InteropAttributes;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public static partial class GdalInfo
{
    [CLSCompliant(false)]
    internal static partial class Interop
    {
        static Interop() => GdalError.EnsureInitialize();

        [LibraryImport("gdal", EntryPoint = nameof(GDALVersionInfo))]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string _GDALVersionInfo(ReadOnlySpan<byte> requestType);

        [GdalWrapperMethod(MethodName = nameof(_GDALVersionInfo))]
        public static partial string GDALVersionInfo(ReadOnlySpan<byte> requestType);

        [LibraryImport("gdal", EntryPoint = nameof(GDALAllRegister))]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        private static partial void _GDALAllRegister();

        [GdalWrapperMethod(MethodName = nameof(_GDALAllRegister))]
        public static partial void GDALAllRegister();
    }
}

