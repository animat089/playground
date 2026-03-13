namespace AnimatLabs.HtmxWorkflowForge.Services;

public interface IWorkflowEventSink
{
    void Report(string operationName, string status, string detail);
}
