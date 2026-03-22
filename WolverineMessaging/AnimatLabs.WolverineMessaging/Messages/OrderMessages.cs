using System;

namespace AnimatLabs.WolverineMessaging.Messages;

public record CreateOrder(string Customer, string Product, int Quantity, decimal Total);
public record OrderCreated(Guid OrderId, string Customer, string Product, int Quantity, decimal Total);
public record NotifyWarehouse(Guid OrderId, string Product, int Quantity);
public record WarehouseNotified(Guid OrderId, DateTime NotifiedAt);
