// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet.Geometry;

[NativeMarshalling(typeof(GdalMarshaller<OgrPoint, OgrGeometryHandle>))]
public class OgrPoint : OgrGeometry
{
    internal OgrPoint(OgrGeometryHandle handle) : base(handle)
    {
    }

    public unsafe static OgrPoint Create(double x, double y)
    {
        OgrGeometry geometry = OgrApiH.OGR_G_CreateGeometry(OgrWkbGeometryType.Point);
        GdalError.ThrowIfError();
        if (geometry is not OgrPoint point || point.Handle.IsInvalid)
            throw new InvalidOperationException("Unable to create point geometry, invalid type");

        OgrPointRaw* raw = (OgrPointRaw*)point.Handle.DangerousGetHandle();
        
        raw->X = x;
        raw->Y = y;
        
        return point;
    }

    public unsafe double X
    {
        get => RawPointer->X;
        set => RawPointer->X = value;
    }
    
    public unsafe double Y
    {
        get => RawPointer->Y;
        set => RawPointer->Y = value;
    }
    
    public unsafe double Z
    {
        get => RawPointer->Z;
        set => RawPointer->Z = value;
    }
    
    public unsafe double M
    {
        get => RawPointer->M;
        set => RawPointer->M = value;
    }

    private unsafe protected OgrPointRaw* RawPointer
    {
        get
        {
            if (Handle.IsInvalid)
                throw new InvalidOperationException("Cannot access null OgrPoint");
            return (OgrPointRaw*)Handle.DangerousGetHandle();
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    private protected struct OgrPointRaw
    {
        const uint OGR_G_NOT_EMPTY_POINT = 0x1;
        const uint OGR_G_3D = 0x2;
        const uint OGR_G_MEASURED = 0x4;

        private OgrGeometryRaw _ogrGeometry;
        private double _x;
        private double _y;
        private double _z;
        private double _m;

        public double X
        {
            get => _x;
            set
            {
                _x = value;
                if (double.IsNaN(_x) || double.IsNaN(_y))
                    _ogrGeometry.Flags &= ~OGR_G_NOT_EMPTY_POINT;
                else
                    _ogrGeometry.Flags |= OGR_G_NOT_EMPTY_POINT;
            }
        }
        
        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                if (double.IsNaN(_x) || double.IsNaN(_y))
                    _ogrGeometry.Flags &= ~OGR_G_NOT_EMPTY_POINT;
                else
                    _ogrGeometry.Flags |= OGR_G_NOT_EMPTY_POINT;
            }
        }
        
        public double Z
        {
            get => _z;
            set
            {
                _z = value;
                _ogrGeometry.Flags |= OGR_G_3D;
            }
        }

        public double M
        {
            get => _m;
            set
            {
                _m = value;
                _ogrGeometry.Flags |= OGR_G_MEASURED;
            }
        }
    }
}