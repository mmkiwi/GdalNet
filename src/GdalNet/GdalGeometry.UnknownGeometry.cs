// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.CBindingSG;
using MMKiwi.GdalNet.Handles;


namespace MMKiwi.GdalNet;

public abstract partial class OgrGeometry
{
    [CbsgGenerateWrapper]
    private partial class UnknownGeometry(OgrGeometryHandle handle) : OgrGeometry(handle) , IConstructableWrapper<UnknownGeometry, OgrGeometryHandle>;

}