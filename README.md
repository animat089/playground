# Playground - AnimatLabs Code Samples

Code samples and demos for [animatlabs.com](https://animatlabs.com) blog posts.

All tools and dependencies in this repo are free and open-source. No paid licenses or vendor lock-in. Container images (PostgreSQL, Kafka, Debezium, Prometheus, Grafana, RabbitMQ, Traefik) are OSS. The `docker-compose` commands work with Docker, Podman, Rancher Desktop, or any OCI-compatible runtime. Kubernetes is not required.

## Projects

### WorkflowForge (Solution, 4 projects)

All WorkflowForge demos live under `AnimatLabs.WorkflowForge.sln`:

- Coravel Scheduled Workflows: scheduling, multi-step orchestration, compensation
- HTMX Dashboard: workflow UI with HTMX SSE, no custom JavaScript
- MassTransit Saga: consumer triggers saga with compensation
- Workflows.Sample: shared workflow operations

`dotnet build AnimatLabs.WorkflowForge.sln` builds everything.

See [WorkflowForge/README.md](WorkflowForge/README.md).

### ServerSentEvents

Three SSE patterns in ASP.NET Core Minimal APIs:

- Live Clock: `IAsyncEnumerable` SSE
- Order Stream: named events (`placed` / `cancelled`) on one connection
- System Metrics: event IDs for reconnection, JSON payloads

See [ServerSentEvents/README.md](ServerSentEvents/README.md).

### DotNetMonitoring

Health checks, Prometheus, Grafana:

- Liveness and readiness endpoints
- Custom metrics (order duration histogram, success/failure counters)
- PostgreSQL health check
- Docker optional (Postgres, Prometheus, Grafana)

See [DotNetMonitoring/README.md](DotNetMonitoring/README.md).

### TraefikDotNet

Traefik reverse proxy with multiple .NET services:

- Traefik v3 routing by hostname
- Two sample .NET services
- docker-compose with Traefik dashboard

See [TraefikDotNet/README.md](TraefikDotNet/README.md).

### CdcEventSourcing

Database changes as typed domain events (Debezium, Kafka, .NET):

- PostgreSQL WAL to Debezium to Kafka
- .NET consumer maps to OrderCreated, OrderUpdated
- Containers: PostgreSQL, Kafka (official Apache image, KRaft), Debezium Connect, Kafka UI

See [CdcEventSourcing/README.md](CdcEventSourcing/README.md).

### KafkaSignalR

Kafka to browser via SignalR and SSE:

- BackgroundService bridges Kafka to SignalR and an SSE channel
- HTML page compares both feeds
- Works with CdcEventSourcing or standalone docker-compose

See [KafkaSignalR/README.md](KafkaSignalR/README.md).

### SpectreConsole

Spectre.Console CLI demos:

- Tables, trees, bar charts, spinners, FigletText
- Project health checker sample

See [SpectreConsole/README.md](SpectreConsole/README.md).

### OutboxPattern

Transactional outbox with EF Core and PostgreSQL:

- Order and outbox rows in one transaction
- Outbox table ready for Debezium CDC
- Pairs with CdcEventSourcing for a full pipeline

See [OutboxPattern/README.md](OutboxPattern/README.md).

### MartenEventStore

PostgreSQL document + event store with Marten:

- Event sourcing with typed domain events
- Inline projections for read models
- Event stream endpoint for audit

See [MartenEventStore/README.md](MartenEventStore/README.md).

### WolverineMessaging

Wolverine in-process messaging:

- Handlers as static methods, method injection
- Cascading messages without MediatR-style interfaces
- Single library for many handler scenarios

See [WolverineMessaging/README.md](WolverineMessaging/README.md).

### FastEndpoints

Endpoint-per-file APIs with validation:

- REPR-style organization
- FluentValidation
- Minimal API performance with structured endpoints

See [FastEndpoints/README.md](FastEndpoints/README.md).

### AllSoapBasedApis

SOAP: ASMX, WCF, SoapCore, CoreWCF.

### AspectOrientedProgramming

AOP with PostSharp: logging, security, caching.

### Benchmarking

BenchmarkDotNet: iteration patterns, mapping approaches.

See [Benchmarking/readme.md](Benchmarking/readme.md).

### HashId.NET

Web API with HashidsNet for ID obfuscation.

### Microsoft.AspNetCore.DataProtection

Data Protection API for encrypting user data.

### ReactiveProgramming

Rx.NET: sorting, file watching, stock/temperature samples.

### Refit.ApiSdk

Refit: typed HTTP client, contracts, consumer, tests.

### SourceGenerators

Custom source generators.

See [SourceGenerators/README.md](SourceGenerators/README.md).

## License

Licensed under the Apache License 2.0. See [LICENSE](LICENSE).
