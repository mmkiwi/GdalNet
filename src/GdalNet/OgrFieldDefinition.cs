// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public partial class OgrFieldDefinition : IConstructableWrapper<OgrFieldDefinition, OgrFieldDefinitionHandle>, IHasHandle<OgrFieldDefinitionHandle>
{
    public virtual OgrFieldType FieldType
    {
        get => Interop.OGR_Fld_GetType(Handle);
        set => Interop.OGR_Fld_SetType(Handle, value);
    }

    public virtual OgrFieldSubType FieldSubType
    {
        get => Interop.OGR_Fld_GetSubType(Handle);
        set => Interop.OGR_Fld_SetSubType(Handle, value);
    }

    public string Name
    {
        get => Interop.OGR_Fld_GetNameRef(Handle);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => Interop.OGR_Fld_SetName(Handle, value);
    }

    public string AlternativeName
    {
        get => Interop.OGR_Fld_GetAlternativeNameRef(Handle);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => Interop.OGR_Fld_SetAlternativeName(Handle, value);
    }

    public OgrJustification Justification
    {
        get => Interop.OGR_Fld_GetJustify(Handle);
        set => Interop.OGR_Fld_SetJustify(Handle, value);
    }

    public int Width
    {
        get => Interop.OGR_Fld_GetWidth(Handle);
        set => Interop.OGR_Fld_SetWidth(Handle, value);
    }

    public int Precision
    {
        get => Interop.OGR_Fld_GetPrecision(Handle);
        set => Interop.OGR_Fld_SetPrecision(Handle, value);
    }

    public OgrTimeZoneFlag TimeZoneFlag
    {
        get => Interop.OGR_Fld_GetTZFlag(Handle);
        set => Interop.OGR_Fld_SetTZFlag(Handle, value);
    }

    public string? DefaultValue
    {
        get => Interop.OGR_Fld_GetDefault(Handle);
        set => Interop.OGR_Fld_SetDefault(Handle, value);
    }

    public bool IsDefaultDriverSpecific => Interop.OGR_Fld_IsDefaultDriverSpecific(Handle);

    public bool IsIgnored
    {
        get => Interop.OGR_Fld_IsIgnored(Handle);
        set => Interop.OGR_Fld_SetIgnored(Handle, value);
    }

    public bool IsNullable
    {
        get => Interop.OGR_Fld_IsNullable(Handle);
        set => Interop.OGR_Fld_SetNullable(Handle, value);
    }

    public bool IsUnique
    {
        get => Interop.OGR_Fld_IsUnique(Handle);
        set => Interop.OGR_Fld_SetUnique(Handle, value);
    }

    public string DomainName
    {
        get => Interop.OGR_Fld_GetDomainName(Handle);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => Interop.OGR_Fld_SetDomainName(Handle, value);
    }

    public string GetComment
    {
        get => Interop.OGR_Fld_GetComment(Handle);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => Interop.OGR_Fld_SetComment(Handle, value); 
    }
}
