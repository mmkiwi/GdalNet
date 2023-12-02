// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;

namespace MMKiwi.GdalNet;

internal abstract class GdalInternalHandle : SafeHandle
{
    protected GdalInternalHandle(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
    {
        OwnsHandle = ownsHandle;
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    internal bool OwnsHandle { get; }

    public static readonly object ReentrantLock = new();
}

internal abstract class GdalInternalHandleNeverOwns : GdalInternalHandle
{
    protected GdalInternalHandleNeverOwns() : base(false) { }

    protected override bool ReleaseHandle() => true;

}