# Parsing Building Models in C# with xBIM

Open IFC files, walk building hierarchies, extract geometry data, and render floor plan outlines. No AutoCAD or Revit required.

## Prerequisites

- .NET 8 SDK (xBIM targets .NET Standard 2.0 / .NET Framework, runs on .NET 8)

No Docker required.

## Run

```bash
cd AnimatLabs.IFC.xBIM
dotnet run
```

Parses the included sample IFC file and prints the building hierarchy to the console.

## What It Does

1. **Open IFC** - load IFC2x3 and IFC4 files via xBIM MemoryModel
2. **Walk hierarchy** - IfcProject -> IfcSite -> IfcBuilding -> IfcBuildingStorey -> IfcSpace
3. **Extract properties** - area, volume, material assignments
4. **Spatial data** - bounding boxes and placement coordinates
5. **Console output** - structured tree of the building model

## Blog Post

[Parsing Building Models in C#: IFC Files with xBIM](https://animatlabs.com/blog/) (June 2026)
