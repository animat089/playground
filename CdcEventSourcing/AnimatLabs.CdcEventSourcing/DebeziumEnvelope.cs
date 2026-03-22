using System.Text.Json;
using System.Text.Json.Serialization;

namespace AnimatLabs.CdcEventSourcing;

public sealed class DebeziumEnvelope
{
    [JsonPropertyName("before")]
    public JsonElement? Before { get; set; }

    [JsonPropertyName("after")]
    public JsonElement? After { get; set; }

    [JsonPropertyName("op")]
    public string Operation { get; set; } = string.Empty;

    [JsonPropertyName("ts_ms")]
    public long TimestampMs { get; set; }

    public bool IsCreate => Operation == "c" || Operation == "r";
    public bool IsUpdate => Operation == "u";
    public bool IsDelete => Operation == "d";
}

public sealed class DebeziumMessage
{
    [JsonPropertyName("payload")]
    public DebeziumEnvelope? Payload { get; set; }
}
