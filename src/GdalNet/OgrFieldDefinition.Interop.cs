// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public partial class OgrFieldDefinition
{
    internal static partial class Interop
    {
        [LibraryImport("gdal")]
        public static partial OgrFieldType OGR_Fld_GetType(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetType(MarshalHandle fieldDefinition, OgrFieldType subType);

        [LibraryImport("gdal")]
        public static partial OgrFieldSubType OGR_Fld_GetSubType(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetSubType(MarshalHandle fieldDefinition, OgrFieldSubType subType);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetName(MarshalHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetNameRef(MarshalHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetAlternativeName(MarshalHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetAlternativeNameRef(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial int OGR_Fld_GetWidth(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetWidth(MarshalHandle fieldDefinition, int width);

        [LibraryImport("gdal")]
        public static partial int OGR_Fld_GetPrecision(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetPrecision(MarshalHandle fieldDefinition, int width);

        [LibraryImport("gdal")]
        public static partial OgrJustification OGR_Fld_GetJustify(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetJustify(MarshalHandle fieldDefinition, OgrJustification justification);

        [LibraryImport("gdal")]
        public static partial OgrTimeZoneFlag OGR_Fld_GetTZFlag(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetTZFlag(MarshalHandle fieldDefinition, OgrTimeZoneFlag justification);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsIgnored(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetIgnored(MarshalHandle fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsNullable(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetNullable(MarshalHandle fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsUnique(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetUnique(MarshalHandle fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string? OGR_Fld_GetDefault(MarshalHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetDefault(MarshalHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsDefaultDriverSpecific(MarshalHandle fieldDefinition);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetDomainName(MarshalHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetDomainName(MarshalHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetComment(MarshalHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetComment(MarshalHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_AreTypeSubTypeCompatible(MarshalHandle fieldDefinition);

    }
}
