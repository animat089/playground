namespace AnimatLabs.MartenEventStore.Events;

public record OrderPlaced(Guid OrderId, string Customer, string Product, int Quantity, decimal Total);
public record OrderConfirmed(Guid OrderId, DateTime ConfirmedAt);
public record OrderShipped(Guid OrderId, string TrackingNumber, DateTime ShippedAt);
public record OrderCancelled(Guid OrderId, string Reason, DateTime CancelledAt);
