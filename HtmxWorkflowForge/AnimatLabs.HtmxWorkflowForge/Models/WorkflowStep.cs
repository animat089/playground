namespace AnimatLabs.HtmxWorkflowForge.Models;

public sealed class WorkflowStep
{
    public required string Name { get; init; }
    public required string Status { get; init; }
    public string? Detail { get; init; }
    public string CssClass => Status switch
    {
        "running" => "step-running",
        "completed" => "step-completed",
        "failed" => "step-failed",
        "compensating" => "step-compensating",
        "compensated" => "step-compensated",
        _ => "step-pending"
    };
}

public sealed class WorkflowRun
{
    public string Id { get; } = Guid.NewGuid().ToString("N")[..8];
    public List<WorkflowStep> Steps { get; } = [];
    public string OverallStatus { get; set; } = "pending";
    public bool IsComplete { get; set; }
}
