// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

/// <summary>
///   A handle to a GDAL object that we do not have to close
/// </summary>
/// <remarks>
///   For example, an OGR layer should not be disposed of, 
///   to dispose of one, the underlying dataset should be
///   disposed of
/// </remarks>
public abstract class GdalHandle
{
    private nint Handle { get; set;  }
    protected void SetHandle(nint handle) => Handle = handle;

    [CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.Default, typeof(Marshal<>))]
    internal static class Marshal<T> where T : GdalHandle
    {
        public static nint ConvertToUnmanaged(T handle) => handle.Handle;
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
}

/// <summary>
///  A handle to a GDAL object that requires Disposal.
/// </summary>
/// <remarks>
///   Child classes should call the corresponding GDAL function
///   to dispose the object in the <see cref="ReleaseHandle"/>
///   method.
/// </remarks>
public abstract class GdalSafeHandle : GdalHandle, IDisposable
{
    private bool _disposedValue;
    
    protected abstract bool ReleaseHandle();

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // No managed objects
            }

            // Save last error from P/Invoke in case the implementation of ReleaseHandle
            // trashes it (important because this ReleaseHandle could occur implicitly
            // as part of unmarshaling another P/Invoke).
            int lastError = Marshal.GetLastPInvokeError();
            ReleaseHandle();
            Marshal.SetLastPInvokeError(lastError);

            _disposedValue = true;
        }
    }

    ~GdalSafeHandle()
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

