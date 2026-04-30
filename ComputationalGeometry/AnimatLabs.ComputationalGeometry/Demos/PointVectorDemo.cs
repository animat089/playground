using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using AnimatLabs.ComputationalGeometry.Rendering;
using NetTopologySuite.Geometries;

namespace AnimatLabs.ComputationalGeometry.Demos;

public static class PointVectorDemo
{
    public static void Run(string outputDir)
    {
        Console.WriteLine("=== Point & Vector Math (Math.NET Spatial) ===\n");

        var origin = new Point2D(0, 0);
        var p1 = new Point2D(3, 4);
        var p2 = new Point2D(-2, 7);

        double dist = origin.DistanceTo(p1);
        Console.WriteLine($"Distance from origin to ({p1.X}, {p1.Y}): {dist:F2}");

        var v1 = new Vector2D(3, 4);
        var v2 = new Vector2D(-2, 7);

        double dotProduct = v1.DotProduct(v2);
        Console.WriteLine($"Dot product of v1 and v2: {dotProduct:F2}");

        double crossProduct = v1.CrossProduct(v2);
        Console.WriteLine($"Cross product (2D, scalar): {crossProduct:F2}");

        var angle = v1.SignedAngleTo(v2, clockWise: false);
        Console.WriteLine($"Angle from v1 to v2: {angle.Degrees:F1} degrees");

        var normalized = v1.Normalize();
        Console.WriteLine($"v1 normalized: ({normalized.X:F3}, {normalized.Y:F3})");

        var rotated = new Point2D(
            p1.X * Math.Cos(Math.PI / 4) - p1.Y * Math.Sin(Math.PI / 4),
            p1.X * Math.Sin(Math.PI / 4) + p1.Y * Math.Cos(Math.PI / 4));
        Console.WriteLine($"p1 rotated 45 degrees: ({rotated.X:F2}, {rotated.Y:F2})");

        var midpoint = new Point2D((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        Console.WriteLine($"Midpoint of p1 and p2: ({midpoint.X:F2}, {midpoint.Y:F2})");

        // 3D example
        var p3d1 = new Point3D(1, 2, 3);
        var p3d2 = new Point3D(4, 5, 6);
        var dist3d = p3d1.DistanceTo(p3d2);
        Console.WriteLine($"\n3D distance: {dist3d:F2}");

        var v3d1 = new Vector3D(1, 0, 0);
        var v3d2 = new Vector3D(0, 1, 0);
        var cross3d = v3d1.CrossProduct(v3d2);
        Console.WriteLine($"3D cross product: ({cross3d.X}, {cross3d.Y}, {cross3d.Z})");

        // Render SVG
        var svg = new SvgRenderer();
        var scale = 20.0;
        var allPoints = new[] { origin, p1, p2, midpoint, rotated };
        svg.AddPoints(allPoints.Select(p => new Coordinate(p.X * scale, -p.Y * scale)), "#e74c3c", 4);
        svg.AddLine(
            new Coordinate(origin.X * scale, -origin.Y * scale),
            new Coordinate(p1.X * scale, -p1.Y * scale), "#3498db", 2);
        svg.AddLine(
            new Coordinate(origin.X * scale, -origin.Y * scale),
            new Coordinate(p2.X * scale, -p2.Y * scale), "#2ecc71", 2);
        svg.AddLine(
            new Coordinate(p1.X * scale, -p1.Y * scale),
            new Coordinate(p2.X * scale, -p2.Y * scale), "#95a5a6", 1);
        svg.AddLabel(p1.X * scale + 5, -p1.Y * scale, "p1 (3,4)");
        svg.AddLabel(p2.X * scale + 5, -p2.Y * scale, "p2 (-2,7)");
        svg.AddLabel(midpoint.X * scale + 5, -midpoint.Y * scale, "mid");
        svg.AddLabel(rotated.X * scale + 5, -rotated.Y * scale, "rot45");

        var path = Path.Combine(outputDir, "01-points-vectors.svg");
        svg.SaveTo(path);
        Console.WriteLine($"\nSVG saved: {path}\n");
    }
}
