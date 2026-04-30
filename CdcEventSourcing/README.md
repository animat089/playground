# CDC Event Sourcing

Captures PostgreSQL row changes with Debezium, streams them through Apache Kafka, and maps them to typed .NET events.

This sample keeps the application write path simple: write to PostgreSQL, let Debezium read the WAL, and let a .NET consumer react to committed changes.

## Prerequisites

- Any OCI container runtime (Docker, Podman, Rancher Desktop) with Compose support
- .NET 8 SDK

The compose file uses free/open-source images:

- `postgres:16-alpine`
- `apache/kafka:3.7.0` in KRaft mode
- `debezium/connect:2.5`
- `provectuslabs/kafka-ui`

## Run

Start the infrastructure:

```bash
docker-compose up -d
```

This starts PostgreSQL (`wal_level=logical`), Kafka (KRaft, no ZooKeeper), Debezium Connect, and Kafka UI.

Images are free and open-source. Kafka uses the official `apache/kafka` image (Apache 2.0). Works with Docker, Podman, or any OCI-compatible runtime.

Wait about 30 seconds for Debezium to be ready, then register the connector:

```bash
curl.exe -X POST http://localhost:8083/connectors \
  -H "Content-Type: application/json" \
  -d @register-connector.json
```

On macOS/Linux, `curl` is fine. On Windows PowerShell, use `curl.exe`.

Run the consumer:

```bash
cd AnimatLabs.CdcEventSourcing
dotnet run
```

Expected snapshot output:

```text
Listening on orders.public.orders. Insert or update rows in the orders table to see events.
Press Ctrl+C to stop.

[OrderCreated] #1 alice bought 2x Widget A for $49.98
[OrderCreated] #2 bob bought 1x Widget B for $24.99
[OrderCreated] #3 carol bought 5x Widget C for $124.95
```

Open another terminal and insert or update rows:

```bash
docker exec cdc-postgres psql -U postgres -d orders -c \
  "INSERT INTO orders (customer, product, quantity, total_amount) VALUES ('dave', 'Widget D', 3, 74.97);"
```

```bash
docker exec cdc-postgres psql -U postgres -d orders -c \
  "UPDATE orders SET status = 'shipped' WHERE customer = 'dave';"
```

```bash
docker exec cdc-postgres psql -U postgres -d orders -c \
  "DELETE FROM orders WHERE customer = 'dave';"
```

Expected CDC output:

```text
[OrderCreated] #4 dave bought 3x Widget D for $74.97
[OrderUpdated] #4 pending -> shipped (dave)
[OrderDeleted] #4 dave
```

The update/delete output depends on `ALTER TABLE orders REPLICA IDENTITY FULL;` in `setup.sql`. Without it, PostgreSQL only sends the primary key for previous row values.

## URLs

- Kafka UI: http://localhost:8080
- Debezium API: http://localhost:8083/connectors

## Cleanup

```bash
docker-compose down -v
```
