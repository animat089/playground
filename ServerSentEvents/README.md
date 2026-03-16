# Server-Sent Events in .NET

Three SSE patterns in ASP.NET Core Minimal APIs. No frameworks, no SignalR.

## Run

From the `ServerSentEvents` folder:

```bash
cd AnimatLabs.ServerSentEvents
dotnet run
```

Open `http://localhost:5074`. Three sections: a live clock, an order stream with placed/cancelled events, and system metrics updating every 2 seconds.

## Patterns

1. **Live Clock** (`/events/clock`) -- `IAsyncEnumerable`, one event type
2. **Order Stream** (`/events/orders`) -- named events on a single connection
3. **System Metrics** (`/events/metrics`) -- event IDs for reconnection

All three start automatically when the page loads.

## Requirements

- .NET 8.0 SDK
