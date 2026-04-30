using NetTopologySuite.Geometries;
using NetTopologySuite.Index.Strtree;
using AnimatLabs.ComputationalGeometry.Rendering;

namespace AnimatLabs.ComputationalGeometry.Demos;

public static class SpatialIndexDemo
{
    public static void Run(string outputDir)
    {
        Console.WriteLine("=== Spatial Indexing with STRtree ===\n");

        var factory = new GeometryFactory();
        var rng = new Random(42);

        var points = new List<Point>();
        for (int i = 0; i < 200; i++)
        {
            points.Add(factory.CreatePoint(new Coordinate(
                rng.NextDouble() * 400,
                rng.NextDouble() * 300)));
        }

        var tree = new STRtree<Point>();
        foreach (var pt in points)
            tree.Insert(pt.EnvelopeInternal, pt);
        tree.Build();

        // Range query: find points in a rectangular window
        var queryEnv = new Envelope(100, 200, 80, 180);
        var inRange = tree.Query(queryEnv);
        Console.WriteLine($"Total points: {points.Count}");
        Console.WriteLine($"Points in query window (100-200, 80-180): {inRange.Count}");

        // Nearest neighbor using manual envelope expansion
        var target = new Coordinate(250, 150);
        var nearest = FindNearest(tree, factory, target);
        Console.WriteLine($"Nearest to ({target.X:F0}, {target.Y:F0}): " +
                          $"({nearest.X:F1}, {nearest.Y:F1}), " +
                          $"distance = {target.Distance(nearest.Coordinate):F2}");

        // Render
        var svg = new SvgRenderer();
        svg.AddPoints(points.Select(p => p.Coordinate), "#bdc3c7", 2);
        svg.AddPoints(inRange.Select(p => p.Coordinate), "#e74c3c", 3);

        var queryRect = factory.CreatePolygon([
            new Coordinate(queryEnv.MinX, queryEnv.MinY),
            new Coordinate(queryEnv.MaxX, queryEnv.MinY),
            new Coordinate(queryEnv.MaxX, queryEnv.MaxY),
            new Coordinate(queryEnv.MinX, queryEnv.MaxY),
            new Coordinate(queryEnv.MinX, queryEnv.MinY)
        ]);
        svg.AddPolygon(queryRect, "#e74c3c", "#c0392b", 0.1);

        svg.AddPoints([target], "#2ecc71", 6);
        svg.AddLine(target, nearest.Coordinate, "#2ecc71", 1);
        svg.AddLabel(target.X + 5, target.Y - 5, "query");
        svg.AddLabel(nearest.X + 5, nearest.Y - 5, "nearest");

        var path = Path.Combine(outputDir, "03-spatial-index.svg");
        svg.SaveTo(path);
        Console.WriteLine($"\nSVG saved: {path}\n");
    }

    private static Point FindNearest(STRtree<Point> tree, GeometryFactory factory, Coordinate target)
    {
        double searchRadius = 10;
        var targetPoint = factory.CreatePoint(target);

        while (searchRadius < 1000)
        {
            var env = new Envelope(
                target.X - searchRadius, target.X + searchRadius,
                target.Y - searchRadius, target.Y + searchRadius);

            var candidates = tree.Query(env);
            if (candidates.Count > 0)
            {
                return candidates
                    .OrderBy(p => p.Distance(targetPoint))
                    .First();
            }
            searchRadius *= 2;
        }

        throw new InvalidOperationException("No points found in tree");
    }
}
