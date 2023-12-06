// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet.CHelpers;

[GdalGenerateWrapper(HandleSetVisibility = MemberVisibility.Private)]
internal unsafe sealed partial class CStringList : IConstructableWrapper<CStringList, CStringList.MarshalHandle>, IHasHandle<CStringList.MarshalHandle>
{
    [GdalGenerateHandle]
    internal abstract partial class MarshalHandle : GdalInternalHandle, IConstructableHandle<MarshalHandle>
    {
        protected override GdalCplErr? ReleaseHandleCore()
        {
            Interop.CSLDestroy(handle);
            return null;
        }

    }
}
