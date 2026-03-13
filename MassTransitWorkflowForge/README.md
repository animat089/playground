# MassTransit + WorkflowForge: The Saga Pattern Done Right

MassTransit distributes the messages. WorkflowForge compensates the failures.

## What This Demonstrates

- **MassTransit message-driven architecture**: Order processing via pub/sub
- **WorkflowForge saga orchestration**: Multi-step workflow with automatic rollback
- **Real compensation**: When payment fails, stock reservation is released automatically
- **InMemory transport**: Runs without RabbitMQ for easy demo (RabbitMQ config included)

## The Saga

```
SubmitOrder → ReserveStock → ChargePayment → CreateShipment → OrderAccepted
                                  ↓ (failure)
                            ChargePayment.Compensate → ReserveStock.Compensate → OrderFailed
```

Orders over $500 simulate a payment gateway timeout. Watch the compensation cascade in reverse order.

## Run It

```bash
cd AnimatLabs.MassTransitWorkflowForge.OrderService
dotnet run
```

The app auto-submits two orders:
1. **$99.99** -- succeeds (all 3 steps complete)
2. **$999.99** -- fails at payment (compensation rolls back stock)

## Switch to RabbitMQ

Edit `Program.cs`: comment out `UsingInMemory`, uncomment `UsingRabbitMq`. Then start RabbitMQ:

```bash
docker run -d -p 5672:5672 -p 15672:15672 rabbitmq:management
```

## Requirements

- .NET 8.0 SDK
- No Docker required (InMemory transport by default)
