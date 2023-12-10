// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;

namespace MMKiwi.GdalNet;

public sealed partial record GdalError
{
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
    public string Message { get; }
    public GdalCplErr Severity { get; }
    public ErrorCodes ErrorNum { get; }

    [field: ThreadStatic]
    public static GdalError? LastError { get; private set; }

    public static EventHandler<GdalErrorEventArgs>? ErrorRaised { get; }

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
        if (error == GdalCplErr.Failure || error == GdalCplErr.Fatal)
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
            OgrError.Falure => throw new GdalException("OGR: Failure"),
            OgrError.UnsupportedSRS => throw new InvalidDataException("OGR: Unsupported spatial reference"),
            OgrError.InvalidHandle => throw new InvalidDataException("OGR: Invalid handle"),
            OgrError.NonExistingFeature => throw new InvalidDataException("OGR: Non existing feature"),
            _ => throw new GdalException("Unknown OGR exception"),
        };
    }


    public static void ThrowIfError()
    {
        if (LastError == null || LastError.Severity == GdalCplErr.None)
        {
            return;
        }

        if (LastError.Severity is GdalCplErr.Failure or GdalCplErr.Fatal)
        {
            try
            {
                throw LastError.ErrorNum switch
                {
                    ErrorCodes.ApplicationDefined => new Exception($"GDAL ERROR: Code:{(int)LastError.ErrorNum}, {LastError.Message}"),
                    ErrorCodes.OutOfMemory => new InsufficientMemoryException($"GDAL ERROR: Code:{(int)LastError.ErrorNum}, {LastError.Message}"),
                    ErrorCodes.FileIO => new IOException($"GDAL ERROR: Code:{(int)LastError.ErrorNum}, {LastError.Message}"),
                    ErrorCodes.OpenFailed => new IOException($"GDAL ERROR: Code:{(int)LastError.ErrorNum}, {LastError.Message}"),
                    ErrorCodes.IllegalArg => new ArgumentException($"GDAL ERROR: Code:{(int)LastError.ErrorNum}, {LastError.Message}"),
                    ErrorCodes.NotSupported => new NotSupportedException($"GDAL ERROR: Code:{(int)LastError.ErrorNum}, {LastError.Message}"),
                    ErrorCodes.AssertionFailed => new Exception($"GDAL ERROR: Code: {(int)LastError.ErrorNum} , {LastError.Message}"),
                    ErrorCodes.NoWriteAccess => new IOException($"GDAL ERROR: Code:{(int)LastError.ErrorNum}, {LastError.Message}"),
                    ErrorCodes.UserInterrupt => new OperationCanceledException($"GDAL ERROR: Code:{(int)LastError.ErrorNum}, {LastError.Message}"),
                    ErrorCodes.ObjectNull => new NullReferenceException($"GDAL ERROR: Code:{(int)LastError.ErrorNum}, {LastError.Message}"),
                    _ => new Exception($"GDAL ERROR: {LastError.Message}")
                };
            }
            finally
            {
                LastError = null;
            }
        }
    }

    private static void DebugError(object? sender, GdalErrorEventArgs eventArgs)
    {
        if (eventArgs.Error.Severity == GdalCplErr.None)
        {
            return;
        }

        Debug.WriteLine($"GDAL Error Severity:{eventArgs.Error.Severity}, Code: {(int)eventArgs.Error.ErrorNum}, {eventArgs.Error.Message}", "GDAL");
    }

    [CustomMarshaller(typeof(OgrError), MarshalMode.ManagedToUnmanagedOut, typeof(ThrowMarshal))]
    [CustomMarshaller(typeof(GdalCplErr), MarshalMode.ManagedToUnmanagedOut, typeof(ThrowMarshal))]
    public static class ThrowMarshal
    {
        public static OgrError ConvertToManaged(OgrError unmanaged)
        {
            ThrowIfError(unmanaged);
            return unmanaged;
        }

        public static GdalCplErr ConvertToManaged(GdalCplErr unmanaged)
        {
            ThrowIfError(unmanaged);
            return unmanaged;
        }
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
    public GdalException()
    {
    }

    public GdalException(string? message) : base(message)
    {
    }

    public GdalException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}