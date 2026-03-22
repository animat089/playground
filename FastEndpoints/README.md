# FastEndpoints

Endpoint-per-file API structure using FastEndpoints. Minimal API performance with validation and clear file layout.

## Prerequisites

- .NET 8 SDK

## Run

```bash
cd AnimatLabs.FastEndpoints
dotnet run
```

Create a user:

```bash
curl -X POST http://localhost:5196/api/users \
  -H "Content-Type: application/json" \
  -d '{"name":"alice","email":"alice@example.com"}'
```

List users:

```bash
curl http://localhost:5196/api/users
```

Invalid input triggers validation:

```bash
curl -X POST http://localhost:5196/api/users \
  -H "Content-Type: application/json" \
  -d '{"name":"","email":"not-an-email"}'
```

## What This Shows

- REPR pattern: one file per endpoint (Request, Endpoint, Response)
- FluentValidation built in
- No controller layer or large `Program.cs` maps
- Same execution model as Minimal APIs

## Cleanup

No Docker. Stop the app when done.
