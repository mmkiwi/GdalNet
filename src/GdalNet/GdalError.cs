// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Reflection;

namespace MMKiwi.GdalNet;

public sealed partial record class GdalError
{
    private GdalError(GdalCplErr severity, int errorNum, string message)
    {
        Severity = severity;
        ErrorNum = errorNum;
        Message = message;
    }
    public string Message { get; }
    public GdalCplErr Severity { get; }
    public int ErrorNum { get; }
    public static GdalError? LastError { get; private set; }

    public static EventHandler<GdalErrorEventArgs>? ErrorRaised { get; }
}

public sealed class GdalErrorEventArgs : EventArgs
{
    internal GdalErrorEventArgs(GdalError error)
    {
        Error = error;
    }

    public GdalError Error { get; }
}