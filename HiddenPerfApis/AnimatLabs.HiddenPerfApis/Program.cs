using System.Buffers;
using System.Collections.Frozen;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Primitives;

Console.WriteLine("=== FrozenDictionary / FrozenSet ===");
Console.WriteLine();

var raw = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
{
    ["gzip"] = 1,
    ["br"] = 2,
    ["zstd"] = 3,
};

FrozenDictionary<string, int> encodings = raw.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

foreach (var name in (ReadOnlySpan<string>)["gzip", "br", "zstd", "deflate"])
{
    bool found = encodings.TryGetValue(name, out int id);
    Console.WriteLine($"  {name}: {(found ? $"id={id}" : "not found")}");
}

FrozenSet<string> allowedHosts = raw.Keys.ToFrozenSet(StringComparer.OrdinalIgnoreCase);
Console.WriteLine($"  Contains 'GZIP' (case-insensitive): {allowedHosts.Contains("GZIP")}");

string[] whitelist = ["api.example.com", "cdn.example.com"];
FrozenSet<string> trusted = whitelist.ToFrozenSet(StringComparer.OrdinalIgnoreCase);
Console.WriteLine($"  trusted.Contains('CDN.EXAMPLE.COM'): {trusted.Contains("CDN.EXAMPLE.COM")}");
Console.WriteLine($"  trusted.Contains('evil.test'): {trusted.Contains("evil.test")}");

Console.WriteLine();
Console.WriteLine("=== SearchValues<char> ===");
Console.WriteLine();

SearchValues<char> separators = SearchValues.Create([',', ';', '\t', '|']);

ReadOnlySpan<char> line = "one,two;three\tfour|five";
int start = 0;
int segmentIndex = 0;
while (start < line.Length)
{
    int next = line[start..].IndexOfAny(separators);
    if (next < 0)
    {
        Console.WriteLine($"  segment[{segmentIndex}]: {line[start..]}");
        break;
    }

    int absolute = start + next;
    Console.WriteLine($"  segment[{segmentIndex}]: {line[start..absolute]}");
    segmentIndex++;
    start = absolute + 1;
}

Console.WriteLine();
Console.WriteLine("=== SearchValues<byte> ===");
Console.WriteLine();

SearchValues<byte> crlf = SearchValues.Create("\r\n"u8);
ReadOnlySpan<byte> buf = "HTTP/1.1 200 OK\r\nContent-Length: 42\r\n\r\n"u8;
int idx = buf.IndexOfAny(crlf);
Console.WriteLine($"  First CR or LF at index: {idx}");
Console.WriteLine($"  Header separators via static hoist: {HttpDelimiters.HeaderSeparators.Contains(':')}");

Console.WriteLine();
Console.WriteLine("=== CollectionsMarshal.GetValueRefOrAddDefault ===");
Console.WriteLine();

var counts = new Dictionary<string, int>();
string[] words = ["hello", "world", "hello", "dotnet", "hello", "world"];

foreach (var word in words)
{
    ref int slot = ref CollectionsMarshal.GetValueRefOrAddDefault(counts, word, out bool exists);
    slot = exists ? slot + 1 : 1;
}

foreach (var kvp in counts)
    Console.WriteLine($"  {kvp.Key}: {kvp.Value}");

Console.WriteLine();
Console.WriteLine("  Tuple-key aggregation:");
var aggregates = new Dictionary<(int Tenant, DayOfWeek Dow), decimal>();

AddSale(1, DayOfWeek.Monday, 100m);
AddSale(1, DayOfWeek.Monday, 50m);
AddSale(2, DayOfWeek.Friday, 200m);

foreach (var kvp in aggregates)
    Console.WriteLine($"  Tenant={kvp.Key.Tenant}, {kvp.Key.Dow}: {kvp.Value}");

Console.WriteLine();
Console.WriteLine("=== StringValues ===");
Console.WriteLine();

StringValues single = new("application/json");
StringValues multi = new(new[] { "gzip", "deflate", "br" });
StringValues empty = StringValues.Empty;

Inspect(single);
Inspect(multi);
Inspect(empty);

Console.WriteLine($"  FirstOrNull(single): {FirstOrNull(single)}");
Console.WriteLine($"  FirstOrNull(empty): {FirstOrNull(empty) ?? "(null)"}");

Handle(new StringValues(new[] { "text/html", "application/json" }), new StringValues("\"abc123\""));

Console.WriteLine();
Console.WriteLine("Done.");

void AddSale(int tenant, DayOfWeek dow, decimal amount)
{
    var key = (tenant, dow);
    ref decimal total = ref CollectionsMarshal.GetValueRefOrAddDefault(aggregates, key, out bool existed);
    total = existed ? total + amount : amount;
}

static void Inspect(StringValues sv)
{
    if (sv.Count == 0)
    {
        Console.WriteLine("  Inspect: empty");
        return;
    }

    if (sv.Count == 1)
    {
        Console.WriteLine($"  Inspect: single = {sv[0]}");
        return;
    }

    Console.WriteLine($"  Inspect: {sv.Count} values = {string.Join(", ", sv.ToArray())}");
}

static string? FirstOrNull(StringValues sv) => sv.Count == 0 ? null : sv[0];

static void Handle(StringValues accept, StringValues etag)
{
    if (etag.Count != 0)
        Console.WriteLine($"  Handle: etag={etag[0]}");

    if (accept.Count == 0)
        return;

    string joined = string.Join(',', accept.ToArray());
    Console.WriteLine($"  Handle: accept={joined}");
}

static class HttpDelimiters
{
    public static readonly SearchValues<char> HeaderSeparators =
        SearchValues.Create([',', ':']);
}
