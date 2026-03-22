# Wolverine Messaging

In-process messaging with Wolverine: no marker interfaces, method injection, cascading messages.

One handler returns a response and a follow-on message; Wolverine dispatches the second part to its handler. No MediatR-style interfaces or per-handler DI registration.

## Prerequisites

- .NET 8 SDK

## Run

```bash
cd AnimatLabs.WolverineMessaging
dotnet run
```

Create an order:

```bash
curl -X POST http://localhost:5194/api/orders \
  -H "Content-Type: application/json" \
  -d '{"customer":"alice","product":"Widget A","quantity":2,"total":49.98}'
```

The console logs three lines: order created, event handled, warehouse notified, from one HTTP request and cascading messages.

## What This Shows

- Handlers as ordinary static methods
- Cascading messages via tuple returns
- Method injection (`ILogger`, services) on handler parameters
- No explicit handler registration

## Cleanup

No Docker. Stop the app when done.
