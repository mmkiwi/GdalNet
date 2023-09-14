# GdalNet

This is a manual binding of [GDAL](https://gdal.org) to C#. It uses the new
marshaling infrastructure in newer versions of .NET for improved performance
and an easier to use, more .NET-like API.

## Status

Currently pre-alpha with very little implemented and a constantly changing API.
I wouldn't use this for any sort of development yet. This project is targeting
GDAL version 3.7.x as of right now. 

## License

This is licensed under the [MPL 2.0](LICENSE.md).

## Building

On Windows, this uses vcpkg to get and build GDAL. Initialize the vcpkg
submodule and run `.\vcpkg install` from the vcpkg folder. The gdal dlls
will automatically be copied to the target folder.