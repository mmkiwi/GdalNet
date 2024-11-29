// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections;

using MMKiwi.GdalNet.Error;

namespace MMKiwi.GdalNet;

public static class GdalDriverManager
{
    public static GdalDriverList Drivers { get; } = new();
}

public class GdalDriverList : IReadOnlyList<GdalDriver>
{
    public IEnumerator<GdalDriver> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count
    {
        get
        {
            int result = GdalH.GDALGetDriverCount();
            GdalError.ThrowIfError();
            return result;
        }
    }

    public GdalDriver this[int index]
    {
        get
        {
            if (index > Count || index < 0)
                throw new IndexOutOfRangeException();
            
            var result = GdalH.GDALGetDriver(index);
            GdalError.ThrowIfError();
            return result ?? throw new IndexOutOfRangeException();
        }
    }

    public GdalDriver this[string name]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(name);

            var result = GdalH.GDALGetDriverByName(name);
            GdalError.ThrowIfError();
            return result ?? throw new KeyNotFoundException();
        }
    }
}