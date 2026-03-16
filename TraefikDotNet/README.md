# Traefik + .NET: No More Port Conflicts

Two .NET services behind Traefik. Both run on port 8080 internally. Traefik routes by hostname.

## Run

```bash
docker-compose up --build
```

First run pulls base images and builds both services -- takes a couple of minutes.

## Access

| URL | What |
|-----|------|
| http://api.localhost | API service (JSON endpoints) |
| http://api.localhost/orders | Sample order data |
| http://web.localhost | Web frontend (HTML page) |
| http://localhost:8080 | Traefik dashboard |

> **Windows note:** If `api.localhost` and `web.localhost` don't resolve in your browser, use curl or PowerShell with a Host header instead:
> ```powershell
> Invoke-WebRequest -Uri http://localhost -Headers @{Host="api.localhost"}
> ```

## How It Works

Both services expose 8080 inside their containers. Traefik listens on port 80 and routes based on the `Host` header. Docker labels configure the routing -- no config files.

Add a third service? Add another `build:` block with a `Host()` label. Traefik discovers it from the labels.

## Requirements

- Docker Desktop (with WSL2 backend on Windows)
- No .NET SDK needed (services build inside Docker)

## Project Structure

```
TraefikDotNet/
  docker-compose.yml       # Traefik v3.6 + 2 services
  ApiService/
    Program.cs             # Minimal API with /orders endpoint
    Dockerfile             # Multi-stage .NET 9 build
  WebService/
    Program.cs             # HTML frontend
    Dockerfile             # Multi-stage .NET 9 build
```
