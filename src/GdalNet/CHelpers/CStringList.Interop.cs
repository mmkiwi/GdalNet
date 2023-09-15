// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet.CHelpers;

[NativeMarshalling(typeof(GdalHandle.Marshal<CStringList>))]
internal partial class CStringList
{
    public unsafe static partial class Interop
    {
        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial nint CSLAddString(CStringList? strList, string newString);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial nint CSLAddStringMayFail(CStringList? strList, string newString);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial int CSLCount(CStringList? strList);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [return:MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string CSLGetField(CStringList strList, int index);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        public static partial void CSLDestroy(CStringList unmanaged);
    }
}
