// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Handles;
using MMKiwi.GdalNet.InteropAttributes;

namespace MMKiwi.GdalNet;

[GdalGenerateWrapper]
public sealed partial class GdalDataset : GdalMajorObject, IConstructableWrapper<GdalDataset, GdalDatasetHandle>, IHasHandle<GdalDatasetHandle>
{
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    private GdalDataset(GdalDatasetHandle handle) : base(handle)
    {
        RasterBands = new GdalBandCollection(this);
        Layers = new OgrLayerCollection(this);
    }

    /// <summary>
    /// <para>Open a raster or vector file as a GDALDataset.</para>
    /// <para>
    ///  This function will try to open the passed file, or virtual dataset name by invoking the Open method of each
    ///  registered GDALDriver in turn.The first successful open will result in a returned dataset.If all drivers fail
    ///  then NULL is returned and an error is issued.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    ///  This is the equivalent to the GDAL C function <a href="https://gdal.org/api/raster_c_api.html#_CPPv410GDALOpenExPKcjPPCKcPPCKcPPCKc">GDALOpenEx()</a>  
    /// </para>
    /// <para>Several recommendations:</para>
    /// <list type="bullet">
    ///  <item>
    ///   <description>
    ///    If you open a dataset object with GDAL_OF_UPDATE access, it is not recommended to open a new dataset on the
    ///    same underlying file.
    ///   </description>
    ///  </item>
    ///  <item>
    ///   <description>
    ///    The returned dataset should only be accessed by one thread at a time. If you want to use it from different
    ///    threads, you must add all necessary code (mutexes, etc.) to avoid concurrent use of the object. (Some
    ///    drivers, such as GeoTIFF, maintain internal state variables that are updated each time a new block is read,
    ///    thus preventing concurrent use.) 
    ///   </description>
    ///  </item>
    /// </list>
    /// <para>
    ///  For drivers supporting the VSI virtual file API, it is possible to open a file in a.zip archive (see
    ///  VSIInstallZipFileHandler()), in a.tar/.tar.gz/.tgz archive(see VSIInstallTarFileHandler()) or on a HTTP / FTP
    ///  server(see VSIInstallCurlFileHandler())
    /// </para>
    /// <para>
    ///  In order to reduce the need for searches through the operating system file system machinery, it is possible to
    ///  give an optional list of files with the <c>siblingFiles</c> parameter.This is the list of all files at the same
    ///  level in the file system as the target file, including the target file. The filenames must not include any path
    ///  components, are essentially just the output of VSIReadDir() on the parent directory.If the target object does
    ///  not have filesystem semantics then the file list should be NULL.
    /// </para>
    /// </remarks>
    /// <param name="fileName">
    ///  the name of the file to access. In the case of exotic drivers this may not refer to a physical file, but
    ///  instead contain information for the driver on how to access a dataset. It should be in UTF-8 encoding.
    /// </param>
    /// <param name="openSettings">
    ///   the GDAL settings to use when opening the dataset.
    /// </param>
    /// <param name="allowedDrivers">
    ///   null to consider all candidate drivers, or a list of strings with the driver short names that must be
    ///   considered.
    /// </param>
    /// <param name="openOptions">
    ///  null, or a list of strings with open options passed to candidate drivers. An option exists for all drivers,
    ///  OVERVIEW_LEVEL=level, to select a particular overview level of a dataset. The level index starts at 0. The
    ///  level number can be suffixed by "only" to specify that only this overview level must be visible, and not
    ///  sub-levels. Open options are validated by default, and a warning is emitted in case the option is not
    ///  recognized. In some scenarios, it might be not desirable (e.g. when not knowing which driver will open the
    ///  file), so the special open option VALIDATE_OPEN_OPTIONS can be set to NO to avoid such warnings.
    ///  Alternatively, since GDAL 2.1, an option name can be preceded by the @ character to indicate that it may not
    ///  cause a warning if the driver doesn't declare this option. Starting with GDAL 3.3, OVERVIEW_LEVEL=NONE is
    ///  supported to indicate that no overviews should be exposed.
    /// </param>
    /// <param name="siblingFiles">
    ///  <c>null</c>, or a list of strings that are filenames that are auxiliary to the main filename. If <c>null</c> is
    ///  passed, a probing of the file system will be done.
    /// </param>
    /// <returns>A GdalDataset handle</returns>
    /// <exception cref="ArgumentException">Thrown if there is not enough memory to open the file.</exception>
    /// <exception cref="IOException">Thrown if the file doesn't exist or there was another error opening it.</exception>
    /// <exception cref="InsufficientMemoryException">Thrown if there is not enough memory to open the file.</exception>
    /// <exception cref="NotSupportedException">Thrown if the file format is not supported.</exception>
    /// <exception cref="GdalException">Thrown if there is any other issue opening the file.</exception>
    public static GdalDataset Open(string fileName,
                                    GdalOpenSettings? openSettings = null,
                                    IEnumerable<string>? allowedDrivers = null,
                                    IReadOnlyDictionary<string, string>? openOptions = null,
                                    IEnumerable<string>? siblingFiles = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        GdalOpenSettings openFlags = openSettings ?? new();
        var dataset = Interop.GDALOpenEx(fileName, openFlags.Flags, allowedDrivers, openOptions, siblingFiles);
        if (dataset == null)
            throw new GdalException("Could not open file");
        return dataset;
    }

    private new GdalDatasetHandle Handle => (GdalDatasetHandle)base.Handle;

    public GdalBandCollection RasterBands { get; }
    public OgrLayerCollection Layers { get; }
    public int RasterXSize => Interop.GDALGetRasterXSize(this);
    public int RasterYSize => Interop.GDALGetRasterYSize(this);
}