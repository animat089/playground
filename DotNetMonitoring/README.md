# Production Monitoring for .NET

Health checks, Prometheus metrics, Grafana dashboards, and Serilog structured logging for an ASP.NET Core app. Pairs with the `docker-compose.azure-local.yml` stack from the blog repo.

## What It Does

- **Liveness probe** (`/health/live`) -- is the process alive?
- **Readiness probe** (`/health/ready`) -- can it handle traffic? (checks PostgreSQL)
- **Prometheus metrics** (`/metrics`) -- request duration, order processing histogram, success/failure counters
- **Custom business metrics** -- `app_order_duration_seconds` histogram, `app_orders_total` counter with status labels
- **Serilog** structured request logging

## Run It

### 1. Start Infrastructure

Using the blog's `docker-compose.azure-local.yml`:

```bash
cd animatlabs.github.io
docker-compose -f docker-compose.azure-local.yml up postgres prometheus grafana -d
```

This starts:
- PostgreSQL on `:5432`
- Prometheus on `:9090`
- Grafana on `:3000` (admin/admin)

**Note:** Copy `Config/prometheus.yml` to the location expected by `docker-compose.azure-local.yml`, or mount it directly.

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

- **Health checks:** http://localhost:5076/health/live and http://localhost:5076/health/ready
- **Raw metrics:** http://localhost:5076/metrics
- **Prometheus:** http://localhost:9090 -- query `app_orders_total` or `app_order_duration_seconds_bucket`
- **Grafana:** http://localhost:3000 -- add Prometheus as a data source (http://prometheus:9090)

## Without Docker

The app runs fine without Docker. Health checks will report "Unhealthy" for PostgreSQL (expected), but liveness and metrics still work. Good for quick local testing.

## Requirements

- .NET 8.0 SDK
- Docker (for PostgreSQL, Prometheus, Grafana) -- optional for basic health check testing
