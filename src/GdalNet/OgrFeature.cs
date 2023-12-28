// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Reflection.Metadata;

using MMKiwi.CBindingSG;
using MMKiwi.GdalNet.Handles;


namespace MMKiwi.GdalNet;

[CbsgGenerateWrapper]
public sealed partial class OgrFeature : IDisposable , IConstructableWrapper<OgrFeature, OgrFeatureHandle>, IHasHandle<OgrFeatureHandle>
{
    private bool _disposedValue;

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Handle.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
    }
}
