# Hidden .NET Performance APIs

Demonstrates underused .NET APIs: FrozenDictionary/FrozenSet, SearchValues (char and byte), CollectionsMarshal.GetValueRefOrAddDefault, and StringValues.

## Prerequisites

- .NET 8 SDK

No Docker, no external tools.

## Run

```bash
cd AnimatLabs.HiddenPerfApis
dotnet run
```

Expected output:

```text
=== FrozenDictionary / FrozenSet ===

  gzip: id=1
  br: id=2
  zstd: id=3
  deflate: not found
  Contains 'GZIP' (case-insensitive): True
  trusted.Contains('CDN.EXAMPLE.COM'): True
  trusted.Contains('evil.test'): False

=== SearchValues<char> ===

  segment[0]: one
  segment[1]: two
  segment[2]: three
  segment[3]: four
  segment[4]: five

=== SearchValues<byte> ===

  First CR or LF at index: 15
  Header separators via static hoist: True

=== CollectionsMarshal.GetValueRefOrAddDefault ===

  hello: 3
  world: 2
  dotnet: 1

  Tuple-key aggregation:
  Tenant=1, Monday: 150
  Tenant=2, Friday: 200

=== StringValues ===

  Inspect: single = application/json
  Inspect: 3 values = gzip, deflate, br
  Inspect: empty
  FirstOrNull(single): application/json
  FirstOrNull(empty): (null)
  Handle: etag="abc123"
  Handle: accept=text/html,application/json

Done.
```

## What It Demonstrates

1. **FrozenDictionary/FrozenSet** - build-once read-many lookups with optimized layout; FrozenSet whitelist pattern
2. **SearchValues (char)** - precomputed delimiter sets for vectorized `IndexOfAny`
3. **SearchValues (byte)** - same pattern for binary protocols (CR/LF detection)
4. **HttpDelimiters** - static readonly hoist for process-lifetime search sets
5. **CollectionsMarshal.GetValueRefOrAddDefault** - single-probe dictionary accumulation with both simple and tuple keys
6. **StringValues** - zero-allocation one-or-many string wrapper with Inspect, FirstOrNull, and Handle patterns

## Project Structure

```
AnimatLabs.HiddenPerfApis/
  Program.cs          # all API demos
  AnimatLabs.HiddenPerfApis.csproj
```
