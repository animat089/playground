# System.IO.Pipelines + Channels

Demonstrates low-allocation file parsing with `PipeReader` and producer-consumer coordination with `Channel<T>`.

## Prerequisites

- .NET 8 SDK

No Docker, no external tools.

## Run

```bash
cd AnimatLabs.IoPipelines
dotnet run
```

On first run it generates a 10,000-line CSV file as sample data. Expected output:

```text
Generating sample-data.csv...
--- Pipeline line counter ---
Lines (via PipeReader): 10001
--- Pipeline + Channel producer/consumer ---
Produced: 10001, Consumed: 10001
```

## What It Demonstrates

1. **PipeReader line counting** - scans for `\n` delimiters without allocating strings, returns buffers to the pool via `AdvanceTo`
2. **Pipeline + Channel** - PipeReader parses lines into `ParsedRecord` values, pushes them into a bounded channel (capacity 1000), a second task consumes and processes records with backpressure

## Project Structure

```
AnimatLabs.IoPipelines/
  Program.cs          # pipeline counter + channel producer/consumer
  AnimatLabs.IoPipelines.csproj
```
