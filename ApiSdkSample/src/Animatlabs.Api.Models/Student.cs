namespace AnimatLabs.Api.Models;

/// <summary>
/// Student model inheriting from EntityBase
/// </summary>
public class Student : EntityBase
{
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
}