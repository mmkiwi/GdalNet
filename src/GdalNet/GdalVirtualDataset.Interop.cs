// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

public sealed partial class GdalVirtualDataset
{
    [CLSCompliant(false)]
    internal static partial class Interop
    {
        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8, EntryPoint =nameof(VSIFileFromMemBuffer))]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        private unsafe static partial GdalVirtualDatasetHandle.Owns _VSIFileFromMemBuffer(string fileName,
                                                                byte* buffer,
                                                                long buffSize,
                                                                [MarshalAs(UnmanagedType.Bool)] bool takeOwnership);

        [GdalWrapperMethod(MethodName =nameof(_VSIFileFromMemBuffer))]
        public unsafe static partial GdalVirtualDataset? VSIFileFromMemBuffer(string fileName,
                                                        byte* buffer,
                                                        long buffSize,
                                                        bool takeOwnership);
    }
}