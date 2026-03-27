# Spectre.Console: Project Health Checker

Tables, trees, charts, and spinners in the terminal using Spectre.Console.

## What It Does

- **Tables:** a health report with colored status per project
- Dependency **trees** show the graph with vulnerability badges
- Bar charts for test coverage percentages
- A **spinner** runs while scanning
- FigletText renders the ASCII art header
- Rules add a summary divider with pass/fail count

## Run

From the `SpectreConsole` folder:

```bash
cd AnimatLabs.SpectreConsole
dotnet run
```

You'll see a Figlet header, a spinner, a health report table, a dependency tree, coverage bars, and a "3/5 projects healthy" summary rule.

## Requirements

- .NET 8.0 SDK
- ANSI color support (default in most terminals)
