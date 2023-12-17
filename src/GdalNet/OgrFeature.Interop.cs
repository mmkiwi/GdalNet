// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.InteropAttributes;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public partial class OgrFeature
{
    internal static partial class Interop
    {
        [LibraryImport("gdal")]
        private static partial int OGR_F_GetFieldCount(OgrFeatureHandle feature);
        [GdalWrapperMethod]
        public static partial int OGR_F_GetFieldCount(OgrFeature feature);

        [LibraryImport("gdal")]
        private static partial int OGR_F_GetFieldAsInteger(OgrFeatureHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial int OGR_F_GetFieldAsInteger(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        private static partial long OGR_F_GetFieldAsInteger64(OgrFeatureHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial long OGR_F_GetFieldAsInteger64(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        private static partial double OGR_F_GetFieldAsDouble(OgrFeatureHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial double OGR_F_GetFieldAsDouble(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string OGR_F_GetFieldAsString(OgrFeatureHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial string OGR_F_GetFieldAsString(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string OGR_F_GetFieldAsISO8601DateTime(OgrFeatureHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial string OGR_F_GetFieldAsISO8601DateTime(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        private static partial int[] OGR_F_GetFieldAsIntegerList(OgrFeatureHandle fieldDefinition, int index, out int count);
        [GdalWrapperMethod]
        public static partial int[] OGR_F_GetFieldAsIntegerList(OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        private static partial long[] OGR_F_GetFieldAsInteger64List(OgrFeatureHandle fieldDefinition, int index, out int count);
        [GdalWrapperMethod]
        public static partial long[] OGR_F_GetFieldAsInteger64List(OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        private static partial double[] OGR_F_GetFieldAsDoubleList(OgrFeatureHandle fieldDefinition, int index, out int count);
        [GdalWrapperMethod]
        public static partial double[] OGR_F_GetFieldAsDoubleList(OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(CStringArrayMarshal))]
        private static partial string[] OGR_F_GetFieldAsStringList(OgrFeatureHandle fieldDefinition, int index);
        [GdalWrapperMethod]
        public static partial string[] OGR_F_GetFieldAsStringList(OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        private static partial byte[] OGR_F_GetFieldAsBinary(OgrFeatureHandle fieldDefinition, int index, out int count);
        [GdalWrapperMethod]
        public static partial byte[] OGR_F_GetFieldAsBinary(OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        private static partial OgrFieldDefinitionHandle OGR_F_GetFieldDefnRef(OgrFeatureHandle feature, int index);
        [GdalWrapperMethod]
        public static partial OgrFieldDefinition OGR_F_GetFieldDefnRef(OgrFeature feature, int index);
    }
}