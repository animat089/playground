namespace AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.Options;

public sealed class ReconciliationJobOptions
{
    public const string SectionName = "ReconciliationJob";

    public int ScheduleSeconds { get; set; } = 10;

    public bool DemoFailure { get; set; } = false;
}