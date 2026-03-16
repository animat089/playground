namespace AnimatLabs.WorkflowForge.Workflows.Sample.OrderProcessing.Services;

public interface IWorkflowEventSink
{
    void Report(string operationName, string status, string detail);
}
