// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.Win32.SafeHandles;

namespace MMKiwi.GdalNet.Handles;

internal abstract class GdalInternalHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private protected GdalInternalHandle(bool ownsHandle) : base(ownsHandle)
    {
    }

    //private static readonly object s_reentrantLock = new();

    protected sealed override bool ReleaseHandle()
    {
        //lock (s_reentrantLock)
        {
            return ReleaseHandleCore();
        }
    }

    private protected abstract bool ReleaseHandleCore();

    internal abstract class NeverOwns : GdalInternalHandle
    {
        private protected NeverOwns() : base(false)
        {
        }

        private protected override bool ReleaseHandleCore() => true;
    }
}