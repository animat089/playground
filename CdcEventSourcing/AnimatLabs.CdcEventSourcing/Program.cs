using System.Text.Json;
using System.Text.Json.Serialization;
using AnimatLabs.CdcEventSourcing;
using AnimatLabs.CdcEventSourcing.DomainEvents;
using Confluent.Kafka;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "cdc-consumer",
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableAutoCommit = true
};

var topic = "orders.public.orders";

using var consumer = new ConsumerBuilder<string, string>(config).Build();
consumer.Subscribe(topic);

Console.WriteLine($"Listening on {topic}. Insert or update rows in the orders table to see events.");
Console.WriteLine("Press Ctrl+C to stop.");
Console.WriteLine();

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

try
{
    while (!cts.Token.IsCancellationRequested)
    {
        var result = consumer.Consume(cts.Token);
        if (result?.Message?.Value is null) continue;

        var message = JsonSerializer.Deserialize<DebeziumMessage>(result.Message.Value);
        var envelope = message?.Payload;
        if (envelope is null) continue;

        if (envelope.IsCreate)
        {
            var row = envelope.After?.Deserialize<OrderRow>();
            if (row is null) continue;

            var created = new OrderCreated(
                row.Id, row.Customer, row.Product,
                row.Quantity, row.TotalAmount, row.Status,
                DateTimeOffset.FromUnixTimeMilliseconds(envelope.TimestampMs));

            Console.WriteLine($"[OrderCreated] #{created.Id} {created.Customer} bought {created.Quantity}x {created.Product} for {created.TotalAmount:C}");
        }
        else if (envelope.IsUpdate)
        {
            var before = envelope.Before?.Deserialize<OrderRow>();
            var after = envelope.After?.Deserialize<OrderRow>();
            if (after is null) continue;

            var updated = new OrderUpdated(
                after.Id, before?.Status, after.Status,
                after.Customer, after.Product, after.TotalAmount,
                DateTimeOffset.FromUnixTimeMilliseconds(envelope.TimestampMs));

            Console.WriteLine($"[OrderUpdated] #{updated.Id} {updated.PreviousStatus} -> {updated.CurrentStatus} ({updated.Customer})");
        }
        else if (envelope.IsDelete)
        {
            var row = envelope.Before?.Deserialize<OrderRow>();
            Console.WriteLine($"[OrderDeleted] #{row?.Id} {row?.Customer}");
        }
    }
}
catch (OperationCanceledException) { }
finally
{
    consumer.Close();
}

[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
internal sealed class OrderRow
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("customer")]
    public string Customer { get; set; } = "";

    [JsonPropertyName("product")]
    public string Product { get; set; } = "";

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("total_amount")]
    public decimal TotalAmount { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = "";
}
