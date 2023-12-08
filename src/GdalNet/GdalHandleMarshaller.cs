// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;

namespace MMKiwi.GdalNet;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedIn, typeof(GdalHandleMarshaller<,>.ManagedToUnmanagedIn))]
[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedRef, typeof(GdalHandleMarshaller<,>.ManagedToUnmanagedRef))]
[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedOut, typeof(GdalHandleMarshaller<,>.ManagedToUnmanagedOut))]
internal static class GdalHandleMarshaller<TRes, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] THandle> where THandle : GdalInternalHandleNeverOwns, new()
    where TRes : class, IConstructibleWrapper<TRes, THandle>
{
    public struct ManagedToUnmanagedIn
    {
        SafeHandleMarshaller<THandle>.ManagedToUnmanagedIn _marshaller;

        public void FromManaged(TRes? managed) => _marshaller.FromManaged(managed?.Handle!);

        public nint ToUnmanaged() => _marshaller.ToUnmanaged();

        public void Free() => _marshaller.Free();
    }

    public struct ManagedToUnmanagedRef
    {
        SafeHandleMarshaller<THandle>.ManagedToUnmanagedRef _marshaller;

        public ManagedToUnmanagedRef()
        {
            _marshaller = new();
        }

        public void FromManaged(TRes? managed) => _marshaller.FromManaged(managed?.Handle!);
        public nint ToUnmanaged() => _marshaller.ToUnmanaged();
        public void FromUnmanaged(nint value) => _marshaller.FromUnmanaged(value);
        public void OnInvoked() => _marshaller.OnInvoked();
        public TRes? ToManagedFinally()
        {
            var handle = _marshaller.ToManagedFinally();
            return handle.IsInvalid ? null : TRes.Construct(handle);
        }
        public void Free() => _marshaller.Free();
    }

    public struct ManagedToUnmanagedOut
    {
        SafeHandleMarshaller<THandle>.ManagedToUnmanagedOut _marshaller;

        public ManagedToUnmanagedOut()
        {
            _marshaller = new();
        }

        public void FromUnmanaged(nint value) => _marshaller.FromUnmanaged(value);

        public TRes? ToManaged()
        {
            var handle = _marshaller.ToManaged();
            return handle.IsInvalid ? null : TRes.Construct(handle);
        }

        public void Free() => _marshaller.Free();
    }
}

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedIn, typeof(GdalHandleMarshallerIn<,>.ManagedToUnmanagedIn))]
internal static class GdalHandleMarshallerIn<TRes, THandle> where THandle : GdalInternalHandle
    where TRes : class, IHasHandle<THandle>
{
    public struct ManagedToUnmanagedIn
    {
        SafeHandleMarshaller<THandle>.ManagedToUnmanagedIn _marshaller;

        public void FromManaged(TRes managed) => _marshaller.FromManaged(managed.Handle);

        public nint ToUnmanaged() => _marshaller.ToUnmanaged();

        public void Free() => _marshaller.Free();
    }
}

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedRef, typeof(GdalHandleMarshallerOutOwns<,>.ManagedToUnmanagedRef))]
[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedOut, typeof(GdalHandleMarshallerOutOwns<,>.ManagedToUnmanagedOut))]
internal static class GdalHandleMarshallerOutOwns<TRes, THandle> where THandle : GdalInternalHandle, IConstructibleHandle<THandle>
    where TRes : class, IConstructibleWrapper<TRes, THandle>
{

    /// <summary>
    /// Custom marshaller to marshal a <see cref="SafeHandle"/> as its underlying handle value.
    /// </summary>
    public struct ManagedToUnmanagedRef
    {
        private bool _addRefd;
        private bool _callInvoked;
        private THandle? _handle;
        private IntPtr _originalHandleValue;
        private readonly THandle _newHandle;
        private THandle? _handleToReturn;

        /// <summary>
        /// Create the marshaller in a default state.
        /// </summary>
        public ManagedToUnmanagedRef()
        {
            _addRefd = false;
            _callInvoked = false;
            // SafeHandle ref marshalling has always required parameterless constructors,
            // but it has never required them to be public.
            // We construct the handle now to ensure we don't cause an exception
            // before we are able to capture the unmanaged handle after the call.
            _newHandle = THandle.Construct(true);
        }

        /// <summary>
        /// Initialize the marshaller from a managed handle.
        /// </summary>
        /// <param name="handle">The managed handle</param>
        public void FromManaged(TRes wrapper)
        {
            _handle = wrapper.Handle;
            _handle.DangerousAddRef(ref _addRefd);
            _originalHandleValue = _handle.DangerousGetHandle();
        }

        /// <summary>
        /// Retrieve the unmanaged handle.
        /// </summary>
        /// <returns>The unmanaged handle</returns>
        public readonly IntPtr ToUnmanaged() => _originalHandleValue;

        /// <summary>
        /// Initialize the marshaller from an unmanaged handle.
        /// </summary>
        /// <param name="value">The unmanaged handle.</param>
        public void FromUnmanaged(IntPtr value)
        {
            if (value == _originalHandleValue)
            {
                _handleToReturn = _handle;
            }
            else
            {
                Marshal.InitHandle(_newHandle, value);
                _handleToReturn = _newHandle;
            }
        }

        /// <summary>
        /// Notify the marshaller that the native call has been invoked.
        /// </summary>
        public void OnInvoked()
        {
            _callInvoked = true;
        }

        /// <summary>
        /// Retrieve the managed handle from the marshaller.
        /// </summary>
        /// <returns>The managed handle.</returns>
        public readonly TRes? ToManagedFinally() => TRes.Construct(_handleToReturn!);

        /// <summary>
        /// Free any resources and reference counts owned by the marshaller.
        /// </summary>
        public void Free()
        {
            if (_addRefd)
            {
                _handle!.DangerousRelease();
            }

            // If we never invoked the call, then we aren't going to use the
            // new handle. Dispose it now to avoid clogging up the finalizer queue
            // unnecessarily.
            if (!_callInvoked)
            {
                _newHandle.Dispose();
            }
        }
    }

    /// <summary>
    /// Custom marshaller to marshal a <see cref="SafeHandle"/> as its underlying handle value.
    /// </summary>
    public struct ManagedToUnmanagedOut
    {
        private bool _initialized;
        private readonly THandle _newHandle;

        /// <summary>
        /// Create the marshaller in a default state.
        /// </summary>
        public ManagedToUnmanagedOut()
        {
            _initialized = false;
            // SafeHandle out marshalling has always required parameterless constructors,
            // but it has never required them to be public.
            // We construct the handle now to ensure we don't cause an exception
            // before we are able to capture the unmanaged handle after the call.
            _newHandle = THandle.Construct(true)!;
        }

        /// <summary>
        /// Initialize the marshaller from an unmanaged handle.
        /// </summary>
        /// <param name="value">The unmanaged handle.</param>
        public void FromUnmanaged(IntPtr value)
        {
            _initialized = true;
            Marshal.InitHandle(_newHandle, value);
        }

        /// <summary>
        /// Retrieve the managed handle from the marshaller.
        /// </summary>
        /// <returns>The managed handle.</returns>
        public readonly TRes? ToManaged() => TRes.Construct(_newHandle);

        /// <summary>
        /// Free any resources and reference counts owned by the marshaller.
        /// </summary>
        public readonly void Free()
        {
            // If we never captured the handle value, then we aren't going to use the
            // new handle. Dispose it now to avoid clogging up the finalizer queue
            // unnecessarily.
            if (!_initialized)
            {
                _newHandle!.Dispose();
            }
        }
    }
}

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedRef, typeof(GdalHandleMarshallerOutOwns<,>.ManagedToUnmanagedRef))]
[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedOut, typeof(GdalHandleMarshallerOutOwns<,>.ManagedToUnmanagedOut))]
internal static class GdalHandleMarshallerOutDoesntOwn<TRes, THandle> where THandle : GdalInternalHandle, IConstructibleHandle<THandle>
    where TRes : class, IConstructibleWrapper<TRes, THandle>
{

    /// <summary>
    /// Custom marshaller to marshal a <see cref="SafeHandle"/> as its underlying handle value.
    /// </summary>
    public struct ManagedToUnmanagedRef
    {
        private bool _addRefd;
        private bool _callInvoked;
        private THandle? _handle;
        private IntPtr _originalHandleValue;
        private readonly THandle _newHandle;
        private THandle? _handleToReturn;

        /// <summary>
        /// Create the marshaller in a default state.
        /// </summary>
        public ManagedToUnmanagedRef()
        {
            _addRefd = false;
            _callInvoked = false;
            // SafeHandle ref marshalling has always required parameterless constructors,
            // but it has never required them to be public.
            // We construct the handle now to ensure we don't cause an exception
            // before we are able to capture the unmanaged handle after the call.
            _newHandle = THandle.Construct(false);
        }

        /// <summary>
        /// Initialize the marshaller from a managed handle.
        /// </summary>
        /// <param name="handle">The managed handle</param>
        public void FromManaged(TRes wrapper)
        {
            _handle = wrapper.Handle;
            _handle.DangerousAddRef(ref _addRefd);
            _originalHandleValue = _handle.DangerousGetHandle();
        }

        /// <summary>
        /// Retrieve the unmanaged handle.
        /// </summary>
        /// <returns>The unmanaged handle</returns>
        public readonly IntPtr ToUnmanaged() => _originalHandleValue;

        /// <summary>
        /// Initialize the marshaller from an unmanaged handle.
        /// </summary>
        /// <param name="value">The unmanaged handle.</param>
        public void FromUnmanaged(IntPtr value)
        {
            if (value == _originalHandleValue)
            {
                _handleToReturn = _handle;
            }
            else
            {
                Marshal.InitHandle(_newHandle, value);
                _handleToReturn = _newHandle;
            }
        }

        /// <summary>
        /// Notify the marshaller that the native call has been invoked.
        /// </summary>
        public void OnInvoked()
        {
            _callInvoked = true;
        }

        /// <summary>
        /// Retrieve the managed handle from the marshaller.
        /// </summary>
        /// <returns>The managed handle.</returns>
        public readonly TRes? ToManagedFinally() => TRes.Construct(_handleToReturn!);

        /// <summary>
        /// Free any resources and reference counts owned by the marshaller.
        /// </summary>
        public void Free()
        {
            if (_addRefd)
            {
                _handle!.DangerousRelease();
            }

            // If we never invoked the call, then we aren't going to use the
            // new handle. Dispose it now to avoid clogging up the finalizer queue
            // unnecessarily.
            if (!_callInvoked)
            {
                _newHandle.Dispose();
            }
        }
    }

    /// <summary>
    /// Custom marshaller to marshal a <see cref="SafeHandle"/> as its underlying handle value.
    /// </summary>
    public struct ManagedToUnmanagedOut
    {
        private bool _initialized;
        private readonly THandle _newHandle;

        /// <summary>
        /// Create the marshaller in a default state.
        /// </summary>
        public ManagedToUnmanagedOut()
        {
            _initialized = false;
            // SafeHandle out marshalling has always required parameterless constructors,
            // but it has never required them to be public.
            // We construct the handle now to ensure we don't cause an exception
            // before we are able to capture the unmanaged handle after the call.
            _newHandle = THandle.Construct(false)!;
        }

        /// <summary>
        /// Initialize the marshaller from an unmanaged handle.
        /// </summary>
        /// <param name="value">The unmanaged handle.</param>
        public void FromUnmanaged(IntPtr value)
        {
            _initialized = true;
            Marshal.InitHandle(_newHandle, value);
        }

        /// <summary>
        /// Retrieve the managed handle from the marshaller.
        /// </summary>
        /// <returns>The managed handle.</returns>
        public readonly TRes? ToManaged() => TRes.Construct(_newHandle);

        /// <summary>
        /// Free any resources and reference counts owned by the marshaller.
        /// </summary>
        public readonly void Free()
        {
            // If we never captured the handle value, then we aren't going to use the
            // new handle. Dispose it now to avoid clogging up the finalizer queue
            // unnecessarily.
            if (!_initialized)
            {
                _newHandle!.Dispose();
            }
        }
    }
}