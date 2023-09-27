// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public sealed partial class GdalVirtualDataset
{
    [CLSCompliant(false)]
    internal static partial class Interop
    {
        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalUsing(typeof(MarshalOwnsHandle))]
        public unsafe static partial GdalVirtualDataset VSIFileFromMemBuffer(string fileName,
                                                                byte* buffer,
                                                                long buffSize,
                                                                [MarshalAs(UnmanagedType.Bool)] bool takeOwnership);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial int VSIFCloseL(GdalVirtualDataset dataset);
    }
}