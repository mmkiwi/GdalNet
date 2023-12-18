// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.Handles;

internal abstract partial class GdalInternalHandle : SafeHandle
{
    private protected GdalInternalHandle(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
    {
        OwnsHandle = ownsHandle;
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    private bool OwnsHandle { get; }

    private static readonly object s_reentrantLock = new();

    protected override bool ReleaseHandle()
    {
        if (OwnsHandle)
            return false;
        
        lock (s_reentrantLock)
        {
            if (IsInvalid)
                return false;

            GdalError.ResetErrors();
            var err = ReleaseHandleCore();
            bool errIsFatal = err is not GdalCplErr.Failure && err is not GdalCplErr.Fatal;
            bool lastIsFatal = GdalError.LastError is not null && 
                               GdalError.LastError.Severity is not GdalCplErr.Failure && 
                               GdalError.LastError.Severity is not GdalCplErr.Fatal;
            return !errIsFatal && !lastIsFatal;
        }
    }

    protected abstract GdalCplErr? ReleaseHandleCore();
}