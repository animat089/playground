using Spectre.Console;

AnsiConsole.Write(new FigletText("Project Health").Color(Color.Cyan1));
AnsiConsole.MarkupLine("[grey]Spectre.Console Demo — Project Health Checker[/]");
AnsiConsole.WriteLine();

// Simulated project data
var projects = new[]
{
    new ProjectInfo("AnimatLabs.Core", "net8.0", 12, 0, 95.2, "Healthy"),
    new ProjectInfo("AnimatLabs.Api", "net8.0", 8, 2, 87.1, "Warning"),
    new ProjectInfo("AnimatLabs.Data", "net8.0;net48", 15, 0, 91.8, "Healthy"),
    new ProjectInfo("AnimatLabs.Workers", "net8.0", 6, 5, 62.3, "Critical"),
    new ProjectInfo("AnimatLabs.Tests", "net8.0", 0, 0, 100.0, "Healthy"),
};

// Progress bar
AnsiConsole.Status()
    .Spinner(Spinner.Known.Dots)
    .SpinnerStyle(Style.Parse("cyan"))
    .Start("Analyzing solution...", ctx =>
    {
        foreach (var project in projects)
        {
            ctx.Status($"Scanning [bold]{project.Name}[/]...");
            Thread.Sleep(600);
        }
    });

AnsiConsole.WriteLine();

// Project table
var table = new Table()
    .Border(TableBorder.Rounded)
    .Title("[bold cyan]Solution Health Report[/]")
    .AddColumn(new TableColumn("[bold]Project[/]").LeftAligned())
    .AddColumn(new TableColumn("[bold]Target[/]").Centered())
    .AddColumn(new TableColumn("[bold]Deps[/]").Centered())
    .AddColumn(new TableColumn("[bold]Vulns[/]").Centered())
    .AddColumn(new TableColumn("[bold]Coverage[/]").Centered())
    .AddColumn(new TableColumn("[bold]Status[/]").Centered());

foreach (var p in projects)
{
    var statusMarkup = p.Status switch
    {
        "Healthy" => "[green]Healthy[/]",
        "Warning" => "[yellow]Warning[/]",
        "Critical" => "[red]Critical[/]",
        _ => p.Status
    };

    var vulnMarkup = p.Vulnerabilities > 0
        ? $"[red]{p.Vulnerabilities}[/]"
        : $"[green]{p.Vulnerabilities}[/]";

    var coverageColor = p.Coverage >= 90 ? "green" : p.Coverage >= 70 ? "yellow" : "red";

    table.AddRow(
        $"[bold]{p.Name}[/]",
        p.TargetFramework,
        p.Dependencies.ToString(),
        vulnMarkup,
        $"[{coverageColor}]{p.Coverage:F1}%[/]",
        statusMarkup);
}

AnsiConsole.Write(table);
AnsiConsole.WriteLine();

// Dependency tree
var tree = new Tree("[bold cyan]Dependency Tree[/]")
    .Style(Style.Parse("dim"));

var coreNode = tree.AddNode("[bold]AnimatLabs.Core[/] [grey](0 vulnerabilities)[/]");
coreNode.AddNode("Microsoft.Extensions.Logging [green]8.0.0[/]");
coreNode.AddNode("System.Text.Json [green]8.0.0[/]");

var apiNode = tree.AddNode("[bold]AnimatLabs.Api[/] [yellow](2 vulnerabilities)[/]");
apiNode.AddNode("[bold]AnimatLabs.Core[/]");
var aspNode = apiNode.AddNode("Microsoft.AspNetCore.Mvc [green]8.0.0[/]");
aspNode.AddNode("[red]Newtonsoft.Json 12.0.3[/] [red bold]CVE-2024-XXXX[/]");
aspNode.AddNode("[red]System.Data.SqlClient 4.8.5[/] [red bold]CVE-2024-YYYY[/]");

var dataNode = tree.AddNode("[bold]AnimatLabs.Data[/] [grey](0 vulnerabilities)[/]");
dataNode.AddNode("[bold]AnimatLabs.Core[/]");
dataNode.AddNode("Microsoft.EntityFrameworkCore [green]8.0.0[/]");
dataNode.AddNode("Npgsql [green]8.0.0[/]");

var workersNode = tree.AddNode("[bold]AnimatLabs.Workers[/] [red](5 vulnerabilities)[/]");
workersNode.AddNode("[bold]AnimatLabs.Core[/]");
workersNode.AddNode("[bold]AnimatLabs.Data[/]");
var massTransitNode = workersNode.AddNode("MassTransit [yellow]7.3.1[/] [dim](outdated)[/]");
massTransitNode.AddNode("[red]RabbitMQ.Client 6.2.1[/] [red bold]3 CVEs[/]");

AnsiConsole.Write(tree);
AnsiConsole.WriteLine();

// Summary with bar chart
var chart = new BarChart()
    .Label("[bold cyan]Test Coverage by Project[/]")
    .CenterLabel()
    .Width(60);

foreach (var p in projects)
{
    var color = p.Coverage >= 90 ? Color.Green : p.Coverage >= 70 ? Color.Yellow : Color.Red;
    chart.AddItem(p.Name.Replace("AnimatLabs.", ""), p.Coverage, color);
}

AnsiConsole.Write(chart);
AnsiConsole.WriteLine();

// Final rule
var healthy = projects.Count(p => p.Status == "Healthy");
var total = projects.Length;
var overallColor = healthy == total ? "green" : healthy >= total - 1 ? "yellow" : "red";
AnsiConsole.Write(new Rule($"[{overallColor} bold]{healthy}/{total} projects healthy[/]").RuleStyle(overallColor));

record ProjectInfo(string Name, string TargetFramework, int Dependencies, int Vulnerabilities, double Coverage, string Status);
