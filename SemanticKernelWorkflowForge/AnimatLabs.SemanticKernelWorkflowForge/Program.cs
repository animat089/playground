using AnimatLabs.SemanticKernelWorkflowForge.Workflows;
using Microsoft.SemanticKernel;
using WorkflowForge.Loggers;
using WF = WorkflowForge.WorkflowForge;

// Ollama must be running: docker run -d -p 11434:11434 ollama/ollama
// Then pull a model: docker exec <container> ollama pull phi3

var ollamaEndpoint = args.Length > 0 ? args[0] : "http://localhost:11434";
var modelId = args.Length > 1 ? args[1] : "phi3";
var shouldFail = args.Contains("--fail");

Console.WriteLine("=== Semantic Kernel + WorkflowForge: AI Content Pipeline ===");
Console.WriteLine($"Endpoint: {ollamaEndpoint}");
Console.WriteLine($"Model: {modelId}");
Console.WriteLine($"Force quality failure: {shouldFail}");
Console.WriteLine();

#pragma warning disable SKEXP0070
var kernelBuilder = Kernel.CreateBuilder()
    .AddOllamaChatCompletion(modelId, new Uri(ollamaEndpoint));
#pragma warning restore SKEXP0070

var kernel = kernelBuilder.Build();

var topic = "How .NET developers can use AI agents locally with Ollama";

var workflow = ContentPipelineWorkflow.Build(kernel, shouldFail);

using var foundry = WF.CreateFoundry(
    workflowName: workflow.Name,
    initialProperties: new Dictionary<string, object?>
    {
        [PipelineKeys.Topic] = topic
    });

using var smith = WF.CreateSmith(new ConsoleLogger("WF-AI"));

try
{
    Console.WriteLine($"Starting pipeline for topic: \"{topic}\"");
    Console.WriteLine(new string('-', 60));

    await smith.ForgeAsync(workflow, foundry).ConfigureAwait(false);

    Console.WriteLine(new string('-', 60));
    Console.WriteLine("Pipeline completed successfully!");
    Console.WriteLine();

    var draft = foundry.Properties.TryGetValue(PipelineKeys.Draft, out var d) ? d?.ToString() : null;
    var outputPath = foundry.Properties.TryGetValue(PipelineKeys.OutputPath, out var p) ? p?.ToString() : null;

    if (draft is not null)
    {
        Console.WriteLine("=== Generated Content ===");
        Console.WriteLine(draft);
        Console.WriteLine();
    }

    if (outputPath is not null)
    {
        Console.WriteLine($"Published to: {outputPath}");
    }
}
catch (Exception ex)
{
    Console.WriteLine(new string('-', 60));
    Console.WriteLine($"Pipeline FAILED: {ex.Message}");
    Console.WriteLine("Compensation has been executed -- all intermediate artifacts cleaned up.");
    Console.WriteLine("Content flagged for human review.");
}
