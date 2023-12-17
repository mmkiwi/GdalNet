// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public partial class OgrFieldDefinition
{
    internal static partial class Interop
    {
        [LibraryImport("gdal")]
        public static partial OgrFieldType OGR_Fld_GetType(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetType(OgrFieldDefinitionHandle fieldDefinition, OgrFieldType subType);

        [LibraryImport("gdal")]
        public static partial OgrFieldSubType OGR_Fld_GetSubType(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetSubType(OgrFieldDefinitionHandle fieldDefinition, OgrFieldSubType subType);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetName(OgrFieldDefinitionHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetNameRef(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetAlternativeName(OgrFieldDefinitionHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetAlternativeNameRef(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial int OGR_Fld_GetWidth(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetWidth(OgrFieldDefinitionHandle fieldDefinition, int width);

        [LibraryImport("gdal")]
        public static partial int OGR_Fld_GetPrecision(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetPrecision(OgrFieldDefinitionHandle fieldDefinition, int width);

        [LibraryImport("gdal")]
        public static partial OgrJustification OGR_Fld_GetJustify(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetJustify(OgrFieldDefinitionHandle fieldDefinition, OgrJustification justification);

        [LibraryImport("gdal")]
        public static partial OgrTimeZoneFlag OGR_Fld_GetTZFlag(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetTZFlag(OgrFieldDefinitionHandle fieldDefinition, OgrTimeZoneFlag justification);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsIgnored(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetIgnored(OgrFieldDefinitionHandle fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsNullable(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetNullable(OgrFieldDefinitionHandle fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsUnique(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetUnique(OgrFieldDefinitionHandle fieldDefinition, [MarshalAs(UnmanagedType.Bool)] bool isIgnored);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string? OGR_Fld_GetDefault(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetDefault(OgrFieldDefinitionHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_Fld_IsDefaultDriverSpecific(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetDomainName(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetDomainName(OgrFieldDefinitionHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string OGR_Fld_GetComment(OgrFieldDefinitionHandle fieldDefinition);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void OGR_Fld_SetComment(OgrFieldDefinitionHandle fieldDefinition, string? name);

        [LibraryImport("gdal")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool OGR_AreTypeSubTypeCompatible(OgrFieldDefinitionHandle fieldDefinition);

    }
}
