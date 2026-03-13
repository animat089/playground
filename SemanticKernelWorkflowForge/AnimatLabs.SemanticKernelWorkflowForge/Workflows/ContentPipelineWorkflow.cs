using Microsoft.SemanticKernel;
using WorkflowForge.Abstractions;
using WorkflowForge.Extensions;
using WorkflowForge.Operations;
using WF = WorkflowForge.WorkflowForge;

namespace AnimatLabs.SemanticKernelWorkflowForge.Workflows;

public static class PipelineKeys
{
    public const string Topic = "Topic";
    public const string Classification = "Classification";
    public const string Research = "Research";
    public const string Draft = "Draft";
    public const string QualityScore = "QualityScore";
    public const string OutputPath = "OutputPath";
    public const string ShouldFailQuality = "ShouldFailQuality";
}

public static class ContentPipelineWorkflow
{
    public static IWorkflow Build(Kernel kernel, bool failQualityCheck = false)
    {
        return WF
            .CreateWorkflow("AIContentPipeline")
            .AddOperation(new ClassifyTopicStep(kernel))
            .AddOperation(new ResearchTopicStep(kernel))
            .AddOperation(new DraftContentStep(kernel))
            .AddOperation(new QualityCheckStep(kernel, failQualityCheck))
            .AddOperation(new PublishStep())
            .Build();
    }
}

public sealed class ClassifyTopicStep(Kernel kernel) : WorkflowOperationBase
{
    public override string Name => "ClassifyTopic";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var topic = foundry.GetPropertyOrDefault<string>(PipelineKeys.Topic) ?? "general";
        foundry.Logger.LogInformation("[ClassifyTopic] Classifying: {Topic}", topic);

        var result = await kernel.InvokePromptAsync(
            $"Classify this topic into one category (technology, science, business, lifestyle). Topic: {topic}. Reply with just the category name.",
            cancellationToken: ct).ConfigureAwait(false);

        var classification = result?.ToString()?.Trim() ?? "technology";
        foundry.SetProperty(PipelineKeys.Classification, classification);
        foundry.Logger.LogInformation("[ClassifyTopic] Classified as: {Classification}", classification);

        return inputData;
    }

    public override Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        foundry.Logger.LogWarning("[ClassifyTopic] COMPENSATING: Clearing classification");
        foundry.Properties.TryRemove(PipelineKeys.Classification, out _);
        return Task.CompletedTask;
    }
}

public sealed class ResearchTopicStep(Kernel kernel) : WorkflowOperationBase
{
    public override string Name => "ResearchTopic";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var topic = foundry.GetPropertyOrDefault<string>(PipelineKeys.Topic) ?? "general";
        var classification = foundry.GetPropertyOrDefault<string>(PipelineKeys.Classification) ?? "general";
        foundry.Logger.LogInformation("[ResearchTopic] Researching {Topic} ({Classification})", topic, classification);

        var result = await kernel.InvokePromptAsync(
            $"Provide 3 key facts about '{topic}' in the context of {classification}. Keep it under 100 words. Use bullet points.",
            cancellationToken: ct).ConfigureAwait(false);

        var research = result?.ToString() ?? "No research found.";
        foundry.SetProperty(PipelineKeys.Research, research);
        foundry.Logger.LogInformation("[ResearchTopic] Research complete ({Length} chars)", research.Length);

        return inputData;
    }

    public override Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        foundry.Logger.LogWarning("[ResearchTopic] COMPENSATING: Discarding research notes");
        foundry.Properties.TryRemove(PipelineKeys.Research, out _);
        return Task.CompletedTask;
    }
}

public sealed class DraftContentStep(Kernel kernel) : WorkflowOperationBase
{
    public override string Name => "DraftContent";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var topic = foundry.GetPropertyOrDefault<string>(PipelineKeys.Topic) ?? "general";
        var research = foundry.GetPropertyOrDefault<string>(PipelineKeys.Research) ?? "";
        foundry.Logger.LogInformation("[DraftContent] Drafting content for: {Topic}", topic);

        var result = await kernel.InvokePromptAsync(
            $"Write a short 150-word blog paragraph about '{topic}' using these facts:\n{research}\nKeep it engaging and concise.",
            cancellationToken: ct).ConfigureAwait(false);

        var draft = result?.ToString() ?? "Draft unavailable.";
        foundry.SetProperty(PipelineKeys.Draft, draft);
        foundry.Logger.LogInformation("[DraftContent] Draft complete ({Length} chars)", draft.Length);

        return inputData;
    }

    public override Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        foundry.Logger.LogWarning("[DraftContent] COMPENSATING: Deleting draft, flagging for human review");
        foundry.Properties.TryRemove(PipelineKeys.Draft, out _);
        return Task.CompletedTask;
    }
}

public sealed class QualityCheckStep(Kernel kernel, bool forceFailure) : WorkflowOperationBase
{
    public override string Name => "QualityCheck";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var draft = foundry.GetPropertyOrDefault<string>(PipelineKeys.Draft) ?? "";
        foundry.Logger.LogInformation("[QualityCheck] Reviewing draft quality...");

        if (forceFailure)
        {
            foundry.SetProperty(PipelineKeys.QualityScore, 2);
            foundry.Logger.LogError("[QualityCheck] Quality score: 2/10 -- REJECTED. Triggering compensation.");
            throw new InvalidOperationException("Draft quality too low (2/10). Needs human review.");
        }

        var result = await kernel.InvokePromptAsync(
            $"Rate this text quality from 1-10 (10 being best). Reply with ONLY a number.\n\n{draft}",
            cancellationToken: ct).ConfigureAwait(false);

        var scoreText = result?.ToString()?.Trim() ?? "7";
        _ = int.TryParse(scoreText.AsSpan(0, Math.Min(2, scoreText.Length)), out var score);
        if (score < 1) score = 7;

        foundry.SetProperty(PipelineKeys.QualityScore, score);
        foundry.Logger.LogInformation("[QualityCheck] Quality score: {Score}/10", score);

        if (score < 5)
        {
            throw new InvalidOperationException($"Draft quality too low ({score}/10). Needs human review.");
        }

        return inputData;
    }

    public override Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        foundry.Logger.LogWarning("[QualityCheck] COMPENSATING: Logging rejection for audit");
        return Task.CompletedTask;
    }
}

public sealed class PublishStep : WorkflowOperationBase
{
    public override string Name => "Publish";

    protected override async Task<object?> ForgeAsyncCore(
        object? inputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var draft = foundry.GetPropertyOrDefault<string>(PipelineKeys.Draft) ?? "";
        var score = foundry.GetPropertyOrDefault<int>(PipelineKeys.QualityScore);

        var outputPath = Path.Combine(Path.GetTempPath(), $"content-{Guid.NewGuid():N}.md");
        await File.WriteAllTextAsync(outputPath, draft, ct).ConfigureAwait(false);

        foundry.SetProperty(PipelineKeys.OutputPath, outputPath);
        foundry.Logger.LogInformation("[Publish] Content published to {Path} (quality: {Score}/10)", outputPath, score);

        return inputData;
    }

    public override Task RestoreAsync(
        object? outputData, IWorkflowFoundry foundry, CancellationToken ct)
    {
        var path = foundry.GetPropertyOrDefault<string>(PipelineKeys.OutputPath);
        if (!string.IsNullOrEmpty(path) && File.Exists(path))
        {
            File.Delete(path);
            foundry.Logger.LogWarning("[Publish] COMPENSATING: Published file deleted: {Path}", path);
        }
        return Task.CompletedTask;
    }
}
