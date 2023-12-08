// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MMKiwi.GdalNet.InteropSourceGen;

public record class MethodGenerationInfo(MethodDeclarationSyntax Method, string TargetName);
