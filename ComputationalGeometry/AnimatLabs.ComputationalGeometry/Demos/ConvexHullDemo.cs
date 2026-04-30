using NetTopologySuite.Geometries;
using AnimatLabs.ComputationalGeometry.Rendering;

namespace AnimatLabs.ComputationalGeometry.Demos;

public static class ConvexHullDemo
{
    public static void Run(string outputDir)
    {
        Console.WriteLine("=== Convex Hull from Random Points ===\n");

        var factory = new GeometryFactory();
        var rng = new Random(123);

        var coords = new Coordinate[50];
        for (int i = 0; i < coords.Length; i++)
        {
            coords[i] = new Coordinate(
                rng.NextDouble() * 300 + 50,
                rng.NextDouble() * 200 + 50);
        }

        var multiPoint = factory.CreateMultiPointFromCoords(coords);
        var hull = multiPoint.ConvexHull();

        Console.WriteLine($"Points: {coords.Length}");
        Console.WriteLine($"Hull vertices: {hull.Coordinates.Length - 1}");
        Console.WriteLine($"Hull area: {hull.Area:F0}");
        Console.WriteLine($"Hull perimeter: {hull.Length:F1}");

        // Centroid
        var centroid = hull.Centroid;
        Console.WriteLine($"Centroid: ({centroid.X:F1}, {centroid.Y:F1})");

        // Render
        var svg = new SvgRenderer();
        svg.AddPolygon(hull, "#3498db", "#2c3e50", 0.2);
        svg.AddPoints(coords, "#e74c3c", 3);
        svg.AddPoints([centroid.Coordinate], "#f39c12", 5);
        svg.AddLabel(centroid.X + 5, centroid.Y - 5, "centroid");

        var path = Path.Combine(outputDir, "04-convex-hull.svg");
        svg.SaveTo(path);
        Console.WriteLine($"\nSVG saved: {path}\n");
    }
}
