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

RabbitMQ from `docker-compose.azure-local.yml` runs on port **5673** (not 5672):

```bash
docker-compose -f docker-compose.azure-local.yml up rabbitmq -d
```

## Requirements

- .NET 8.0 SDK
- No Docker required (InMemory by default)
