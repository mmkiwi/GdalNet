// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;
using System.Diagnostics;

namespace MMKiwi.GdalNet;

public partial class OgrFeatureCollection : IEnumerable<OgrFeature>
{
    internal OgrFeatureCollection(OgrLayer layer)
    {
        Layer = layer;
    }

    private OgrLayer Layer { get; }

    public IEnumerator<OgrFeature> GetEnumerator()
    {
        return new FeatureEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void SetNetByIndex(long index){

    }

    private int _isEnumerating;

    private class FeatureEnumerator : IEnumerator<OgrFeature>
    {
        public FeatureEnumerator(OgrFeatureCollection ogrFeatureCollection)
        {

            if (Interlocked.CompareExchange(ref ogrFeatureCollection._isEnumerating, 0, 1) == 1)
                throw new InvalidOperationException($"Cannot enumerate an {nameof(OgrFeatureCollection)} multiple times. Dispose of the previous IEnumerator before continuing.");
            OgrFeatureCollection = ogrFeatureCollection;
            OgrLayer.Interop.OGR_L_ResetReading(OgrFeatureCollection.Layer);
        }

        public OgrFeature Current { get; private set; } = null!;

        private OgrFeatureCollection OgrFeatureCollection { get; }

        object IEnumerator.Current => Current;
        public void Dispose()
        {
            Debug.Assert(OgrFeatureCollection._isEnumerating is 1, "OgrFeatureCollection._isEnumerating should be 1 while enumerating.");
            OgrFeatureCollection._isEnumerating = 0;
        }

        public bool MoveNext()
        {
            Current = OgrLayer.Interop.OGR_L_GetNextFeature(OgrFeatureCollection.Layer);
            return Current != null;
        }

        public void Reset()
        {
            OgrLayer.Interop.OGR_L_ResetReading(OgrFeatureCollection.Layer);
        }
    }
}