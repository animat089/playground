# Production Monitoring for .NET

Health checks, Prometheus metrics, Grafana dashboards, and Serilog structured logging for an ASP.NET Core app.

## What It Does

- Liveness (`/health/live`): process is running
- Readiness (`/health/ready`): PostgreSQL reachable (traffic gate)
- Prometheus (`/metrics`): HTTP metrics plus `app_order_duration_seconds` and `app_orders_total` (status labels)
- Serilog: structured request logging

## Run It

### 1. Start Infrastructure

```bash
docker-compose up -d
```

This starts:

- PostgreSQL on `:5432`
- Prometheus on `:9090`
- Grafana on `:3000` (admin/admin)

Prometheus scrapes `host.docker.internal:5076` so it can reach the app on the host.

### 2. Start the App

```bash
cd AnimatLabs.DotNetMonitoring
dotnet run
```

### 3. Generate Metrics

```bash
# Submit some orders (run a few times)
curl -X POST http://localhost:5076/api/orders
```

### 4. View Dashboards

- Health: http://localhost:5076/health/live and http://localhost:5076/health/ready
- Raw metrics: http://localhost:5076/metrics
- Prometheus: http://localhost:9090 (try `app_orders_total` or `app_order_duration_seconds_bucket`)
- Grafana: http://localhost:3000. Add Prometheus as a data source: `http://prometheus:9090` from containers, or `http://host.docker.internal:9090` from the host

## Without Docker

The app runs without Docker. PostgreSQL shows as unhealthy on readiness (expected); liveness and metrics still work.

## Requirements

- .NET 8.0 SDK
- Any OCI container runtime (Docker, Podman, Rancher Desktop) with Compose support, optional for quick local runs
- Grafana (AGPL v3) is free to use but carries copyleft obligations. For AGPL-sensitive setups, use Prometheus alone at `http://localhost:9090` for queries without Grafana.
