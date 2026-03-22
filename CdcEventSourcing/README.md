# CDC Event Sourcing

Captures database row changes as typed domain events using Debezium, Kafka, and .NET.

PostgreSQL WAL feeds Debezium, which streams change events into Kafka. A .NET console app consumes those events and maps them to `OrderCreated`, `OrderUpdated`, and delete notifications.

## Prerequisites

- Any OCI container runtime (Docker, Podman, Rancher Desktop) with Compose support
- .NET 8 SDK

## Run

Start the infrastructure:

```bash
docker-compose up -d
```

This starts PostgreSQL (`wal_level=logical`), Kafka (KRaft, no ZooKeeper), Debezium Connect, and Kafka UI.

Images are free and open-source. Kafka uses the official `apache/kafka` image (Apache 2.0). Works with Docker, Podman, or any OCI-compatible runtime.

Wait about 30 seconds for Debezium to be ready, then register the connector:

```bash
curl -X POST http://localhost:8083/connectors \
  -H "Content-Type: application/json" \
  -d @register-connector.json
```

Run the consumer:

```bash
cd AnimatLabs.CdcEventSourcing
dotnet run
```

Open another terminal and insert or update rows:

```bash
docker exec -it cdc-postgres psql -U postgres -d orders -c \
  "INSERT INTO orders (customer, product, quantity, total_amount) VALUES ('dave', 'Widget D', 3, 74.97);"
```

```bash
docker exec -it cdc-postgres psql -U postgres -d orders -c \
  "UPDATE orders SET status = 'shipped' WHERE customer = 'alice';"
```

The consumer prints typed domain events as they arrive.

## URLs

- Kafka UI: http://localhost:8080
- Debezium API: http://localhost:8083/connectors

## Cleanup

```bash
docker-compose down -v
```
