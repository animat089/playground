using System;

namespace AnimatLabs.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class AutoToStringAttribute : Attribute
{
    public bool IncludePrivate { get; set; }
    public string[]? Exclude { get; set; }
}
