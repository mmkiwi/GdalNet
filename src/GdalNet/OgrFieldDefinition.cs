// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshallerNeverOwns<OgrFieldDefinition, OgrFieldDefinitionHandle>))]
public class OgrFieldDefinition : IConstructableWrapper<OgrFieldDefinition, OgrFieldDefinitionHandle>, IHasHandle<OgrFieldDefinitionHandle>
{
    public virtual OgrFieldType FieldType
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetType(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_Fld_SetType(this, value);
            GdalError.ThrowIfError();
        }
    }

    public virtual OgrFieldSubType FieldSubType
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetSubType(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_Fld_SetSubType(this, value);
            GdalError.ThrowIfError();
        }
    }

    public string Name
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetNameRef(this);
            GdalError.ThrowIfError();
            return result;
        }
        // No need to check for null; C function will use empty string if null pointer is passed
        set
        {
            OgrApiH.OGR_Fld_SetName(this, value);
            GdalError.ThrowIfError();
        }
    }

    public string AlternativeName
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetAlternativeNameRef(this);
            GdalError.ThrowIfError();
            return result;
        }
        // No need to check for null; C function will use empty string if null pointer is passed
        set
        {
            OgrApiH.OGR_Fld_SetAlternativeName(this, value);
            GdalError.ThrowIfError();
        }
    }

    public OgrJustification Justification
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetJustify(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_Fld_SetJustify(this, value);
            GdalError.ThrowIfError();
        }
    }

    public int Width
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetWidth(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_Fld_SetWidth(this, value);
            GdalError.ThrowIfError();
        }
    }

    public int Precision
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetPrecision(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_Fld_SetPrecision(this, value);
            GdalError.ThrowIfError();
        }
    }

    public OgrTimeZoneFlag TimeZoneFlag
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetTZFlag(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_Fld_SetTZFlag(this, value);
            GdalError.ThrowIfError();
        }
    }

    public string? DefaultValue
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetDefault(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_Fld_SetDefault(this, value);
            GdalError.ThrowIfError();
        }
    }

    public bool IsDefaultDriverSpecific
    {
        get
        {
            var result = OgrApiH.OGR_Fld_IsDefaultDriverSpecific(this);
            GdalError.ThrowIfError();
            return result;
        }
    }

    public bool IsIgnored
    {
        get
        {
            var result = OgrApiH.OGR_Fld_IsIgnored(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_Fld_SetIgnored(this, value);
            GdalError.ThrowIfError();
        }
    }

    public bool IsNullable
    {
        get
        {
            var result = OgrApiH.OGR_Fld_IsNullable(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_Fld_SetNullable(this, value);
            GdalError.ThrowIfError();
        }
    }

    public bool IsUnique
    {
        get
        {
            var result = OgrApiH.OGR_Fld_IsUnique(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_Fld_SetUnique(this, value);
            GdalError.ThrowIfError();
        }
    }

    public string DomainName
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetDomainName(this);
            GdalError.ThrowIfError();
            return result;
        }
        // No need to check for null; C function will use empty string if null pointer is passed
        set
        {
            OgrApiH.OGR_Fld_SetDomainName(this, value);
            GdalError.ThrowIfError();
        }
    }

    public string GetComment
    {
        get
        {
            var result = OgrApiH.OGR_Fld_GetComment(this);
            GdalError.ThrowIfError();
            return result;
        }
        // No need to check for null; C function will use empty string if null pointer is passed
        set
        {
            OgrApiH.OGR_Fld_SetComment(this, value);
            GdalError.ThrowIfError();
        }
    }

    private protected OgrFieldDefinition(OgrFieldDefinitionHandle handle)
    {
        Handle = handle;
    }

    internal OgrFieldDefinitionHandle Handle { get; }

    static OgrFieldDefinition IConstructableWrapper<OgrFieldDefinition, OgrFieldDefinitionHandle>.Construct(OgrFieldDefinitionHandle handle) => new(handle);
    OgrFieldDefinitionHandle IHasHandle<OgrFieldDefinitionHandle>.Handle => Handle;
}