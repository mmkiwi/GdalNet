﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

global using System.Runtime.InteropServices;
global using System.Runtime.InteropServices.Marshalling;
global using System.Runtime.CompilerServices;
global using SuppressMessageAttribute = System.Diagnostics.CodeAnalysis.SuppressMessageAttribute;
[assembly: InternalsVisibleTo("MMKiwi.GdalNet.UnitTests")]
[assembly: CLSCompliant(true)]
[assembly: InternalsVisibleTo("MMKiwi.GdalNet.UnitTests.SourceGenerators")]
[assembly: InternalsVisibleTo(InternalUnitTestConst.AssemblyName)]

internal static class InternalUnitTestConst
{
    public const string AssemblyName = "MMKiwi.GdalNet.UnitTests.SourceGenerators.GeneratedSourceTest";
}