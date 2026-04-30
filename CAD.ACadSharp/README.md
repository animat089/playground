# Reading AutoCAD Files in .NET with ACadSharp

Parse DWG and DXF files without AutoCAD installed. Extract layers, entities, blocks, and dimension data.

## Prerequisites

- .NET 8 SDK (ACadSharp targets .NET Standard 2.0)

No Docker required.

## Run

```bash
cd AnimatLabs.CAD.ACadSharp
dotnet run
```

Reads the included sample DXF file, prints layers and entities to the console.

## What It Does

1. **Read DWG/DXF** - open binary DWG and text DXF without AutoCAD
2. **List layers** - enumerate layers with colors and line types
3. **Extract entities** - lines, circles, arcs, polylines, text, dimensions
4. **Block references** - resolve INSERT entities to their block definitions
5. **Console output** - structured listing of the drawing contents

## Blog Post

[Reading AutoCAD Files in .NET: DWG and DXF with ACadSharp](https://animatlabs.com/blog/) (July 2026)
