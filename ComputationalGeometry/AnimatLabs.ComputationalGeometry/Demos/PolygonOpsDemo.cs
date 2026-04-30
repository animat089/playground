using NetTopologySuite.Geometries;
using AnimatLabs.ComputationalGeometry.Rendering;

namespace AnimatLabs.ComputationalGeometry.Demos;

public static class PolygonOpsDemo
{
    public static void Run(string outputDir)
    {
        Console.WriteLine("=== Polygon Boolean Operations (NetTopologySuite) ===\n");

        var factory = new GeometryFactory();

        var squareCoords = new[]
        {
            new Coordinate(0, 0), new Coordinate(100, 0),
            new Coordinate(100, 100), new Coordinate(0, 100),
            new Coordinate(0, 0)
        };
        var square = factory.CreatePolygon(squareCoords);

        var circleCoords = CreateCircle(70, 70, 60, 64);
        var circle = factory.CreatePolygon(circleCoords);

        Console.WriteLine($"Square area: {square.Area:F0}");
        Console.WriteLine($"Circle area: {circle.Area:F0}");

        var intersection = square.Intersection(circle);
        Console.WriteLine($"Intersection area: {intersection.Area:F0}");

        var union = square.Union(circle);
        Console.WriteLine($"Union area: {union.Area:F0}");

        var difference = square.Difference(circle);
        Console.WriteLine($"Square minus circle: {difference.Area:F0}");

        var symDiff = square.SymmetricDifference(circle);
        Console.WriteLine($"Symmetric difference: {symDiff.Area:F0}");

        // Buffering
        var line = factory.CreateLineString([new Coordinate(20, 150), new Coordinate(180, 150)]);
        var buffered = line.Buffer(15);
        Console.WriteLine($"\nLine buffered by 15: area = {buffered.Area:F0}");

        // Render each operation as a separate SVG
        RenderOperation(outputDir, "02a-intersection.svg", square, circle, intersection, "#e74c3c");
        RenderOperation(outputDir, "02b-union.svg", square, circle, union, "#27ae60");
        RenderOperation(outputDir, "02c-difference.svg", square, circle, difference, "#f39c12");
        RenderOperation(outputDir, "02d-symmetric-diff.svg", square, circle, symDiff, "#9b59b6");

        var bufferSvg = new SvgRenderer();
        bufferSvg.AddPolygon(buffered, "#3498db", "#2c3e50", 0.3);
        bufferSvg.AddLineString(line, "#e74c3c", 2);
        bufferSvg.SaveTo(Path.Combine(outputDir, "02e-buffer.svg"));

        Console.WriteLine($"\nSVGs saved to {outputDir}\n");
    }

    private static void RenderOperation(
        string outputDir, string filename,
        Geometry a, Geometry b, Geometry result, string resultColor)
    {
        var svg = new SvgRenderer();
        svg.AddPolygon(a, "#bdc3c7", "#7f8c8d", 0.2);
        svg.AddPolygon(b, "#bdc3c7", "#7f8c8d", 0.2);
        svg.AddPolygon(result, resultColor, "#2c3e50", 0.5);
        svg.SaveTo(Path.Combine(outputDir, filename));
    }

    private static Coordinate[] CreateCircle(double cx, double cy, double radius, int segments)
    {
        var coords = new Coordinate[segments + 1];
        for (int i = 0; i < segments; i++)
        {
            double angle = 2 * Math.PI * i / segments;
            coords[i] = new Coordinate(
                cx + radius * Math.Cos(angle),
                cy + radius * Math.Sin(angle));
        }
        coords[segments] = coords[0];
        return coords;
    }
}
