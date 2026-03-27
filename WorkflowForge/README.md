# WorkflowForge Playground

All WorkflowForge demo projects live in this solution. One `dotnet build` builds everything.

## Solution Layout

```
AnimatLabs.WorkflowForge.sln
├── AnimatLabs.WorkflowForge.Workflows.Sample            (all workflow definitions + abstractions)
├── AnimatLabs.WorkflowForge.CoravelScheduledWorkflows   (Coravel + WF scheduled jobs)
├── AnimatLabs.WorkflowForge.HtmxDashboard               (HTMX SSE + WF real-time dashboard)
└── AnimatLabs.WorkflowForge.MassTransitSaga.OrderService (MassTransit consumer + WF saga)
```

## Build All

```bash
dotnet build AnimatLabs.WorkflowForge.sln
```

## Projects

### Coravel + WorkflowForge (Scheduled Workflows)

Coravel schedules runs; WorkflowForge defines the steps. Reconciliation workflow that rolls back on failure.

```bash
dotnet run --project AnimatLabs.WorkflowForge.CoravelScheduledWorkflows
```

Toggle failure in `appsettings.json` via `ReconciliationJob:DemoFailure`.

**Blog post:** [WorkflowForge + Coravel: Scheduled Workflows](https://animatlabs.com/technical/.net/workflow/workflowforge-coravel-scheduled-workflows/)

### HTMX Dashboard (Real-Time Workflow Visualization)

HTMX SSE extension streams workflow step updates to the browser. No custom JavaScript. Buttons fire `hx-get` to fetch an SSE-connected fragment, then HTMX swaps HTML as each step completes or compensates.

```bash
dotnet run --project AnimatLabs.WorkflowForge.HtmxDashboard
```

Open `http://localhost:5075`. Click "Run Order Workflow" or "Run with Failure."

**Blog post:** [HTMX + WorkflowForge: Live Workflow Dashboard](https://animatlabs.com/technical/.net/workflow/htmx-dotnet/)

### MassTransit Saga (Message-Driven Compensation)

Single-service demo. MassTransit consumer receives `SubmitOrder`, triggers a WorkflowForge saga (ReserveStock, ChargePayment, CreateShipment). Orders over $500 fail at payment, triggering compensation in reverse.

```bash
dotnet run --project AnimatLabs.WorkflowForge.MassTransitSaga.OrderService
```

Uses InMemory transport by default. RabbitMQ optional; see OrderService README.

**Blog post:** [MassTransit Saga + WorkflowForge Compensation](https://animatlabs.com/technical/.net/workflow/masstransit-workflowforge-saga/)

## Requirements

- .NET 8.0 SDK
- No Docker required (all demos run standalone)
