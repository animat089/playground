using AnimatLabs.SourceGenerators.Attributes;

namespace AnimatLabs.SourceGenerators.Demo.Models;

[AutoToString]
public partial class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
}
