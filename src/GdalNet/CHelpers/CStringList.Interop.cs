// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet.CHelpers;

internal partial class CStringList
{
    [CLSCompliant(false)]
    public unsafe static partial class Interop
    {
        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial CStringList.MarshalHandle.Owns CSLAddString(CStringList.MarshalHandle strList, string newString);
        [GdalWrapperMethod]
        public static partial CStringList CSLAddString(CStringList? strList, string newString);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial CStringList.MarshalHandle.Owns CSLAddStringMayFail(CStringList.MarshalHandle strList, string newString);
        [GdalWrapperMethod] 
        public static partial CStringList CSLAddStringMayFail(CStringList? strList, string newString);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int CSLCount(CStringList.MarshalHandle strList);
        [GdalWrapperMethod] 
        private static partial int CSLCount(CStringList? strList);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string CSLGetField(CStringList.MarshalHandle strList, int index);
        [GdalWrapperMethod] 
        private static partial string CSLGetField(CStringList strList, int index);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial void CSLDestroy(nint unmanaged);
    }
}