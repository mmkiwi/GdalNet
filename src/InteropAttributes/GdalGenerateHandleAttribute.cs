// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace MMKiwi.GdalNet.InteropAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class GdalGenerateHandleAttribute : Attribute
{
    public GenerateType GenerateOwns { get; set; } = GenerateType.Auto;
    public GenerateType GenerateDoesntOwn { get; set; } = GenerateType.Auto;
}

public enum GenerateType
{
    Auto,
    Generate,
    Omit
}