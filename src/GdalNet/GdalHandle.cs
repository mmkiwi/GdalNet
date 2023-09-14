// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public abstract class GdalHandle : IDisposable
{
    private bool _disposedValue;
    private nint _handle;

    protected bool OwnsHandle { get; }

    [CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.Default, typeof(Marshal<>))]
    internal static class Marshal<T> where T : GdalHandle
    {
        public static nint ConvertToUnmanaged(T gdalDataset) => gdalDataset._handle;
        public static T? ConvertToManaged(nint pointer)
        {
            if (pointer <= 0)
            {
                return null;
            }
            else
            {
                T? handle = Activator.CreateInstance(typeof(T), true) as T;
                handle?.SetHandle(pointer);
                return handle;
            }
        }
    }

    protected GdalHandle(bool ownsHandle)
    {
        OwnsHandle = ownsHandle;
    }

    protected void SetHandle(nint handle) => _handle = handle;

    protected abstract bool ReleaseHandle();

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            if (OwnsHandle)
            {
                // Save last error from P/Invoke in case the implementation of ReleaseHandle
                // trashes it (important because this ReleaseHandle could occur implicitly
                // as part of unmarshaling another P/Invoke).
                int lastError = Marshal.GetLastPInvokeError();
                ReleaseHandle();
                Marshal.SetLastPInvokeError(lastError);
            }

            _disposedValue = true;
        }
    }

    ~GdalHandle()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize")]
    public void Close()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

