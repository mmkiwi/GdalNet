// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.VisualBasic.FileIO;

namespace MMKiwi.GdalNet;
[SourceGenerators.GenerateGdalMarshal]
public partial class OgrFieldDefinition : GdalHandle, IEquatable<OgrFieldDefinition>
{
    public virtual OgrFieldType FieldType
    {
        get => Interop.OGR_Fld_GetType(this);
        set => Interop.OGR_Fld_SetType(this, value);
    }

    public virtual OgrFieldSubType FieldSubType
    {
        get => Interop.OGR_Fld_GetSubType(this);
        set => Interop.OGR_Fld_SetSubType(this, value);
    }

    public string Name
    {
        get => Interop.OGR_Fld_GetNameRef(this);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => Interop.OGR_Fld_SetName(this, value);
    }

    public string AlternativeName
    {
        get => Interop.OGR_Fld_GetAlternativeNameRef(this);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => Interop.OGR_Fld_SetAlternativeName(this, value);
    }

    public OgrJustification Justification
    {
        get => Interop.OGR_Fld_GetJustify(this);
        set => Interop.OGR_Fld_SetJustify(this, value);
    }

    public int Width
    {
        get => Interop.OGR_Fld_GetWidth(this);
        set => Interop.OGR_Fld_SetWidth(this, value);
    }

    public int Precision
    {
        get => Interop.OGR_Fld_GetPrecision(this);
        set => Interop.OGR_Fld_SetPrecision(this, value);
    }

    public OgrTimeZoneFlag TimeZoneFlag
    {
        get => Interop.OGR_Fld_GetTZFlag(this);
        set => Interop.OGR_Fld_SetTZFlag(this, value);
    }

    public string? DefaultValue
    {
        get => Interop.OGR_Fld_GetDefault(this);
        set => Interop.OGR_Fld_SetDefault(this, value);
    }

    public bool IsDefaultDriverSpecific => Interop.OGR_Fld_IsDefaultDriverSpecific(this);

    public bool IsIgnored
    {
        get => Interop.OGR_Fld_IsIgnored(this);
        set => Interop.OGR_Fld_SetIgnored(this, value);
    }

    public bool IsNullable
    {
        get => Interop.OGR_Fld_IsNullable(this);
        set => Interop.OGR_Fld_SetNullable(this, value);
    }

    public bool IsUnique
    {
        get => Interop.OGR_Fld_IsUnique(this);
        set => Interop.OGR_Fld_SetUnique(this, value);
    }

    public string DomainName
    {
        get => Interop.OGR_Fld_GetDomainName(this);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => Interop.OGR_Fld_SetDomainName(this, value);
    }

    public string GetComment
    {
        get => Interop.OGR_Fld_GetComment(this);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => Interop.OGR_Fld_SetComment(this, value); 
    }
}
