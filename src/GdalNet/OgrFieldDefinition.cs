// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.VisualBasic.FileIO;

namespace MMKiwi.GdalNet;

public partial class OgrFieldDefinition : GdalHandle, IConstructibleHandle<OgrFieldDefinition>
{
    public virtual bool IsReadOnly { get; } = false;
    private OgrFieldDefinition(nint pointer) : base(pointer) { }
    public static OgrFieldDefinition Construct(nint pointer, bool ownsHandle)
    {
        ThrowIfOwnsHandle(ownsHandle, nameof(OgrFieldDefinition));
        return new(pointer);
    }

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

    internal OgrFieldDefinition AsReadOnly() => new ReadOnly(this);

    public class ReadOnly : OgrFieldDefinition, IConstructibleHandle<OgrFieldDefinition>
    {
        private ReadOnly(nint pointer) : base(pointer)
        {
            _fieldType = new Lazy<OgrFieldType>(() => Interop.OGR_Fld_GetType(this));
            _subType = new Lazy<OgrFieldSubType>(() => Interop.OGR_Fld_GetSubType(this));
        }
        internal ReadOnly(OgrFieldDefinition definition) : this(definition.Handle) { }

        public override bool IsReadOnly => true;

        public static new OgrFieldDefinition Construct(nint pointer, bool ownsHandle) => new ReadOnly(pointer);

        readonly Lazy<OgrFieldType> _fieldType;
        private readonly Lazy<OgrFieldSubType> _subType;

        public override OgrFieldType FieldType
        {
            get => _fieldType.Value;
            set => throw new NotSupportedException("Cannot edit a field definition that is attached to a feature");
        }

        public override OgrFieldSubType FieldSubType
        {
            get => _subType.Value;
            set => throw new NotSupportedException("Cannot edit a field definition that is attached to a feature");
        }
    }

    internal static partial class Interop
    {
        [LibraryImport("gdal")]
        public static partial OgrFieldType OGR_Fld_GetType([MarshalUsing(typeof(MarshalIn<OgrFieldDefinition>))] OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetType([MarshalUsing(typeof(MarshalIn<OgrFieldDefinition>))] OgrFieldDefinition fieldDefinition, OgrFieldType subType);

        [LibraryImport("gdal")]
        public static partial OgrFieldSubType OGR_Fld_GetSubType([MarshalUsing(typeof(MarshalIn<OgrFieldDefinition>))] OgrFieldDefinition fieldDefinition);

        [LibraryImport("gdal")]
        public static partial void OGR_Fld_SetSubType([MarshalUsing(typeof(MarshalIn<OgrFieldDefinition>))] OgrFieldDefinition fieldDefinition, OgrFieldSubType subType);
    }
}