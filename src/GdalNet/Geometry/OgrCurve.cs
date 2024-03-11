// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet.Geometry;

[NativeMarshalling(typeof(GdalMarshaller<OgrCurve, OgrGeometryHandle>))]
public class OgrCurve : OgrGeometry
{
    internal OgrCurve(OgrGeometryHandle handle) : base(handle)
    {
        
    }

    public OgrPoint GetValue(double distance)
    {
        var result = OgrApiH.OGR_G_Value(this, distance);
        GdalError.ThrowIfError();
        return result;
    }
}