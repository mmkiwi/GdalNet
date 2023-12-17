// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Handles;

namespace MMKiwi.GdalNet.Interop;

internal static class GdalConstructionHelper
{
    public static TRes Construct<TRes, THandle>(THandle handle)
        where TRes : class, IConstructableWrapper<TRes, THandle>
        where THandle : GdalInternalHandle
    {
        return handle.IsInvalid
            ? throw new InvalidOperationException("Cannot marshal null handle")
            : TRes.Construct(handle);
    }

    public static TRes? ConstructNullable<TRes, THandle>(THandle handle)
        where TRes : class, IConstructableWrapper<TRes, THandle>
        where THandle : GdalInternalHandle
    {
        return handle.IsInvalid ? null : TRes.Construct(handle);
    }

    public static THandle GetNullHandle<THandle>()
        where THandle : GdalInternalHandle, IConstructableHandle<THandle>
    {
        return THandle.Construct(false);
    }
}
