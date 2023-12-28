// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

global using SuppressMessageAttribute = System.Diagnostics.CodeAnalysis.SuppressMessageAttribute;
global using System.Runtime.InteropServices;
global using MMKiwi.GdalNet.Handles;
global using System.Diagnostics.CodeAnalysis;

using System.Runtime.CompilerServices;

using MMKiwi.GdalNet;

[assembly: InternalsVisibleTo("MMKiwi.GdalNet.UnitTests")]
[assembly: CLSCompliant(true)]
[assembly: InternalsVisibleTo("MMKiwi.GdalNet.UnitTests.SourceGenerators")]
[assembly: InternalsVisibleTo(InternalUnitTestConst.AssemblyName)]
[assembly: DisableRuntimeMarshalling]
namespace MMKiwi.GdalNet;

internal static class InternalUnitTestConst
{
    public const string AssemblyName = "MMKiwi.GdalNet.UnitTests.SourceGenerators.GeneratedSourceTest";
}