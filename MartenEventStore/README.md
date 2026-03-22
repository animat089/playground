# Marten Event Store

PostgreSQL as both document database and event store, using Marten.

An ASP.NET Core API stores order events (placed, confirmed, shipped, cancelled) in PostgreSQL through Marten's event sourcing. Inline projections build an `OrderSummary` read model from those events.

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
cd AnimatLabs.MartenEventStore
dotnet run
```

Place an order:

```bash
curl -X POST http://localhost:5192/api/orders \
  -H "Content-Type: application/json" \
  -d '{"customer":"alice","product":"Widget A","quantity":2,"total":49.98}'
```

Confirm it (replace the order ID):

```bash
curl -X POST http://localhost:5192/api/orders/{orderId}/confirm
```

View the projected summary:

```bash
curl http://localhost:5192/api/orders/{orderId}
```

View the raw event stream:

```bash
curl http://localhost:5192/api/orders/{orderId}/events
```

## What This Shows

- Event sourcing: state changes as immutable events
- Projections: read model derived from the stream
- Audit: full history via the event stream endpoint
- One database for events and projections

## Cleanup

```bash
docker-compose down -v
```
