﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.CBindingSG;
using MMKiwi.GdalNet.Handles;


namespace MMKiwi.GdalNet;

[CbsgGenerateWrapper]
public partial class OgrFieldDefinition : IConstructableWrapper<OgrFieldDefinition, OgrFieldDefinitionHandle>, IHasHandle<OgrFieldDefinitionHandle>
{
    public virtual OgrFieldType FieldType
    {
        get => OgrApiH.OGR_Fld_GetType(Handle);
        set => OgrApiH.OGR_Fld_SetType(Handle, value);
    }

    public virtual OgrFieldSubType FieldSubType
    {
        get => OgrApiH.OGR_Fld_GetSubType(Handle);
        set => OgrApiH.OGR_Fld_SetSubType(Handle, value);
    }

    public string Name
    {
        get => OgrApiH.OGR_Fld_GetNameRef(Handle);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => OgrApiH.OGR_Fld_SetName(Handle, value);
    }

    public string AlternativeName
    {
        get => OgrApiH.OGR_Fld_GetAlternativeNameRef(Handle);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => OgrApiH.OGR_Fld_SetAlternativeName(Handle, value);
    }

    public OgrJustification Justification
    {
        get => OgrApiH.OGR_Fld_GetJustify(Handle);
        set => OgrApiH.OGR_Fld_SetJustify(Handle, value);
    }

    public int Width
    {
        get => OgrApiH.OGR_Fld_GetWidth(Handle);
        set => OgrApiH.OGR_Fld_SetWidth(Handle, value);
    }

    public int Precision
    {
        get => OgrApiH.OGR_Fld_GetPrecision(Handle);
        set => OgrApiH.OGR_Fld_SetPrecision(Handle, value);
    }

    public OgrTimeZoneFlag TimeZoneFlag
    {
        get => OgrApiH.OGR_Fld_GetTZFlag(Handle);
        set => OgrApiH.OGR_Fld_SetTZFlag(Handle, value);
    }

    public string? DefaultValue
    {
        get => OgrApiH.OGR_Fld_GetDefault(Handle);
        set => OgrApiH.OGR_Fld_SetDefault(Handle, value);
    }

    public bool IsDefaultDriverSpecific => OgrApiH.OGR_Fld_IsDefaultDriverSpecific(Handle);

    public bool IsIgnored
    {
        get => OgrApiH.OGR_Fld_IsIgnored(Handle);
        set => OgrApiH.OGR_Fld_SetIgnored(Handle, value);
    }

    public bool IsNullable
    {
        get => OgrApiH.OGR_Fld_IsNullable(Handle);
        set => OgrApiH.OGR_Fld_SetNullable(Handle, value);
    }

    public bool IsUnique
    {
        get => OgrApiH.OGR_Fld_IsUnique(Handle);
        set => OgrApiH.OGR_Fld_SetUnique(Handle, value);
    }

    public string DomainName
    {
        get => OgrApiH.OGR_Fld_GetDomainName(Handle);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => OgrApiH.OGR_Fld_SetDomainName(Handle, value);
    }

    public string GetComment
    {
        get => OgrApiH.OGR_Fld_GetComment(Handle);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => OgrApiH.OGR_Fld_SetComment(Handle, value); 
    }
}
