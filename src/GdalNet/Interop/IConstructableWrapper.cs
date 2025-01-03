// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.Interop;

public interface IConstructableWrapper<out TRes, in THandle> where THandle : SafeHandle
{
    public static abstract TRes Construct(THandle handle);
}

