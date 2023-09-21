// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

/// <summary>
///   A handle to a GDAL object that should never be disposed explicitly.
/// </summary>
/// <remarks>
///   For example, an <see cref="OgrLayer"/> should not be disposed of, 
///   to dispose of one, the underlying dataset should be disposed instead.
/// </remarks>
public abstract class GdalHandle
{
    private protected GdalHandle() { }

    protected nint Handle { get; private set; }
    protected void SetHandle(nint handle) => Handle = handle;

    [CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.Default, typeof(Marshal<>))]
    private protected static class Marshal<T> where T : GdalHandle, IConstructibleHandle<T>
    {
        public static nint ConvertToUnmanaged(T? handle) => handle is null ? 0 : handle.Handle;
        public static T? ConvertToManaged(nint pointer)
        {
            return pointer <= 0 ? null : T.Construct(pointer);
        }
    }

    [CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.Default, typeof(MarshalAbstract<>))]
    private protected static class MarshalAbstract<T> where T : GdalHandle
    {
        public static nint ConvertToUnmanaged(T? handle) => handle is null ? 0 : handle.Handle;
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

    /// <summary>
    /// If <c>true</c>, the caller owns the handle and must call <see cref="Dispose()"/>. 
    /// If <c>false</c>, the caller does not own the handle and calling
    /// <see cref="Dispose()"/> will not release the handle.
    /// </summary>
    /// <remarks>
    /// Refer to the GDAL documentation for more information. For instance,
    /// an <see cref="OgrGeometry"/> can be owned by a parent
    /// <see cref="OgrFeature"/>, or it can be owned by calling code. If 
    /// a <see cref="OgrGeometry"/> is owned by a parent feature, dispose of 
    /// the feature to release the memory for the geometry. 
    /// </remarks>
    public bool OwnsHandle { get; protected init; } = true;

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

            if (OwnsHandle)
            {
                // Save last error from P/Invoke in case the implementation of ReleaseHandle
                // trashes it (important because this ReleaseHandle could occur implicitly
                // as part of unmarshaling another P/Invoke).
                int lastError = Marshal.GetLastPInvokeError();
                lock (s_releaseInterlock)
                    ReleaseHandle();
                Marshal.SetLastPInvokeError(lastError);
            }

            _disposedValue = true;
        }
    }

    private static readonly object s_releaseInterlock = new();

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
}

