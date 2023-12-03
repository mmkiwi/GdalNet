// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public partial class OgrFeature
{
    internal unsafe static partial class Interop
    {
        [LibraryImport("gdal")]
        public static partial void OGR_F_Destroy(nint feature);

        [LibraryImport("gdal")]
        private static partial int OGR_F_GetFieldCount(OgrFeature.MarshalHandle feature);
        [GdalWrapperMethod]
        public static partial int OGR_F_GetFieldCount(OgrFeature feature);

        [LibraryImport("gdal")]
        private static partial int OGR_F_GetFieldAsInteger(OgrFeature.MarshalHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial int OGR_F_GetFieldAsInteger(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        private static partial long OGR_F_GetFieldAsInteger64(OgrFeature.MarshalHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial long OGR_F_GetFieldAsInteger64(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        private static partial double OGR_F_GetFieldAsDouble(OgrFeature.MarshalHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial double OGR_F_GetFieldAsDouble(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string OGR_F_GetFieldAsString(OgrFeature.MarshalHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial string OGR_F_GetFieldAsString(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string OGR_F_GetFieldAsISO8601DateTime(OgrFeature.MarshalHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial string OGR_F_GetFieldAsISO8601DateTime(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        private static partial int[] OGR_F_GetFieldAsIntegerList(OgrFeature.MarshalHandle fieldDefinition, int index, out int count);
        [GdalWrapperMethod]
        public static partial int[] OGR_F_GetFieldAsIntegerList(OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        private static partial long[] OGR_F_GetFieldAsInteger64List(OgrFeature.MarshalHandle fieldDefinition, int index, out int count);
        [GdalWrapperMethod]
        public static partial long[] OGR_F_GetFieldAsInteger64List(OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        private static partial double[] OGR_F_GetFieldAsDoubleList(OgrFeature.MarshalHandle fieldDefinition, int index, out int count);
        [GdalWrapperMethod]
        public static partial double[] OGR_F_GetFieldAsDoubleList(OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(CStringArrayMarshal))]
        private static partial string[] OGR_F_GetFieldAsStringList(OgrFeature.MarshalHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial string[] OGR_F_GetFieldAsStringList(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        private static partial byte[] OGR_F_GetFieldAsBinary(OgrFeature.MarshalHandle fieldDefinition, int index, out int count);
        [GdalWrapperMethod]
        public static partial byte[] OGR_F_GetFieldAsBinary(OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        private static partial OgrFieldDefinition.MarshalHandle OGR_F_GetFieldDefnRef(OgrFeature.MarshalHandle feature, int index);
        [GdalWrapperMethod]
        public static partial OgrFieldDefinition OGR_F_GetFieldDefnRef(OgrFeature feature, int index);
    }
}