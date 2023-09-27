// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public partial class OgrFeature
{
    internal unsafe static partial class Interop
    {
        [LibraryImport("gdal")]
        public static partial void OGR_F_Destroy([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature feature);

        [LibraryImport("gdal")]
        public static partial int OGR_F_GetFieldCount([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature feature);

        [LibraryImport("gdal")]
        public static partial int OGR_F_GetFieldAsInteger([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        public static partial long OGR_F_GetFieldAsInteger64([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        public static partial double OGR_F_GetFieldAsDouble([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_F_GetFieldAsString([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_F_GetFieldAsISO8601DateTime([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        public static partial int[] OGR_F_GetFieldAsIntegerList([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        public static partial long[] OGR_F_GetFieldAsInteger64List([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        public static partial double[] OGR_F_GetFieldAsDoubleList([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(CStringArrayMarshal))]
        public static partial string[] OGR_F_GetFieldAsStringList([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature fieldDefinition, int index);

        [LibraryImport("gdal")]
        [return: MarshalUsing(CountElementName = nameof(count))]
        public static partial byte[] OGR_F_GetFieldAsBinary([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature fieldDefinition, int index, out int count);

        [LibraryImport("gdal")]
        [return:MarshalUsing(typeof(MarshalDoesNotOwnHandle<OgrFieldDefinition>))]
        public static partial OgrFieldDefinition OGR_F_GetFieldDefnRef([MarshalUsing(typeof(MarshalIn<OgrFeature>))] OgrFeature feature, int index);
    }
}