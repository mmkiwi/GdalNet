// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet.Handles;

internal sealed class GdalDatasetHandle : GdalInternalHandle, IConstructableHandle<GdalDatasetHandle>
{
    private protected override bool ReleaseHandleCore() => GdalH.GDALClose(handle) is not (GdalCplErr.Failure or GdalCplErr.Fatal);
         
    private GdalDatasetHandle(bool ownsHandle) : base(ownsHandle)
    {
             
    }
    public static GdalDatasetHandle Construct(bool ownsHandle) => new(ownsHandle);
}