# MassTransit + WorkflowForge: Saga Compensation

MassTransit consumer triggers a WorkflowForge saga with automatic compensation.

## Run

From the WorkflowForge solution root:

```bash
dotnet run --project AnimatLabs.WorkflowForge.MassTransitSaga.OrderService
```

Uses InMemory transport. Auto-submits two test orders:
- **$99.99** -- succeeds (all 3 steps)
- **$999.99** -- fails at payment (compensation rolls back stock)

## Switch to RabbitMQ (Optional)

Edit `Program.cs`: comment out `UsingInMemory`, uncomment `UsingRabbitMq`.

Start RabbitMQ:

```bash
docker-compose up -d
```

Management UI at http://localhost:15672 (guest/guest).

## Requirements

- .NET 8.0 SDK
- No Docker required (InMemory by default)
