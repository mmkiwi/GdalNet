## Release 0.1

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
GDAL0001 | Reliability    | Warning  | Makes sure that a call to any GDAL Method (i.e. any method or class that is annotated with `[GdalEnforceErrorHandlingAttribute]` calls `GdalError.ThrowIfError()`
GDAL0002 | Reliability    | Warning  | Makes sure that any return value of `GdalCppErr` or `OgrError` is handled with a `ThrowIfError` statement.