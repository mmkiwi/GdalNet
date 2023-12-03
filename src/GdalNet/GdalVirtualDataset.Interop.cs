// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet;

public sealed partial class GdalVirtualDataset
{
    [CLSCompliant(false)]
    internal static partial class Interop
    {
        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint =nameof(VSIFileFromMemBuffer))]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        private unsafe static partial GdalVirtualDataset.MarshalHandle.Owns _VSIFileFromMemBuffer(string fileName,
                                                                byte* buffer,
                                                                long buffSize,
                                                                [MarshalAs(UnmanagedType.Bool)] bool takeOwnership);

        [GdalWrapperMethod(MethodName =nameof(_VSIFileFromMemBuffer))]
        public unsafe static partial GdalVirtualDataset VSIFileFromMemBuffer(string fileName,
                                                        byte* buffer,
                                                        long buffSize,
                                                        bool takeOwnership);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial int VSIFCloseL(nint dataset);
    }
}