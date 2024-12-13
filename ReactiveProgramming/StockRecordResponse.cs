using Newtonsoft.Json;

namespace ReactiveProgramming
{
    [JsonObject]
    public class StockRecordResponse
    {
        [JsonProperty("data")]
        public IEnumerable<StockRecord> StockRecords { get; set; }
    }

    [JsonObject]
    public class StockRecord
    {
        [JsonProperty("p")]
        public double Price { get; set; }

        [JsonProperty("s")]
        public string Stock { get; set; }
    }
}
