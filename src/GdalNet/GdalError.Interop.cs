// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

using unsafe CplErrHandle = delegate* unmanaged[Stdcall]<GdalCplErr, int, char*, void>;

public unsafe partial record class GdalError
{
    [CLSCompliant(false)]
    internal unsafe static partial class Interop
    {
        static Interop() => EnsureInitialize();

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial CplErrHandle CPLSetErrorHandler(CplErrHandle newHandler);

        [LibraryImport("gdal")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
        public static partial void CPLErrorReset();
    }

    internal static void EnsureInitialize()
    {
        if (!s_isRegistered)
        {
            Interop.CPLSetErrorHandler(GetStdcallAction());
            s_isRegistered = true;
        }
    }

    static bool s_isRegistered;

    private static CplErrHandle GetStdcallAction() => (CplErrHandle)Marshal.GetFunctionPointerForDelegate(HandleError);

    private unsafe static void HandleError(GdalCplErr error, int errorNum, char* messageUtf8)
    {
        string message = Marshal.PtrToStringUTF8((nint)messageUtf8) ?? string.Empty;
        GdalError errorInfo = new(error, errorNum, message);
        LastError = errorInfo;
        ErrorRaised?.Invoke(null, new(errorInfo));
    }

    public static void ResetErrors()
    {
        Interop.CPLErrorReset();
        LastError = null;
    }
}