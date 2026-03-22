# Traefik + .NET: No More Port Conflicts

Two .NET services behind Traefik. Both listen on port 8080 inside the container. Traefik routes by hostname.

## Run

```bash
docker-compose up --build
```

First run pulls base images and builds both services; allow a couple of minutes.

## Access

| URL | What |
|-----|------|
| http://api.localhost | API service (JSON endpoints) |
| http://api.localhost/orders | Sample order data |
| http://web.localhost | Web frontend (HTML page) |
| http://localhost:8080 | Traefik dashboard |

Windows: if `api.localhost` and `web.localhost` do not resolve in the browser, use curl or PowerShell with a Host header:

```powershell
Invoke-WebRequest -Uri http://localhost -Headers @{Host="api.localhost"}
```

## How It Works

Both services expose 8080 inside their containers. Traefik listens on port 80 and routes based on the `Host` header. Docker labels configure routing without separate Traefik config files.

To add another service, add another `build:` block with a `Host()` label. Traefik picks it up from the labels.

## Requirements

- Any OCI container runtime with Compose support (Docker, Podman, Rancher Desktop)
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
