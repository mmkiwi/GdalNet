// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;
using System.Reflection;

namespace MMKiwi.GdalNet;

public sealed partial record class GdalError
{
#if DEBUG
    static GdalError()
    {
        ErrorRaised += (s,e) => DebugError(s,e);
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

    public static void ThrowIfError()
    {
        if (LastError == null || LastError.Severity == GdalCplErr.None)
        {
            return;
        }

        else if (LastError?.Severity == GdalCplErr.Failure || LastError?.Severity == GdalCplErr.Fatal)
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

    public static void DebugError(object? sender, GdalErrorEventArgs eventArgs)
    {
        if (eventArgs?.Error == null || eventArgs.Error.Severity == GdalCplErr.None)
        {
            return;
        }
        else 
            Debug.WriteLine($"GDAL Error Severity:{eventArgs.Error.Severity}, Code: {(int)eventArgs.Error.ErrorNum}, {eventArgs.Error.Message}", "GDAL");
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