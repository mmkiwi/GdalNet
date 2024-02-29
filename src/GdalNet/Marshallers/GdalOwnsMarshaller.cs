// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet.Marshallers;

/// <summary>
/// A marshaller for <see cref="SafeHandle"/>-derived types that marshals the handle following the lifetime rules for <see cref="SafeHandle"/>s.
/// </summary>
/// <typeparam name="THandle">The <see cref="SafeHandle"/>-derived type.</typeparam>
[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedRef, typeof(GdalOwnsMarshaller<,>.ManagedToUnmanagedRef))]
[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedOut, typeof(GdalOwnsMarshaller<,>.ManagedToUnmanagedOut))]
public static class GdalOwnsMarshaller<TWrapper, THandle>
    where THandle : SafeHandle, IConstructableHandle<THandle>
    where TWrapper : class, IHasHandle<THandle>, IConstructableWrapper<TWrapper, THandle>
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
        private THandle _newHandle;
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
        public void FromManaged(TWrapper handle)
        {
            _handle = handle.Handle;
            _handle.DangerousAddRef(ref _addRefd);
            _originalHandleValue = _handle.DangerousGetHandle();
        }

        /// <summary>
        /// Retrieve the unmanaged handle.
        /// </summary>
        /// <returns>The unmanaged handle</returns>
        public IntPtr ToUnmanaged() => _originalHandleValue;

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
        public TWrapper? ToManagedFinally() => _handleToReturn is { IsInvalid: false } ? TWrapper.Construct(_handleToReturn) : null;

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
        private THandle _newHandle;

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
            _newHandle = THandle.Construct(true);
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
        public TWrapper ToManaged() => _newHandle is { IsInvalid: false } ? TWrapper.Construct(_newHandle) : null;

        /// <summary>
        /// Free any resources and reference counts owned by the marshaller.
        /// </summary>
        public void Free()
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