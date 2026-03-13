# Semantic Kernel + WorkflowForge: AI Agent Pipeline with Rollback

Semantic Kernel reasons. WorkflowForge orchestrates (and rolls back).

## What This Demonstrates

- **AI pipeline orchestration**: 5-step content generation pipeline, each step using an LLM
- **Compensation for AI**: When quality check rejects the output, all steps unwind cleanly
- **100% local**: Uses Ollama -- no OpenAI key, no Azure, no cloud, zero cost
- **WorkflowForge 2.1.1**: Multi-step workflow with automatic compensation

## The Pipeline

```
ClassifyTopic → ResearchTopic → DraftContent → QualityCheck → Publish
                                                    ↓ (rejected)
                                              QualityCheck.Compensate
                                                → DraftContent.Compensate
                                                → ResearchTopic.Compensate
                                                → ClassifyTopic.Compensate
```

## Prerequisites

Start Ollama and pull a model:

```bash
docker run -d -p 11434:11434 --name ollama ollama/ollama
docker exec ollama ollama pull phi3
```

## Run It

Success path (all steps complete, content published):

```bash
cd AnimatLabs.SemanticKernelWorkflowForge
dotnet run
```

Failure path (quality check rejects, compensation runs):

```bash
dotnet run -- --fail
```

Custom endpoint / model:

```bash
dotnet run -- http://localhost:11434 llama3
```

## Requirements

- .NET 8.0 SDK
- Ollama running locally (Docker or native)
- A pulled model (phi3, llama3, mistral, etc.)
