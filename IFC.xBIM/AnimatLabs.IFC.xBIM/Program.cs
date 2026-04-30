using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

Console.WriteLine("IFC Building Model Parser");
Console.WriteLine("xBIM Toolkit for .NET\n");
Console.WriteLine(new string('-', 50));

// To run with a real IFC file, pass the path as an argument:
//   dotnet run -- path/to/building.ifc
// Sample IFC files available at: https://www.ifcwiki.org/index.php/KIT_IFC_Examples

var ifcPath = args.Length > 0 ? args[0] : null;

if (ifcPath is null || !File.Exists(ifcPath))
{
    Console.WriteLine("No IFC file provided. Generating an in-memory sample.\n");
    DemoWithoutFile();
}
else
{
    ParseIfcFile(ifcPath);
}

static void DemoWithoutFile()
{
    Console.WriteLine("xBIM can parse IFC2x3, IFC4, and IFC4x3 files.");
    Console.WriteLine("Typical hierarchy:\n");
    Console.WriteLine("  IfcProject");
    Console.WriteLine("    └─ IfcSite");
    Console.WriteLine("        └─ IfcBuilding");
    Console.WriteLine("            ├─ IfcBuildingStorey (Ground Floor)");
    Console.WriteLine("            │   ├─ IfcWall");
    Console.WriteLine("            │   ├─ IfcDoor");
    Console.WriteLine("            │   └─ IfcSpace (Room A)");
    Console.WriteLine("            └─ IfcBuildingStorey (First Floor)");
    Console.WriteLine("                ├─ IfcWall");
    Console.WriteLine("                └─ IfcSpace (Room B)\n");
    Console.WriteLine("Provide an IFC file to see real data:");
    Console.WriteLine("  dotnet run -- path/to/building.ifc");
}

static void ParseIfcFile(string path)
{
    Console.WriteLine($"Opening: {path}\n");

    using var model = IfcStore.Open(path);
    var project = model.Instances.FirstOrDefault<IIfcProject>();

    if (project is null)
    {
        Console.WriteLine("No IfcProject found in file.");
        return;
    }

    Console.WriteLine($"Project: {project.Name}");
    Console.WriteLine($"Schema:  {model.SchemaVersion}");

    var sites = model.Instances.OfType<IIfcSite>();
    foreach (var site in sites)
    {
        Console.WriteLine($"\n  Site: {site.Name}");

        var buildings = model.Instances.OfType<IIfcBuilding>();
        foreach (var building in buildings)
        {
            Console.WriteLine($"    Building: {building.Name}");

            var storeys = model.Instances.OfType<IIfcBuildingStorey>()
                .OrderBy(s => s.Elevation?.Value ?? 0);

            foreach (var storey in storeys)
            {
                Console.WriteLine($"      Storey: {storey.Name} (elevation: {storey.Elevation?.Value:F1}m)");

                var spaces = model.Instances.OfType<IIfcSpace>();
                int spaceCount = spaces.Count();
                if (spaceCount > 0)
                    Console.WriteLine($"        Spaces: {spaceCount}");
            }
        }
    }

    var walls = model.Instances.OfType<IIfcWall>().Count();
    var doors = model.Instances.OfType<IIfcDoor>().Count();
    var windows = model.Instances.OfType<IIfcWindow>().Count();

    Console.WriteLine($"\n  Summary:");
    Console.WriteLine($"    Walls:   {walls}");
    Console.WriteLine($"    Doors:   {doors}");
    Console.WriteLine($"    Windows: {windows}");
    Console.WriteLine($"    Total entities: {model.Instances.Count}");
}
