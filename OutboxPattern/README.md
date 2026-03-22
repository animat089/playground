# Transactional Outbox Pattern

Writes business data and domain events in one database transaction using the outbox pattern.

The API creates orders and writes matching rows to an outbox table in the same transaction. Both commit or roll back together.

## Prerequisites

- Any OCI container runtime (Docker, Podman, Rancher Desktop) with Compose support
- .NET 8 SDK

## Run

Start PostgreSQL:

```bash
docker-compose up -d
```

Start the API:

```bash
cd AnimatLabs.OutboxPattern
dotnet run
```

Create an order:

```bash
curl -X POST http://localhost:5190/api/orders \
  -H "Content-Type: application/json" \
  -d '{"customer":"alice","product":"Widget A","quantity":2,"totalAmount":49.98}'
```

Check the outbox:

```bash
curl http://localhost:5190/api/outbox
```

The order row and outbox event share one committed transaction.

## Connecting to CDC

Point a Debezium connector at the outbox table (see [CdcEventSourcing](../CdcEventSourcing/)) to stream events to Kafka.

## Cleanup

```bash
docker-compose down -v
```
