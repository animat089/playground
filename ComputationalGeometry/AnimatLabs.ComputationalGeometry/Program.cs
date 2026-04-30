using AnimatLabs.ComputationalGeometry.Demos;

var outputDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "output");
Directory.CreateDirectory(outputDir);

Console.WriteLine("Computational Geometry in C#");
Console.WriteLine("Math.NET Spatial + NetTopologySuite\n");
Console.WriteLine(new string('-', 50));

PointVectorDemo.Run(outputDir);
PolygonOpsDemo.Run(outputDir);
SpatialIndexDemo.Run(outputDir);
ConvexHullDemo.Run(outputDir);

Console.WriteLine(new string('-', 50));
Console.WriteLine($"\nAll SVG files are in: {Path.GetFullPath(outputDir)}");
Console.WriteLine("Open them in any browser to view the visualizations.");
