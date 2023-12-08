// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.VisualBasic.FileIO;

using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public partial class OgrFieldDomain
{
    internal partial class Interop
    {
        [LibraryImport("gdal")]
        public unsafe static partial void OGR_FldDomain_Destroy(nint domain);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public unsafe static partial string OGR_FldDomain_GetName(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        public unsafe static partial string OGR_FldDomain_GetDescription(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        public unsafe static partial OgrFieldDomainType OGR_FldDomain_GetDomainType(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        public unsafe static partial OgrFieldType OGR_FldDomain_GetFieldType(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        public unsafe static partial OgrFieldSubType OGR_FldDomain_GetFieldSubType(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        public unsafe static partial OgrFieldDomainSplitPolicy OGR_FldDomain_GetSplitPolicy(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        public unsafe static partial void OGR_FldDomain_SetSplitPolicy(OgrFieldDomain domain, OgrFieldDomainSplitPolicy value);

        [LibraryImport("gdal")]
        public unsafe static partial OgrFieldDomainMergePolicy OGR_FldDomain_GetMergePolicy(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        public unsafe static partial void OGR_FldDomain_SetMergePolicy(OgrFieldDomain domain, OgrFieldDomainMergePolicy value);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8)]
        [return:MarshalUsing(typeof(GdalHandleMarshallerOutOwns<OgrCodedFieldDomain, OgrCodedFieldDomain.MarshalHandle>))]
        public unsafe static partial OgrCodedFieldDomain? OGR_CodedFldDomain_Create(string name, string? description, OgrFieldType fieldType, OgrFieldSubType fieldSubType, nint enumeration);
    }
}

public enum OgrFieldDomainType
{
    Coded = 0,
    Range = 1,
    Glob = 2
}

public enum OgrFieldDomainSplitPolicy
{
    DefaultValue = 0,
    Duplicate = 1,
    GeometryRatio = 2
}

public enum OgrFieldDomainMergePolicy
{
    DefaultValue = 0,
    Sum = 1,
    GeometryWeighted = 2
}