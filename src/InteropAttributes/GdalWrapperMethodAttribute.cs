using System;
using System.Collections.Generic;
using System.Text;

namespace MMKiwi.GdalNet.InteropAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class GdalWrapperMethodAttribute : Attribute
{
    public string? MethodName { get; set; }
}
