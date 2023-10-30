namespace AnimatLabs.Api.Models;

/// <summary>
/// Base entity class with common properties
/// </summary>
public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedOn { get; set; }
}