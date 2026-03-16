# Playground - AnimatLabs Code Samples

Code samples and demos for [animatlabs.com](https://animatlabs.com) blog posts.

## Projects

### WorkflowForge (Solution -- 4 projects)

All WorkflowForge demos consolidated under one solution: `AnimatLabs.WorkflowForge.sln`

- **Coravel Scheduled Workflows** -- scheduling + multi-step orchestration + compensation
- **HTMX Dashboard** -- real-time workflow visualization with HTMX SSE, zero custom JavaScript
- **MassTransit Saga** -- message-driven consumer triggers WF saga with automatic rollback
- **Workflows.Sample** -- shared workflow operations library

`dotnet build AnimatLabs.WorkflowForge.sln` builds everything.

- See: [WorkflowForge/README.md](WorkflowForge/README.md)

### ServerSentEvents

Three SSE patterns in pure ASP.NET Core Minimal APIs:

- **Live Clock** -- simplest SSE with `IAsyncEnumerable`
- **Order Stream** -- named events (`placed` / `cancelled`) on one connection
- **System Metrics** -- event IDs for auto-reconnection, JSON payloads

- See: [ServerSentEvents/README.md](ServerSentEvents/README.md)

### DotNetMonitoring

Production monitoring setup with health checks, Prometheus, and Grafana:

- Liveness and readiness health check endpoints
- Custom Prometheus metrics (order duration histogram, success/failure counters)
- PostgreSQL health check integration
- Runs against `docker-compose.azure-local.yml`

- See: [DotNetMonitoring/README.md](DotNetMonitoring/README.md)

### TraefikDotNet

Docker + Traefik reverse proxy with multiple .NET services:

- Traefik v3 routing by hostname (no port conflicts)
- Two sample .NET services behind Traefik
- docker-compose with Traefik dashboard

- See: [TraefikDotNet/README.md](TraefikDotNet/README.md)

### SpectreConsole

Beautiful CLI output with Spectre.Console:

- Tables, trees, bar charts, progress spinners, FigletText
- "Project Health Checker" demo showcasing all major Spectre features

- See: [SpectreConsole/README.md](SpectreConsole/README.md)

### AllSoapBasedApis

SOAP-based web service implementations: ASMX, WCF, SoapCore, CoreWCF.

### AspectOrientedProgramming

AOP with PostSharp: logging, security, caching aspects.

### Benchmarking

BenchmarkDotNet: iteration patterns, data mapping approaches.

- See: [Benchmarking/readme.md](Benchmarking/readme.md)

### HashId.NET

ASP.NET Core Web API with HashidsNet for ID obfuscation.

### Microsoft.AspNetCore.DataProtection

Data Protection API for encrypting/decrypting user information.

### ReactiveProgramming

Rx.NET patterns: sorting, file watching, stock/temperature tracking.

### Refit.ApiSdk

API SDK with Refit: typed HTTP client, contracts, consumer, tests.

### SourceGenerators

Custom source generators: attributes, compile-time code gen.

- See: [SourceGenerators/README.md](SourceGenerators/README.md)

## License

Licensed under the Apache License 2.0 - see the [LICENSE](LICENSE) file for details.
