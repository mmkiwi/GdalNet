// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;
using System.Diagnostics;

using MMKiwi.GdalNet.Error;
using MMKiwi.GdalNet.Interop;

namespace MMKiwi.GdalNet;

public class OgrFeatureCollection : IEnumerable<OgrFeature>
{
    internal OgrFeatureCollection(OgrLayer layer)
    {
        Layer = layer;
    }

    private OgrLayer Layer { get; }

    public bool TryGetNonEnumeratedCount(out long count)
    {
        count = OgrApiH.OGR_L_GetFeatureCount(Layer, false);
        GdalError.ThrowIfError();
        return count > 0;
    }

    public void Add(OgrFeature feature)
    {
        OgrApiH.OGR_L_CreateFeature(Layer, feature).ThrowIfError();
    }
    
    public void Add(params ReadOnlySpan<OgrFeature> features)
    {
        foreach(var feature in features)
            OgrApiH.OGR_L_CreateFeature(Layer, feature).ThrowIfError();
    }
    
    public long? GetCount(bool onlyIfCheap)
    {
        long count = OgrApiH.OGR_L_GetFeatureCount(Layer, false);
        GdalError.ThrowIfError();
        return count > 0 ? count : null;
    }

    public IEnumerator<OgrFeature> GetEnumerator()
    {
        return new FeatureEnumerator(this);
    }

    [ExcludeFromCodeCoverage]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private int _isEnumerating;

    private sealed class FeatureEnumerator : IEnumerator<OgrFeature>
    {
        public FeatureEnumerator(OgrFeatureCollection ogrFeatureCollection)
        {

            if (Interlocked.CompareExchange(ref ogrFeatureCollection._isEnumerating, 1, 0) == 1)
                throw new InvalidOperationException($"Cannot enumerate an {nameof(OgrFeatureCollection)} multiple times. Dispose of the previous IEnumerator before continuing.");
            OgrFeatureCollection = ogrFeatureCollection;
            OgrApiH.OGR_L_ResetReading(OgrFeatureCollection.Layer);
            GdalError.ThrowIfError();
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
            var current = OgrApiH.OGR_L_GetNextFeature(OgrFeatureCollection.Layer)!;
            GdalError.ThrowIfError();
            return (Current = current) != null;
        }

        public void Reset()
        {
            OgrApiH.OGR_L_ResetReading(OgrFeatureCollection.Layer);
            GdalError.ThrowIfError();
        }
    }
}