// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;
using MMKiwi.GdalNet.Marshallers;

namespace MMKiwi.GdalNet;

[CLSCompliant(false)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[GdalEnforceErrorHandling]
internal static partial class OgrSrsApiH
{
    [LibraryImport(GdalH.GdalDll)]
    public static partial void OSRDestroySpatialReference(nint srs);
    
    [LibraryImport(GdalH.GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalUsing(typeof(GdalOwnsMarshaller<OgrSpatialReference, OgrSpatialReferenceHandle>))]    
    public static partial OgrSpatialReference OSRNewSpatialReference(string? wkt);
    
    [LibraryImport(GdalH.GdalDll)]
    public static partial OgrError OSRImportFromEPSG(OgrSpatialReference srs, int epsg);
    
    [LibraryImport(GdalH.GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    public static partial OgrError OSRImportFromWkt(OgrSpatialReference srs, in string wkt);
    
    [LibraryImport(GdalH.GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    public static partial OgrError OSRExportToWkt(OgrSpatialReference srs, out string? wkt);

    [LibraryImport(GdalH.GdalDll, StringMarshalling = StringMarshalling.Utf8)]
    public static partial string? OSRGetName(OgrSpatialReference srs);

}