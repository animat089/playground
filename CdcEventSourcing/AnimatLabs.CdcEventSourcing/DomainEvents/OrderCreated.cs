namespace AnimatLabs.CdcEventSourcing.DomainEvents;

public record OrderCreated(
    int Id,
    string Customer,
    string Product,
    int Quantity,
    decimal TotalAmount,
    string Status,
    DateTimeOffset CreatedAt);
