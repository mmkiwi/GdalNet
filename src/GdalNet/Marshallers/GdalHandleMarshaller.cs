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
[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.ManagedToUnmanagedIn, typeof(GdalMarshaller<,>.ManagedToUnmanagedIn))]
public static class GdalMarshaller<TWrapper, THandle>
    where THandle : SafeHandle
    where TWrapper : IHasHandle<THandle>
{
    /// <summary>
    /// Custom marshaller to marshal a <see cref="SafeHandle"/> as its underlying handle value.
    /// </summary>
    public struct ManagedToUnmanagedIn
    {
        private bool _addRefd;
        private THandle? _handle;

        /// <summary>
        /// Initializes the marshaller from a managed handle.
        /// </summary>
        /// <param name="handle">The managed handle.</param>
        public void FromManaged(TWrapper? wrapper)
        {
            _handle = wrapper?.Handle;
            _handle?.DangerousAddRef(ref _addRefd);
        }

        /// <summary>
        /// Get the unmanaged handle.
        /// </summary>
        /// <returns>The unmanaged handle.</returns>
        public IntPtr ToUnmanaged() => _handle?.DangerousGetHandle() ?? IntPtr.Zero;

        /// <summary>
        /// Release any references keeping the managed handle alive.
        /// </summary>
        public void Free()
        {
            if (_addRefd)
            {
                _handle!.DangerousRelease();
            }
        }
    }
}