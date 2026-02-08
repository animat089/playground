# AnimatLabs Source Generators Playground

This solution demonstrates compile-time code generation using Roslyn source generators and the equivalent patterns using T4 templates. It mirrors the blog examples for configuration binding, enum helpers, mapping, and ToString generation.

## Solution Layout

- src/AnimatLabs.SourceGenerators.Attributes
  - Marker attributes that trigger generation.
- src/AnimatLabs.SourceGenerators
  - Incremental generator implementation.
- src/AnimatLabs.SourceGenerators.Demo
  - Console app that uses all four generator scenarios.
- tests/AnimatLabs.SourceGenerators.Tests
  - Tests for generator output + T4 output checks.
- tools/AnimatLabs.SourceGenerators.T4
  - T4 templates and checked-in generated output.

## What Gets Generated (Source Generators)

- AutoToString: `[AutoToString]` creates a ToString override.
- Enum Extensions: `[GenerateEnumExtensions]` creates display helpers + TryParse + GetAll.
- Mapper: `[GenerateMapper]` implements partial mapping methods.
- Configuration: `[GenerateConfiguration]` creates a strongly-typed Bind method and nested binders.

## What Gets Generated (T4)

Templates live under tools/AnimatLabs.SourceGenerators.T4/Templates and the generated outputs are checked in under tools/AnimatLabs.SourceGenerators.T4/Generated.

## Build + Test

```bash
# From SourceGenerators/AnimatLabs.SourceGenerators

dotnet build AnimatLabs.SourceGenerators.sln

dotnet test AnimatLabs.SourceGenerators.sln
```

## Notes

- The demo project enables EmitCompilerGeneratedFiles so you can inspect generated output in obj/.
- T4 output is included as static examples for comparison and does not run during build.
