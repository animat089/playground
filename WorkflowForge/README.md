# WorkflowForge + Coravel: Scheduled Workflows Sample

This sample demonstrates the pattern:

- Coravel handles **when** to run
- WorkflowForge handles **what** to run (multi-step workflow + compensation)

## What we’re trying to achieve

Show a runnable example of scheduled workflow orchestration where:

- scheduling and infrastructure concerns stay in a small host project
- business logic stays in a separate workflows project
- failures automatically trigger compensation (refunds + inventory release)

## Solution layout

The runnable solution is in this folder:

- `AnimatLabs.WorkflowForge.sln`

Projects:

- `AnimatLabs.WorkflowForge.CoravelScheduledWorkflows` (console host + Coravel scheduler + fake implementations)
- `AnimatLabs.WorkflowForge.Workflows.Sample` (workflow builder + operations + models + service interfaces)

This example is “real-world shaped” because it’s demonstrating the multi-step orchestration **plus** compensation (the saga/rollback story). If your job is truly a single step with no rollback needs, you can (and probably should) keep it much simpler—see **Minimal alternative** below.

## Run

From this folder:

```bash
dotnet run --project AnimatLabs.WorkflowForge.CoravelScheduledWorkflows/AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.csproj
```

## Configure

Edit `appsettings.json`:

- `ReconciliationJob:ScheduleSeconds` controls how often the job runs
- `ReconciliationJob:DemoFailure` toggles a simulated failure so you can see compensation execute

You can also override these from the command line:

```bash

dotnet run --project AnimatLabs.WorkflowForge.CoravelScheduledWorkflows/AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.csproj -- \
	ReconciliationJob:ScheduleSeconds=2 \
	ReconciliationJob:DemoFailure=true
```

Tip (Linux/macOS): if you want to run this briefly, wrap it with `timeout`:

```bash
timeout 12s dotnet run --project AnimatLabs.WorkflowForge.CoravelScheduledWorkflows/AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.csproj -- \
	ReconciliationJob:ScheduleSeconds=2 \
	ReconciliationJob:DemoFailure=false
```

## Why the example has multiple steps

If the goal was only “run something every N seconds,” Coravel alone is enough.

This sample adds steps (payments + inventory + emails) because:

- It shows **state changes** that must be undone if a later step fails.
- It proves the “automatic compensation” value prop in logs (refund + inventory release).
- It matches the common integration-job reality: *fetch → mutate external systems → notify*.

## Compensation rule (important)

WorkflowForge only performs compensation when the workflow reports it supports restore.
In v2, that is computed from the operations: the workflow is restorable only when **all operations** are restorable.

That’s why some “non-mutating” operations in this sample still implement `SupportsRestore = true` with a no-op `RestoreAsync(...)`: it enables compensation for the steps that really need it (payments + inventory).

## Minimal alternative (when you don’t need compensation)

If your scheduled job is a single action (or you’re fine handling failures inline), keep it simple:

- Use Coravel `IInvocable` to run your logic directly.
- Skip WorkflowForge entirely.
