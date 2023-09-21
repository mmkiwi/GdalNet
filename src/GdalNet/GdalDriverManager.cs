// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.CHelpers;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(Marshal<GdalDriverManager>))]
public partial class GdalDriverManager : GdalHandle, IConstructibleHandle<GdalDriverManager>
{
    private GdalDriverManager(nint pointer) => SetHandle(pointer);
    public static GdalDriverManager Construct(nint pointer) => new(pointer);
}