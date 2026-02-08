using System;

namespace AnimatLabs.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class GenerateConfigurationAttribute : Attribute
{
    public string? SectionName { get; set; }
}
