// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.ComponentModel;

using NetEscapades.EnumGenerators;

namespace MMKiwi.GdalNet.InteropAttributes;

[EnumExtensions]
public enum ConstructorVisibility
{
    [Description("public")] Public,
    [Description("protected")] Protected,
    [Description("internal")] Internal,
    [Description("protected internal")] ProtectedInternal,
    [Description("private")] Private,
    [Description("private protected")] PrivateProtected
}