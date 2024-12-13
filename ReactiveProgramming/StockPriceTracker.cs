using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WebSocketSharp;

namespace ReactiveProgramming;

internal class StockPriceTracker
{
    private const string stock = "AAPL";
    private static readonly string apiKey = "ch5e6fpr01qjg0authc0ch5e6fpr01qjg0authcg";

    public static void StartTracking()
    {
        using (var stockPriceSubject = new Subject<double>())
        {
            // Connect to the real-time stock price stream for a specific company
            using (var client = new WebSocket($"wss://ws.finnhub.io/?token={apiKey}"))
            {
                client.OnOpen += (sender, e) => Console.WriteLine("Connected successfully!");
                client.OnMessage += (sender, e) =>
                {
                    if (TryParseJson<StockRecordResponse>(e.Data, out StockRecordResponse? response) && response!=null)
                    {
                        var price = response.StockRecords.Average(record => record.Price);
                        stockPriceSubject.OnNext(price);
                    }
                };

                client.Connect();
                client.Send("{\"type\":\"subscribe\",\"symbol\":\"" + stock + "\"}");

                // Calculate the moving average of the stock price over a 5-second window
                var movingAverage = stockPriceSubject.Window(TimeSpan.FromSeconds(5))
                    .Select(window => window.Average().OnErrorResumeNext(window))
                    .Subscribe(movingPrice => movingPrice
                    .Subscribe(price => Console.WriteLine($"Moving price: {price}")));

                Console.ReadLine();
                movingAverage.Dispose();
            }
        }
    }

    public static bool TryParseJson<T>(string inputStr, out T? result)
    {
        bool success = true;
        var settings = new JsonSerializerSettings
        {
            Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
            MissingMemberHandling = MissingMemberHandling.Error
        };
        result = JsonConvert.DeserializeObject<T>(inputStr, settings);
        return success;
    }
}