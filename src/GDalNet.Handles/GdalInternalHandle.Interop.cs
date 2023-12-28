// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;

using Microsoft.Win32.SafeHandles;

namespace MMKiwi.GdalNet.Handles;

internal abstract partial class GdalInternalHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private protected GdalInternalHandle(bool ownsHandle) : base(ownsHandle)
    {

    }

    protected override bool ReleaseHandle()
    {
        return ReleaseHandleCore() is { } and not GdalCplErr.Failure and not GdalCplErr.Fatal;
    }

    private protected abstract GdalCplErr? ReleaseHandleCore();

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private protected static partial class Interop
    {
        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial GdalCplErr GDALClose(nint dataset);

        [LibraryImport("gdal")]
        public static partial void OGR_F_Destroy(nint feature);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial void CSLDestroy(nint unmanaged);

        [LibraryImport("gdal")]
        public static partial void OGR_FldDomain_Destroy(nint domain);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial int VSIFCloseL(nint dataset);

        [LibraryImport("gdal")]
        public unsafe static partial void OGR_G_DestroyGeometry(nint geometry);
    }
}