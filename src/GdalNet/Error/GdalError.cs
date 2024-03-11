// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet.Error;

using unsafe CplErrHandle = delegate* unmanaged[Stdcall]<GdalCplErr, int, char*, void>;

public unsafe partial record GdalError
{
    [CLSCompliant(false)]
    internal static partial class Interop
    {
        [LibraryImport(GdalH.GdalDll)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial CplErrHandle CPLSetErrorHandler(CplErrHandle newHandler);

        [LibraryImport(GdalH.GdalDll)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial void CPLErrorReset();
        
        [LibraryImport(GdalH.GdalDll)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial int CPLGetLastErrorType();
        
        [LibraryImport(GdalH.GdalDll)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial int CPLGetLastErrorNo();

        [LibraryImport(GdalH.GdalDll)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        [return:MarshalUsing(typeof(Utf8StringNoFree))]
        public static partial string CPLGetLastErrorMsg();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Initialize()
    {
        if (s_isRegistered)
        {
            return;
        }

        Interop.CPLSetErrorHandler(&HandleError);
        s_isRegistered = true;
    }

    [ThreadStatic]
    static bool s_isRegistered;

    [CLSCompliant(false)]
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void HandleError(GdalCplErr error, int errorNum, char* messageUtf8)
    {
        string message = Marshal.PtrToStringUTF8((nint)messageUtf8) ?? string.Empty;
        GdalError errorInfo = new(error, errorNum, message);
        LastError = errorInfo;
        ErrorRaised?.Invoke(null, new GdalErrorEventArgs(errorInfo));
    }

    public static void ResetErrors()
    {
        Interop.CPLErrorReset();
        LastError = null;
    }

#if DEBUG
    static GdalError()
    {
        ErrorRaised += DebugError;
    }
#endif
    private GdalError(GdalCplErr severity, int errorNum, string message)
    {
        Severity = severity;
        ErrorNum = (ErrorCodes)errorNum;
        Message = message;
    }
    
    public static GdalError? GetLastError()
    {
        var severity = (GdalCplErr)Interop.CPLGetLastErrorType();
        return severity == GdalCplErr.None ? null : new GdalError(severity, Interop.CPLGetLastErrorNo(), Interop.CPLGetLastErrorMsg());
    }

    public static void EnableDebugLogging()
    {
        Initialize();
        // This won't raise an exception if it's not been hooked up yet
        // and prevents double subscribing.
        ErrorRaised -= DebugError;
        ErrorRaised += DebugError;
    }

    public static void DisableDebugLogging()
    {
        ErrorRaised -= DebugError;
    }

    public string Message { get; }
    public GdalCplErr Severity { get; }
    public ErrorCodes ErrorNum { get; }

    [field: ThreadStatic] public static GdalError? LastError { get; private set; }

    public static EventHandler<GdalErrorEventArgs>? ErrorRaised { get; set; }

    public enum ErrorCodes
    {
        None = 0,
        ApplicationDefined = 1,
        OutOfMemory = 2,
        FileIO = 3,
        OpenFailed = 4,
        IllegalArg = 5,
        NotSupported = 6,
        AssertionFailed = 7,
        NoWriteAccess = 8,
        UserInterrupt = 9,
        ObjectNull = 10
    }

    public static void ThrowIfError(GdalCplErr error)
    {
        ThrowIfError();
        if (error is GdalCplErr.Failure or GdalCplErr.Fatal)
        {
            throw new GdalException("Unknown GDAL error");
        }
    }

    public static void ThrowIfError(OgrError error)
    {
        ThrowIfError();
        if (error == OgrError.None)
            return;
        throw error switch
        {
            OgrError.NotEnoughData => throw new InvalidDataException("OGR: not enough data"),
            OgrError.NotEnoughMemory => throw new InsufficientMemoryException("OGR: not enough memory"),
            OgrError.UnsupportedGeometryType => throw new InvalidDataException("OGR: Unsupported Geometry Type"),
            OgrError.UnsupportedOperation => throw new InvalidOperationException("OGR: Unsupported Operation"),
            OgrError.CorruptData => throw new InvalidDataException("OGR: Corrupt Data"),
            OgrError.Failure => throw new GdalException("OGR: Failure"),
            OgrError.UnsupportedSRS => throw new InvalidDataException("OGR: Unsupported spatial reference"),
            OgrError.InvalidHandle => throw new InvalidDataException("OGR: Invalid handle"),
            OgrError.NonExistingFeature => throw new InvalidDataException("OGR: Non existing feature"),
            _ => throw new GdalException("Unknown OGR exception"),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfError()
    {
        GdalError? lastError = GetLastError(); 
        if (lastError?.Severity is not (GdalCplErr.Failure or GdalCplErr.Fatal))
        {
            return;
        }

        ThrowLastError(lastError);
    }
    private static void ThrowLastError(GdalError lastError)
    {

        try
        {
            throw lastError.ErrorNum switch
            {
                ErrorCodes.ApplicationDefined => new GdalException(
                    $"GDAL ERROR: Code:{(int)lastError.ErrorNum}, {lastError.Message}"),
                ErrorCodes.OutOfMemory => new InsufficientMemoryException(
                    $"GDAL ERROR: Code:{(int)lastError.ErrorNum}, {lastError.Message}"),
                ErrorCodes.FileIO => new IOException(
                    $"GDAL ERROR: Code:{(int)lastError.ErrorNum}, {lastError.Message}"),
                ErrorCodes.OpenFailed => new IOException(
                    $"GDAL ERROR: Code:{(int)lastError.ErrorNum}, {lastError.Message}"),
                ErrorCodes.IllegalArg => new ArgumentException(
                    $"GDAL ERROR: Code:{(int)lastError.ErrorNum}, {lastError.Message}"),
                ErrorCodes.NotSupported => new NotSupportedException(
                    $"GDAL ERROR: Code:{(int)lastError.ErrorNum}, {lastError.Message}"),
                ErrorCodes.AssertionFailed => new GdalException(
                    $"GDAL ERROR: Code: {(int)lastError.ErrorNum} , {lastError.Message}"),
                ErrorCodes.NoWriteAccess => new IOException(
                    $"GDAL ERROR: Code:{(int)lastError.ErrorNum}, {lastError.Message}"),
                ErrorCodes.UserInterrupt => new OperationCanceledException(
                    $"GDAL ERROR: Code:{(int)lastError.ErrorNum}, {lastError.Message}"),
                ErrorCodes.ObjectNull => new NullReferenceException(
                    $"GDAL ERROR: Code:{(int)lastError.ErrorNum}, {lastError.Message}"),
                _ => new Exception($"GDAL ERROR: {lastError.Message}")
            };
        }
        finally
        {
            Interop.CPLErrorReset();
        }
    }

    private static void DebugError(object? sender, GdalErrorEventArgs eventArgs)
    {
        if (eventArgs.Error.Severity == GdalCplErr.None)
        {
            return;
        }

        Debug.WriteLine(
            $"GDAL Error Severity:{eventArgs.Error.Severity}, Code: {(int)eventArgs.Error.ErrorNum}, {eventArgs.Error.Message}",
            "GDAL");
    }
}

[CustomMarshaller(typeof(OgrError), MarshalMode.ManagedToUnmanagedOut, typeof(ThrowMarshal))]
[CustomMarshaller(typeof(GdalCplErr), MarshalMode.ManagedToUnmanagedOut, typeof(ThrowMarshal))]
public static class ThrowMarshal
{
    public static OgrError ConvertToManaged(OgrError unmanaged)
    {
        GdalError.ThrowIfError(unmanaged);
        return unmanaged;
    }

    public static GdalCplErr ConvertToManaged(GdalCplErr unmanaged)
    {
        GdalError.ThrowIfError(unmanaged);
        return unmanaged;
    }
}

public sealed class GdalErrorEventArgs : EventArgs
{
    internal GdalErrorEventArgs(GdalError error)
    {
        Error = error;
    }

    public GdalError Error { get; }
}

public class GdalException : ApplicationException
{
    internal GdalException()
    {
    }

    internal GdalException(string? message) : base(message)
    {
    }

    internal GdalException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}