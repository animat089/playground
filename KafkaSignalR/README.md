# Kafka to Browser: SignalR + SSE

Streams Kafka events to the browser using SignalR and Server-Sent Events side by side.

A `BackgroundService` consumes from a Kafka topic, pushes each message to a SignalR hub group and to an SSE channel. The browser page shows both feeds so you can compare them.

Pairs with the [CdcEventSourcing](../CdcEventSourcing/) project. If you run CDC there, the same Kafka topic feeds events into this app.

## Prerequisites

- Any OCI container runtime (Docker, Podman, Rancher Desktop) with Compose support
- .NET 8 SDK

## Run

### Kafka from the CDC project

If the CdcEventSourcing stack is already running, skip docker-compose here and start the app:

```bash
cd AnimatLabs.KafkaSignalR
dotnet run
```

### Standalone Kafka

```bash
docker-compose up -d
```

Wait about 15 seconds, then create the topic:

```bash
docker exec stream-kafka /opt/kafka/bin/kafka-topics.sh --bootstrap-server localhost:29092 --create --topic orders.public.orders --partitions 1 --replication-factor 1
```

Start the app:

```bash
cd AnimatLabs.KafkaSignalR
dotnet run
```

Open http://localhost:5180 in your browser.

Produce test messages:

```bash
docker exec -it stream-kafka /opt/kafka/bin/kafka-console-producer.sh --broker-list localhost:29092 --topic orders.public.orders
```

Type JSON lines and press Enter. They appear in both the SignalR and SSE columns.

## URLs

- App: http://localhost:5180
- SignalR hub: ws://localhost:5180/hub/events
- SSE: http://localhost:5180/sse/events

## Cleanup

```bash
docker-compose down -v
```
