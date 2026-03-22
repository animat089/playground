namespace AnimatLabs.CdcEventSourcing.DomainEvents;

public record OrderUpdated(
    int Id,
    string? PreviousStatus,
    string CurrentStatus,
    string Customer,
    string Product,
    decimal TotalAmount,
    DateTimeOffset UpdatedAt);
