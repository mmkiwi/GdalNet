// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Geometry;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalMarshaller<OgrFeature, OgrFeatureHandle>))]
public sealed class OgrFeature : IDisposable, IConstructableWrapper<OgrFeature, OgrFeatureHandle>,
    IHasHandle<OgrFeatureHandle>
{
    private bool _disposedValue;

    public static OgrFeature Create(OgrFeatureDefinition featureDefinition)
    {
        var result = OgrApiH.OGR_F_Create(featureDefinition);
        GdalError.ThrowIfError();
        return result ?? throw new OutOfMemoryException();
    }
    
    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                this.Handle.Dispose();
            }

            _disposedValue = true;
        }
    }

    public long Fid
    {
        get
        {
            var result = OgrApiH.OGR_F_GetFID(this);
            GdalError.ThrowIfError();
            return result;
        }
        set => OgrApiH.OGR_F_SetFID(this, value).ThrowIfError();
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
    }

    static OgrFeature IConstructableWrapper<OgrFeature, OgrFeatureHandle>.Construct(OgrFeatureHandle handle)
        => new(handle);

    OgrFeatureHandle IHasHandle<OgrFeatureHandle>.Handle => Handle;

    private OgrFeature(OgrFeatureHandle handle) => Handle = handle;

    internal OgrFeatureHandle Handle { get; }

    public bool IsNull(int index)
    {
        bool isNull = OgrApiH.OGR_F_IsFieldNull(this, index);
        GdalError.ThrowIfError();
        return isNull;
    }

    public void SetNull(int index)
    {
        OgrApiH.OGR_F_SetFieldNull(this, index);
        GdalError.ThrowIfError();
    }

    public OgrGeometry Geometry
    {
        get
        {
            var result = OgrApiH.OGR_F_GetGeometryRef(this);
            GdalError.ThrowIfError();
            return result;
        }
        set
        {
            OgrApiH.OGR_F_SetGeometry(this, value).ThrowIfError();
        }
    }
    
    public bool IsSetAndNotNull(int index)
    {
        bool isNull = OgrApiH.OGR_F_IsFieldSetAndNotNull(this, index);
        GdalError.ThrowIfError();
        return isNull;
    }

    public bool IsSet(int index)
    {
        bool isNull = OgrApiH.OGR_F_IsFieldSet(this, index);
        GdalError.ThrowIfError();
        return isNull;
    }

    public int GetInt32(int index)
    {
        int result = OgrApiH.OGR_F_GetFieldAsInteger(this, index);
        GdalError.ThrowIfError();
        return result;
    }

    public int? GetNullableInt32(int index)
    {
        bool isNull = OgrApiH.OGR_F_IsFieldSetAndNotNull(this, index);
        GdalError.ThrowIfError();
        if (!isNull)
            return null;

        int result = OgrApiH.OGR_F_GetFieldAsInteger(this, index);
        GdalError.ThrowIfError();
        return result;
    }

    public long GetInt64(int index)
    {
        long result = OgrApiH.OGR_F_GetFieldAsInteger64(this, index);
        GdalError.ThrowIfError();
        return result;
    }

    public long? GetNullableInt64(int index)
    {
        bool isNull = OgrApiH.OGR_F_IsFieldSetAndNotNull(this, index);
        GdalError.ThrowIfError();
        if (!isNull)
            return null;

        long result = OgrApiH.OGR_F_GetFieldAsInteger64(this, index);
        GdalError.ThrowIfError();
        return result;
    }

    public double GetDouble(int index)
    {
        double result = OgrApiH.OGR_F_GetFieldAsDouble(this, index);
        GdalError.ThrowIfError();
        return result;
    }

    public double? GetNullableDouble(int index)
    {
        bool isNull = OgrApiH.OGR_F_IsFieldSetAndNotNull(this, index);
        GdalError.ThrowIfError();
        if (!isNull)
            return null;

        double result = OgrApiH.OGR_F_GetFieldAsDouble(this, index);
        GdalError.ThrowIfError();
        return result;
    }

    public string? GetString(int index)
    {
        string result = OgrApiH.OGR_F_GetFieldAsString(this, index);
        GdalError.ThrowIfError();
        return result;
    }

    public int[]? GetInt32Array(int index)
    {
        int[]? result = OgrApiH.OGR_F_GetFieldAsIntegerList(this, index, out int _);
        GdalError.ThrowIfError();
        return result;
    }

    public long[]? GetInt64Array(int index)
    {
        long[]? result = OgrApiH.OGR_F_GetFieldAsInteger64List(this, index, out int _);
        GdalError.ThrowIfError();
        return result;
    }

    public double[]? GetDoubleArray(int index)
    {
        double[]? result = OgrApiH.OGR_F_GetFieldAsDoubleList(this, index, out int _);
        GdalError.ThrowIfError();
        return result;
    }

    public string[]? GetStringArray(int index)
    {
        string[]? result = OgrApiH.OGR_F_GetFieldAsStringList(this, index);
        GdalError.ThrowIfError();
        return result;
    }

    public byte[]? GetByteArray(int index)
    {
        byte[]? result = OgrApiH.OGR_F_GetFieldAsBinary(this, index, out int _);
        GdalError.ThrowIfError();
        return result;
    }

    public DateTime GetDateTime(int index)
    {
        bool res = OgrApiH.OGR_F_GetFieldAsDateTimeEx(this, index, out int year, out int month, out int day, out int hour,
            out int minute, out float second, out int tzFlag);
        GdalError.ThrowIfError();
        if (!res)
            throw new GdalException($"Unknown exception parsing datetime for field {index}");

        var wholeSec = (int)second;
        var milliseconds = (int)((wholeSec - second) * 1000);
        
        return new DateTime(year, month, day, hour, minute, wholeSec, milliseconds, tzFlag switch
            {
                1 => DateTimeKind.Local,
                100 => DateTimeKind.Utc,
                _ => DateTimeKind.Unspecified
            }
        );
    }

    public void SetField(int index, int value)
    {
        OgrApiH.OGR_F_SetFieldInteger(this, index, value);
        GdalError.ThrowIfError();
    }
    
    public void SetField(int index, long value)
    {
        OgrApiH.OGR_F_SetFieldInteger64(this, index, value);
        GdalError.ThrowIfError();
    }
    
    public void SetField(int index, double value)
    {
        OgrApiH.OGR_F_SetFieldDouble(this, index, value);
        GdalError.ThrowIfError();
    }
    
    public void SetField(int index, string? value)
    {
        OgrApiH.OGR_F_SetFieldString(this, index, value);
        GdalError.ThrowIfError();
    }
    
    public void SetField(int index, int[]? value)
    {
        OgrApiH.OGR_F_SetFieldIntegerList(this, index, value?.Length ?? 0, value);
        GdalError.ThrowIfError();
    }
    
    public void SetField(int index, long[]? value)
    {
        OgrApiH.OGR_F_SetFieldInteger64List(this, index, value?.Length ?? 0, value);
        GdalError.ThrowIfError();
    }
    
    public void SetField(int index, double[]? value)
    {
        OgrApiH.OGR_F_SetFieldDoubleList(this, index, value?.Length ?? 0, value);
        GdalError.ThrowIfError();
    }
    
    public void SetField(int index, string[]? value)
    {
        OgrApiH.OGR_F_SetFieldStringList(this, index, value);
        GdalError.ThrowIfError();
    }
    
    public void SetField(int index, byte[]? value)
    {
        OgrApiH.OGR_F_SetFieldBinary(this, index, value?.Length ?? 0, value);
        GdalError.ThrowIfError();
    }
    
    public void SetField(int index, DateTime value)
    {
        float secAndMs = value.Second + value.Millisecond/1000f;
        OgrApiH.OGR_F_SetFieldDateTimeEx(this, index, value.Year, value.Month, value.Day, value.Hour, value.Minute, secAndMs, value.Kind switch
        {
            DateTimeKind.Local => 1,
            DateTimeKind.Utc => 100,
            _ => 0
        });
        
        GdalError.ThrowIfError();
    }

    public int FieldCount
    {
        get
        {
            var result = OgrApiH.OGR_F_GetFieldCount(this);
            GdalError.ThrowIfError();
            return result;
        }
    }
    
    public int GeometryFieldCount
    {
        get
        {
            var result = OgrApiH.OGR_F_GetGeomFieldCount(this);
            GdalError.ThrowIfError();
            return result;
        }
    }
    
    public int GetGeometryFieldIndex(string name)
    {
        var result = OgrApiH.OGR_F_GetGeomFieldIndex(this, name);
        GdalError.ThrowIfError();
        return result;
    }
    
    public int GetFieldIndex(string name)
    {
        var result = OgrApiH.OGR_F_GetFieldIndex(this, name);
        GdalError.ThrowIfError();
        return result;
    }

    public void UnsetField(int index)
    {
        OgrApiH.OGR_F_UnsetField(this, index);
        GdalError.ThrowIfError();
    }
}