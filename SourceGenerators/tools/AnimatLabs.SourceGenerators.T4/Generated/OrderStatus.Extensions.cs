namespace AnimatLabs.SourceGenerators.T4.Generated;

public static class OrderStatusExtensions
{
    public static string ToDisplayName(this OrderStatus value) => value switch
    {
        OrderStatus.Pending => "Pending Approval",
        OrderStatus.Processing => "In Progress",
        OrderStatus.Shipped => "Shipped",
        OrderStatus.Delivered => "Delivered",
        OrderStatus.Cancelled => "Cancelled",
        _ => value.ToString()
    };

    public static bool TryParse(string value, out OrderStatus result)
    {
        switch (value)
        {
            case "Pending Approval":
            case "Pending":
                result = OrderStatus.Pending;
                return true;
            case "In Progress":
            case "Processing":
                result = OrderStatus.Processing;
                return true;
            case "Shipped":
                result = OrderStatus.Shipped;
                return true;
            case "Delivered":
                result = OrderStatus.Delivered;
                return true;
            case "Cancelled":
                result = OrderStatus.Cancelled;
                return true;
            default:
                result = default;
                return false;
        }
    }
}
