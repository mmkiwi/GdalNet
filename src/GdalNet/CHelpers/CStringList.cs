// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMKiwi.GdalNet.CHelpers;

/// <summary>
/// The C String List methods from GDAL. This manipulates and underlying
/// C array of strings (i.e. <c>char**</c>). These methods are pretty 
/// inefficient, so the ideal is to use C# methods such as <see cref="List{T}"/>
/// or <see cref="Array"/> of strings and then convert at the end.
/// </summary>
/// <remarks>
///  <see cref="Marshallers.CStringArrayMarshal"/> can be used to marshal
///  any IEnumerable 
/// </remarks>
internal unsafe sealed partial class CStringList : GdalSafeHandle
{
    public static CStringList Create(string firstString)
    {
        return new(Interop.CSLAddString(null, firstString), true);
    }

    protected override bool ReleaseHandle()
    {
        Interop.CSLDestroy(this);
        return true;
    }

    public byte** HandlePointer => (byte**)Handle;

    public void AddString(string value)
    {
        nint newHandle = Interop.CSLAddStringMayFail(this, value);
        if (newHandle != 0)
        {
            Handle = newHandle;
        }
    }
}
