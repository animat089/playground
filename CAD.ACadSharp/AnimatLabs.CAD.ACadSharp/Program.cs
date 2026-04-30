using ACadSharp;
using ACadSharp.IO;
using ACadSharp.Entities;

Console.WriteLine("AutoCAD File Reader");
Console.WriteLine("ACadSharp for .NET\n");
Console.WriteLine(new string('-', 50));

// To run with a real DWG/DXF file, pass the path as an argument:
//   dotnet run -- path/to/drawing.dxf
// Sample DXF files: any CAD software can export DXF, or find samples online

var filePath = args.Length > 0 ? args[0] : null;

if (filePath is null || !File.Exists(filePath))
{
    Console.WriteLine("No DWG/DXF file provided.\n");
    Console.WriteLine("ACadSharp reads DWG (R13-R2018) and DXF (ASCII/binary) files.");
    Console.WriteLine("No AutoCAD installation required.\n");
    Console.WriteLine("Usage:");
    Console.WriteLine("  dotnet run -- path/to/drawing.dwg");
    Console.WriteLine("  dotnet run -- path/to/drawing.dxf");
    Console.WriteLine("\nSupported entity types:");
    Console.WriteLine("  Lines, Circles, Arcs, Polylines, LWPolylines");
    Console.WriteLine("  Text, MText, Dimensions, Hatches");
    Console.WriteLine("  Block references (INSERT), Ellipses, Splines");
    return;
}

CadDocument doc;
var ext = Path.GetExtension(filePath).ToLowerInvariant();

if (ext == ".dwg")
{
    using var reader = new DwgReader(filePath);
    doc = reader.Read();
}
else if (ext == ".dxf")
{
    using var reader = new DxfReader(filePath);
    doc = reader.Read();
}
else
{
    Console.WriteLine($"Unsupported file type: {ext}");
    return;
}

Console.WriteLine($"File: {Path.GetFileName(filePath)}");
Console.WriteLine($"Version: {doc.Header.Version}\n");

// Layers
Console.WriteLine("Layers:");
foreach (var layer in doc.Layers)
{
    Console.WriteLine($"  {layer.Name,-30} Color: {layer.Color}");
}

// Entities by type
var entityGroups = doc.Entities
    .GroupBy(e => e.GetType().Name)
    .OrderByDescending(g => g.Count());

Console.WriteLine($"\nEntities ({doc.Entities.Count()} total):");
foreach (var group in entityGroups)
{
    Console.WriteLine($"  {group.Key,-25} {group.Count(),5}");
}

// Block definitions
var blockCount = doc.BlockRecords.Count();
if (blockCount > 0)
{
    Console.WriteLine($"\nBlock definitions: {blockCount}");
    foreach (var block in doc.BlockRecords.Take(10))
    {
        Console.WriteLine($"  {block.Name}");
    }
    if (blockCount > 10)
        Console.WriteLine($"  ... and {blockCount - 10} more");
}
