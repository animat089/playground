# Computational Geometry in C#

Spatial queries, polygon operations, nearest-neighbor search, and SVG rendering with Math.NET Spatial and NetTopologySuite.

## Prerequisites

- .NET 10 SDK

No Docker required. All geometry runs in-process with NuGet packages only.

## Run

```bash
cd AnimatLabs.ComputationalGeometry
dotnet run
```

Output lands in the `output/` directory as SVG files you can open in any browser.

## What It Does

1. **Point & Vector math** - 2D/3D transforms, distances, angles via Math.NET Spatial
2. **Polygon operations** - union, intersection, difference, buffering via NetTopologySuite
3. **Spatial indexing** - STRtree nearest-neighbor and range queries
4. **Convex hull** - gift wrapping a random point cloud
5. **SVG export** - every result rendered as a self-contained SVG file

## Project Structure

```
AnimatLabs.ComputationalGeometry/
├── Program.cs              (entry point, runs all demos)
├── Demos/
│   ├── PointVectorDemo.cs  (Math.NET Spatial basics)
│   ├── PolygonOpsDemo.cs   (NTS boolean operations)
│   ├── SpatialIndexDemo.cs (STRtree queries)
│   └── ConvexHullDemo.cs   (hull from random points)
└── Rendering/
    └── SvgRenderer.cs      (geometry to SVG)
```

## Blog Post

[Computational Geometry in C#: Math.NET Spatial + NetTopologySuite](https://animatlabs.com/blog/) (May 2026)
