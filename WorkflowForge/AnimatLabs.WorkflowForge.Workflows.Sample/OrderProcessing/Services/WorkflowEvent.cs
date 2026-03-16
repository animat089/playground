namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Services;

public sealed class WorkflowEvent
{
    public required string OperationName { get; init; }
    public required string Status { get; init; }
    public required string Detail { get; init; }
}
