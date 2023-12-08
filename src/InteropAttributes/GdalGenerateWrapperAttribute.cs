// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace MMKiwi.GdalNet.InteropAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class GdalGenerateWrapperAttribute : Attribute
{
    public MemberVisibility ConstructorVisibility { get; set; } = MemberVisibility.Private;
    public MemberVisibility HandleVisibility { get; set; } = MemberVisibility.Internal;
    public MemberVisibility HandleSetVisibility { get; set; } = MemberVisibility.DoNotGenerate;
}
