// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshallerNeverOwns<OgrFieldDefinition, OgrFieldDefinitionHandle>))]
public class OgrFieldDefinition : IConstructableWrapper<OgrFieldDefinition, OgrFieldDefinitionHandle>, IHasHandle<OgrFieldDefinitionHandle>
{
    public virtual OgrFieldType FieldType
    {
        get => OgrApiH.OGR_Fld_GetType(this);
        set => OgrApiH.OGR_Fld_SetType(this, value);
    }

    public virtual OgrFieldSubType FieldSubType
    {
        get => OgrApiH.OGR_Fld_GetSubType(this);
        set => OgrApiH.OGR_Fld_SetSubType(this, value);
    }

    public string Name
    {
        get => OgrApiH.OGR_Fld_GetNameRef(this);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => OgrApiH.OGR_Fld_SetName(this, value);
    }

    public string AlternativeName
    {
        get => OgrApiH.OGR_Fld_GetAlternativeNameRef(this);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => OgrApiH.OGR_Fld_SetAlternativeName(this, value);
    }

    public OgrJustification Justification
    {
        get => OgrApiH.OGR_Fld_GetJustify(this);
        set => OgrApiH.OGR_Fld_SetJustify(this, value);
    }

    public int Width
    {
        get => OgrApiH.OGR_Fld_GetWidth(this);
        set => OgrApiH.OGR_Fld_SetWidth(this, value);
    }

    public int Precision
    {
        get => OgrApiH.OGR_Fld_GetPrecision(this);
        set => OgrApiH.OGR_Fld_SetPrecision(this, value);
    }

    public OgrTimeZoneFlag TimeZoneFlag
    {
        get => OgrApiH.OGR_Fld_GetTZFlag(this);
        set => OgrApiH.OGR_Fld_SetTZFlag(this, value);
    }

    public string? DefaultValue
    {
        get => OgrApiH.OGR_Fld_GetDefault(this);
        set => OgrApiH.OGR_Fld_SetDefault(this, value);
    }

    public bool IsDefaultDriverSpecific => OgrApiH.OGR_Fld_IsDefaultDriverSpecific(this);

    public bool IsIgnored
    {
        get => OgrApiH.OGR_Fld_IsIgnored(this);
        set => OgrApiH.OGR_Fld_SetIgnored(this, value);
    }

    public bool IsNullable
    {
        get => OgrApiH.OGR_Fld_IsNullable(this);
        set => OgrApiH.OGR_Fld_SetNullable(this, value);
    }

    public bool IsUnique
    {
        get => OgrApiH.OGR_Fld_IsUnique(this);
        set => OgrApiH.OGR_Fld_SetUnique(this, value);
    }

    public string DomainName
    {
        get => OgrApiH.OGR_Fld_GetDomainName(this);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => OgrApiH.OGR_Fld_SetDomainName(this, value);
    }

    public string GetComment
    {
        get => OgrApiH.OGR_Fld_GetComment(this);
        // No need to check for null; C function will use empty string if null pointer is passed
        set => OgrApiH.OGR_Fld_SetComment(this, value); 
    }

    private protected OgrFieldDefinition(OgrFieldDefinitionHandle handle)
    {
        Handle = handle;
    }
    
    internal OgrFieldDefinitionHandle Handle { get; }
    
    static OgrFieldDefinition IConstructableWrapper<OgrFieldDefinition, OgrFieldDefinitionHandle>.Construct(OgrFieldDefinitionHandle handle) => new(handle);
    OgrFieldDefinitionHandle IHasHandle<OgrFieldDefinitionHandle>.Handle => Handle;
}
