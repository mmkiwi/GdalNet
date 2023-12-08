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

    protected override bool ReleaseHandle()
    {
        lock (ReentrantLock)
        {
            if (IsInvalid)
                return false;

            GdalError.ResetErrors();
            var err = ReleaseHandleCore();
            bool errIsFatal = err is not null or GdalCplErr.Failure or GdalCplErr.Fatal;
            bool lastIsFatal = GdalError.LastError is not null && GdalError.LastError.Severity is not GdalCplErr.Failure or GdalCplErr.Fatal;
            return !errIsFatal && !lastIsFatal;
        }
    }

    protected abstract GdalCplErr? ReleaseHandleCore();
}

internal abstract class GdalInternalHandleNeverOwns : GdalInternalHandle
{
    protected GdalInternalHandleNeverOwns() : base(false) { }

    protected override GdalCplErr? ReleaseHandleCore() => default;
    protected override bool ReleaseHandle() => true;

}