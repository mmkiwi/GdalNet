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
        public static partial OgrFieldType OGR_Fld_GetType(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetType(OgrFieldDefinition fieldDefinition, OgrFieldType subType);

        [LibraryImport("gdal")]
        public static partial OgrFieldSubType OGR_Fld_GetSubType(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetSubType(OgrFieldDefinition fieldDefinition, OgrFieldSubType subType);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetName(OgrFieldDefinition fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetNameRef(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetAlternativeName(OgrFieldDefinition fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetAlternativeNameRef(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial int OGR_Fld_GetWidth(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetWidth(OgrFieldDefinition fieldDefinition, int width);

        [LibraryImport("gdal")]
        public static partial int OGR_Fld_GetPrecision(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetPrecision(OgrFieldDefinition fieldDefinition, int width);

        [LibraryImport("gdal")]
        public static partial OgrJustification OGR_Fld_GetJustify(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetJustify(OgrFieldDefinition fieldDefinition, OgrJustification justification);

        [LibraryImport("gdal")]
        public static partial OgrTimeZoneFlag OGR_Fld_GetTZFlag(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetTZFlag(OgrFieldDefinition fieldDefinition, OgrTimeZoneFlag justification);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsIgnored(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetIgnored(OgrFieldDefinition fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsNullable(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetNullable(OgrFieldDefinition fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsUnique(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetUnique(OgrFieldDefinition fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string? OGR_Fld_GetDefault(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetDefault(OgrFieldDefinition fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsDefaultDriverSpecific(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetDomainName(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetDomainName(OgrFieldDefinition fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetComment(OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetComment(OgrFieldDefinition fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_AreTypeSubTypeCompatible(OgrFieldDefinition fieldDefinition);

    }
}
