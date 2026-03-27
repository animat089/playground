# HTMX + WorkflowForge Dashboard

Workflow dashboard with no custom JavaScript. HTMX SSE extension handles every UI update.

## Run

From the WorkflowForge solution root:

```bash
dotnet run --project AnimatLabs.WorkflowForge.HtmxDashboard
```

Open `http://localhost:5075`.

Two buttons: "Run Order Workflow" runs 5 steps to completion. "Run with Failure" fails at ChargePayment and triggers compensation in reverse.

## How It Works

1. Button fires `hx-get="/workflow/reset"`; returns HTML with `sse-connect`
2. HTMX opens an SSE connection to `/workflow/stream`
3. Server sends `event: step` with HTML fragments, HTMX swaps them in
4. `event: done` swaps the final status

No `<script>` blocks. Pure HTMX attributes.

## Requirements

- .NET 8.0 SDK

**Documentation:** [animatlabs.com/workflow-forge](https://animatlabs.com/workflow-forge)
