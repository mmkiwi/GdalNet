// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Reflection;

namespace MMKiwi.GdalNet;

[NativeMarshalling(typeof(GdalHandleMarshallerIn<OgrGeometry, MarshalHandle>))]
public abstract partial class OgrGeometry : IHasHandle<OgrGeometry.MarshalHandle>
{
    internal MarshalHandle Handle { get; }

    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    private protected OgrGeometry(MarshalHandle handle) => this.Handle = handle;

    internal sealed class MarshalHandle : GdalInternalHandle, IConstructibleHandle<MarshalHandle>
    {
        private MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        public static MarshalHandle Construct(bool ownsHandle)
            => new(ownsHandle);

        protected override bool ReleaseHandle()
        {
            lock (ReentrantLock)
            {
                GdalError.ResetErrors();
                Interop.OGR_G_DestroyGeometry(handle);
                return GdalError.LastError is not null && GdalError.LastError.Severity is not GdalCplErr.Failure or GdalCplErr.Fatal;
            }
        }
    }

    [CustomMarshaller(typeof(OgrGeometry), MarshalMode.ManagedToUnmanagedRef, typeof(MarshallerOutOwns.ManagedToUnmanagedRef))]
    [CustomMarshaller(typeof(OgrGeometry), MarshalMode.ManagedToUnmanagedOut, typeof(MarshallerOutOwns.ManagedToUnmanagedOut))]
    internal static class MarshallerOutOwns
    {

        /// <summary>
        /// Custom marshaller to marshal a <see cref="SafeHandle"/> as its underlying handle value.
        /// </summary>
        public struct ManagedToUnmanagedRef
        {
            private bool _addRefd;
            private bool _callInvoked;
            private MarshalHandle? _handle;
            private IntPtr _originalHandleValue;
            private readonly MarshalHandle _newHandle;
            private MarshalHandle? _handleToReturn;

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
                _newHandle = MarshalHandle.Construct(true);
            }

            /// <summary>
            /// Initialize the marshaller from a managed handle.
            /// </summary>
            /// <param name="handle">The managed handle</param>
            public void FromManaged(OgrGeometry wrapper)
            {
                _handle = wrapper.Handle;
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
            public OgrGeometry? ToManagedFinally() => ConstructGeometry(_handleToReturn!);

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
            private readonly MarshalHandle _newHandle;

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
                _newHandle = MarshalHandle.Construct(true)!;
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
            public OgrGeometry? ToManaged() => ConstructGeometry(_newHandle);

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

    [CustomMarshaller(typeof(OgrGeometry), MarshalMode.ManagedToUnmanagedRef, typeof(MarshallerOutOwns.ManagedToUnmanagedRef))]
    [CustomMarshaller(typeof(OgrGeometry), MarshalMode.ManagedToUnmanagedOut, typeof(MarshallerOutOwns.ManagedToUnmanagedOut))]
    internal static class MarshallerOutDoesntOwn
    {

        /// <summary>
        /// Custom marshaller to marshal a <see cref="SafeHandle"/> as its underlying handle value.
        /// </summary>
        public struct ManagedToUnmanagedRef
        {
            private bool _addRefd;
            private bool _callInvoked;
            private MarshalHandle? _handle;
            private IntPtr _originalHandleValue;
            private readonly MarshalHandle _newHandle;
            private MarshalHandle? _handleToReturn;

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
                _newHandle = MarshalHandle.Construct(false);
            }

            /// <summary>
            /// Initialize the marshaller from a managed handle.
            /// </summary>
            /// <param name="handle">The managed handle</param>
            public void FromManaged(OgrGeometry wrapper)
            {
                _handle = wrapper.Handle;
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
            public OgrGeometry? ToManagedFinally() => ConstructGeometry(_handleToReturn);

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
            private readonly MarshalHandle _newHandle;

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
                _newHandle = MarshalHandle.Construct(false)!;
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
            public OgrGeometry? ToManaged() => ConstructGeometry(_newHandle);

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

    private static OgrGeometry? ConstructGeometry(MarshalHandle? handle)
    {
        if (handle is null || handle.IsInvalid)
            return null;
        else
        {
            OgrWkbGeometryType type = Interop.OGR_G_GetGeometryType(handle);
            return type switch
            {
                OgrWkbGeometryType.Point => new OgrPoint(handle),//
                OgrWkbGeometryType.LineString => throw new NotImplementedException(),//
                OgrWkbGeometryType.Polygon => throw new NotImplementedException(),//
                OgrWkbGeometryType.MultiPoint => throw new NotImplementedException(),//
                OgrWkbGeometryType.MultiLineString => throw new NotImplementedException(),//
                OgrWkbGeometryType.MultiPolygon => throw new NotImplementedException(),//
                OgrWkbGeometryType.GeometryCollection => throw new NotImplementedException(),//
                OgrWkbGeometryType.CircularString => throw new NotImplementedException(),//
                OgrWkbGeometryType.CompoundCurve => throw new NotImplementedException(),//
                OgrWkbGeometryType.CurvePolygon => throw new NotImplementedException(),//
                OgrWkbGeometryType.MultiCurve => throw new NotImplementedException(),//
                OgrWkbGeometryType.MultiSurface => throw new NotImplementedException(),//
                OgrWkbGeometryType.Curve => throw new NotImplementedException(),
                OgrWkbGeometryType.Surface => throw new NotImplementedException(),
                OgrWkbGeometryType.PolyhedralSurface => throw new NotImplementedException(),//
                OgrWkbGeometryType.TIN => throw new NotImplementedException(),//
                OgrWkbGeometryType.Triangle => throw new NotImplementedException(),//
                OgrWkbGeometryType.LinearRing => throw new NotImplementedException(),//
                _ => new UnknownGeometry(handle),
            };
        }
    }
    private class UnknownGeometry : OgrGeometry, IConstructibleWrapper<UnknownGeometry, OgrGeometry.MarshalHandle>
    {
        public UnknownGeometry(MarshalHandle handle) : base(handle)
        {
        }

        static UnknownGeometry? IConstructibleWrapper<UnknownGeometry, MarshalHandle>.Construct(MarshalHandle handle)
            => new(handle);
    }
}
