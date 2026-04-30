using System.Buffers;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Channels;

var samplePath = Path.Combine(AppContext.BaseDirectory, "sample-data.csv");
if (!File.Exists(samplePath))
{
    Console.WriteLine("Generating sample-data.csv...");
    await using var writer = new StreamWriter(samplePath);
    await writer.WriteLineAsync("id,name,amount");
    for (int i = 1; i <= 10_000; i++)
        await writer.WriteLineAsync($"{i},item_{i},{i * 1.5m:F2}");
}

Console.WriteLine("--- Pipeline line counter ---");
int lineCount = await CountLinesWithPipeline(samplePath);
Console.WriteLine($"Lines (via PipeReader): {lineCount}");

Console.WriteLine();
Console.WriteLine("--- Pipeline + Channel producer/consumer ---");
await RunPipelineWithChannel(samplePath);

async Task<int> CountLinesWithPipeline(string path)
{
    await using var stream = File.OpenRead(path);
    var reader = PipeReader.Create(stream);
    int count = 0;

    while (true)
    {
        var result = await reader.ReadAsync();
        var buffer = result.Buffer;

        while (TryReadLine(ref buffer, out _))
            count++;

        reader.AdvanceTo(buffer.Start, buffer.End);

        if (result.IsCompleted) break;
    }

    await reader.CompleteAsync();
    return count;
}

async Task RunPipelineWithChannel(string path)
{
    var channel = Channel.CreateBounded<ParsedRecord>(new BoundedChannelOptions(1000)
    {
        FullMode = BoundedChannelFullMode.Wait,
        SingleWriter = true,
        SingleReader = true
    });

    int produced = 0;
    int consumed = 0;

    var parseTask = Task.Run(async () =>
    {
        await using var stream = File.OpenRead(path);
        var reader = PipeReader.Create(stream);

        while (true)
        {
            var result = await reader.ReadAsync();
            var buffer = result.Buffer;

            while (TryReadLine(ref buffer, out var line))
            {
                var record = ParseRecord(line);
                await channel.Writer.WriteAsync(record);
                produced++;
            }

            reader.AdvanceTo(buffer.Start, buffer.End);
            if (result.IsCompleted) break;
        }

        await reader.CompleteAsync();
        channel.Writer.Complete();
    });

    var processTask = Task.Run(async () =>
    {
        await foreach (var record in channel.Reader.ReadAllAsync())
        {
            await ProcessRecord(record);
            consumed++;
        }
    });

    await Task.WhenAll(parseTask, processTask);
    Console.WriteLine($"Produced: {produced}, Consumed: {consumed}");
}

static ParsedRecord ParseRecord(ReadOnlySequence<byte> line)
{
    var text = Encoding.UTF8.GetString(line);
    var parts = text.Split(',');
    if (parts.Length < 3)
        return new ParsedRecord(0, text, 0m);

    int.TryParse(parts[0], out var id);
    decimal.TryParse(parts[2], out var amount);
    return new ParsedRecord(id, parts[1], amount);
}

static Task ProcessRecord(ParsedRecord record)
{
    // Placeholder for real work: DB write, HTTP call, etc.
    return Task.CompletedTask;
}

static bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
{
    var pos = buffer.PositionOf((byte)'\n');
    if (pos is null) { line = default; return false; }

    line = buffer.Slice(0, pos.Value);
    buffer = buffer.Slice(buffer.GetPosition(1, pos.Value));
    return true;
}

record ParsedRecord(int Id, string Name, decimal Amount);
