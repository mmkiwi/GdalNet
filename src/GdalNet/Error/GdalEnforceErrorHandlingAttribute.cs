// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.Error;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class GdalEnforceErrorHandlingAttribute : Attribute
{
    public GdalEnforceErrorHandlingAttribute(bool handleErrors = true)
    {
        HandleErrors = handleErrors;
    }
    public bool HandleErrors { get; set; }
}