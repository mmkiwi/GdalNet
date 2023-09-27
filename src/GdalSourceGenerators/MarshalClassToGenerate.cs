// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.SourceGenerators;

public readonly record struct MarshalClassToGenerate
{
    public readonly string Name;
    public readonly string? Namespace;
    public readonly MarshalBaseType BaseType;
    public readonly bool HasConstructor;
    public readonly bool IsAbstract;
    public readonly MarshalHidingType IsHiding;

    public MarshalClassToGenerate(string name, string? @namespace, MarshalBaseType baseType, bool hasConstructor, bool isAbstract, MarshalHidingType isHiding)
    {
        Name = name;
        Namespace = @namespace;
        BaseType = baseType;
        HasConstructor = hasConstructor;
        IsAbstract = isAbstract;
        IsHiding = isHiding;
    }
}
