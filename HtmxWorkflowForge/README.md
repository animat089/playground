# HTMX + WorkflowForge: Real-Time Workflow Dashboard

A live workflow dashboard with **zero JavaScript frameworks**. HTMX handles the UI via Server-Sent Events. WorkflowForge 2.1.1 orchestrates the workflow and runs compensation when steps fail.

## What This Demonstrates

- **HTMX SSE**: Real-time UI updates without React, Vue, or SignalR
- **WorkflowForge orchestration**: Multi-step order processing workflow
- **Automatic compensation**: When a step fails, watch all completed steps roll back in real-time
- **Channel-based streaming**: Go `System.Threading.Channels` pipes workflow events to the SSE endpoint

## The Workflow

```
ValidateOrder → ReserveStock → ChargePayment → CreateShipment → SendNotification
```

Click **"Run with Failure"** to make `ChargePayment` throw. WorkflowForge compensates all completed operations in reverse order -- and you see it happen live on the dashboard.

## Run It

```bash
cd AnimatLabs.HtmxWorkflowForge
dotnet run
```

Open `http://localhost:5000` (or the URL shown in console output).

## Requirements

- .NET 8.0 SDK
- No Docker, no npm, no cloud services
