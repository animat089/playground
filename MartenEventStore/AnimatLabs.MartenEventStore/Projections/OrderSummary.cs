using AnimatLabs.MartenEventStore.Events;
using Marten.Events.Aggregation;

namespace AnimatLabs.MartenEventStore.Projections;

public class OrderSummary
{
    public Guid Id { get; set; }
    public string Customer { get; set; } = "";
    public string Product { get; set; } = "";
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = "placed";
    public string? TrackingNumber { get; set; }
    public int Version { get; set; }
}

public class OrderSummaryProjection : SingleStreamProjection<OrderSummary>
{
    public OrderSummary Create(OrderPlaced e)
    {
        return new OrderSummary
        {
            Id = e.OrderId,
            Customer = e.Customer,
            Product = e.Product,
            Quantity = e.Quantity,
            Total = e.Total,
            Status = "placed"
        };
    }

    public void Apply(OrderConfirmed e, OrderSummary view)
    {
        view.Status = "confirmed";
    }

    public void Apply(OrderShipped e, OrderSummary view)
    {
        view.Status = "shipped";
        view.TrackingNumber = e.TrackingNumber;
    }

    public void Apply(OrderCancelled e, OrderSummary view)
    {
        view.Status = "cancelled";
    }
}
