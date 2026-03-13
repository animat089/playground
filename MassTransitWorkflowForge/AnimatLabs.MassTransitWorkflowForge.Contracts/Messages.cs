namespace AnimatLabs.MassTransitWorkflowForge.Contracts;

public record SubmitOrder(Guid OrderId, string CustomerEmail, decimal Amount);

public record OrderAccepted(Guid OrderId);
public record OrderFailed(Guid OrderId, string Reason);

public record ReserveStock(Guid OrderId, int Quantity);
public record StockReserved(Guid OrderId);
public record StockReservationFailed(Guid OrderId, string Reason);

public record ChargePayment(Guid OrderId, decimal Amount);
public record PaymentCharged(Guid OrderId, string TransactionId);
public record PaymentFailed(Guid OrderId, string Reason);

public record CreateShipment(Guid OrderId, string CustomerEmail);
public record ShipmentCreated(Guid OrderId, string TrackingNumber);
