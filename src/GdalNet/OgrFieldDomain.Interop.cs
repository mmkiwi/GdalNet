// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.VisualBasic.FileIO;

using MMKiwi.GdalNet.InteropAttributes;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

public partial class OgrFieldDomain
{
    internal partial class Interop
    {
        [LibraryImport("gdal")]
        public static partial void OGR_FldDomain_Destroy(nint domain);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string OGR_FldDomain_GetName(OgrFieldDomain.MarshalHandle domain);
        [GdalWrapperMethod]
        public static partial string OGR_FldDomain_GetName(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        [return: MarshalUsing(typeof(Utf8StringNoFree))]
        private static partial string OGR_FldDomain_GetDescription(OgrFieldDomain.MarshalHandle domain);
        [GdalWrapperMethod]
        public static partial string OGR_FldDomain_GetDescription(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial OgrFieldDomainType OGR_FldDomain_GetDomainType(OgrFieldDomain.MarshalHandle domain);
        [GdalWrapperMethod]
        public static partial OgrFieldDomainType OGR_FldDomain_GetDomainType(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial OgrFieldType OGR_FldDomain_GetFieldType(OgrFieldDomain.MarshalHandle domain);
        [GdalWrapperMethod]
        public static partial OgrFieldType OGR_FldDomain_GetFieldType(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial OgrFieldSubType OGR_FldDomain_GetFieldSubType(OgrFieldDomain.MarshalHandle domain);
        [GdalWrapperMethod]
        public static partial OgrFieldSubType OGR_FldDomain_GetFieldSubType(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial OgrFieldDomainSplitPolicy OGR_FldDomain_GetSplitPolicy(OgrFieldDomain.MarshalHandle domain);
        [GdalWrapperMethod]
        public static partial OgrFieldDomainSplitPolicy OGR_FldDomain_GetSplitPolicy(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial void OGR_FldDomain_SetSplitPolicy(OgrFieldDomain.MarshalHandle domain, OgrFieldDomainSplitPolicy value);
        [GdalWrapperMethod]
        public static partial void OGR_FldDomain_SetSplitPolicy(OgrFieldDomain domain, OgrFieldDomainSplitPolicy value);

        [LibraryImport("gdal")]
        private static partial OgrFieldDomainMergePolicy OGR_FldDomain_GetMergePolicy(OgrFieldDomain.MarshalHandle domain);
        [GdalWrapperMethod]
        public static partial OgrFieldDomainMergePolicy OGR_FldDomain_GetMergePolicy(OgrFieldDomain domain);

        [LibraryImport("gdal")]
        private static partial void OGR_FldDomain_SetMergePolicy(OgrFieldDomain.MarshalHandle domain, OgrFieldDomainMergePolicy value);
        [GdalWrapperMethod]
        public static partial void OGR_FldDomain_SetMergePolicy(OgrFieldDomain domain, OgrFieldDomainMergePolicy value);

        [LibraryImport("gdal", StringMarshalling = StringMarshalling.Utf8,EntryPoint = nameof(OGR_CodedFldDomain_Create))]
        private static partial OgrCodedFieldDomain.MarshalHandle.Owns _OGR_CodedFldDomain_Create(string name, string? description, OgrFieldType fieldType, OgrFieldSubType fieldSubType, nint enumeration);
        [GdalWrapperMethod(MethodName = nameof(_OGR_CodedFldDomain_Create))]
        public static partial OgrCodedFieldDomain.MarshalHandle.Owns OGR_CodedFldDomain_Create(string name, string? description, OgrFieldType fieldType, OgrFieldSubType fieldSubType, nint enumeration);
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