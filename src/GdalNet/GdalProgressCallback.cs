// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet;

public delegate int GdalProgressCallback(double pctComplete, string? message);
internal unsafe delegate int GdalProgressCallbackRaw(double pctComplete, byte* message, void* userData);
