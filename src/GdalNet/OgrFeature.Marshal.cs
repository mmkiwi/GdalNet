// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public sealed partial class OgrFeature : IConstructableWrapper<OgrFeature, OgrFeature.MarshalHandle>, IHasHandle<OgrFeature.MarshalHandle>
{
    [GdalGenerateHandle]
    internal abstract partial class MarshalHandle : GdalInternalHandle, IConstructableHandle<MarshalHandle>
    {
        protected override GdalCplErr? ReleaseHandleCore()
        {
            Interop.OGR_F_Destroy(handle);
            return null;
        }

        internal class Owns : MarshalHandle { public Owns() : base(true) { } }
        internal class DoesntOwn : MarshalHandle { public DoesntOwn() : base(true) { } }
    }
}