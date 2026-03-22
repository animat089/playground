using System.Threading.Channels;
using AnimatLabs.KafkaSignalR.Hubs;
using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;

namespace AnimatLabs.KafkaSignalR.Services;

public sealed class KafkaConsumerService : BackgroundService
{
    private readonly IHubContext<EventHub> _hub;
    private readonly Channel<string> _sseChannel;
    private readonly ILogger<KafkaConsumerService> _log;
    private readonly string _topic;
    private readonly ConsumerConfig _config;

    public KafkaConsumerService(
        IHubContext<EventHub> hub,
        Channel<string> sseChannel,
        IConfiguration configuration,
        ILogger<KafkaConsumerService> log)
    {
        _hub = hub;
        _sseChannel = sseChannel;
        _log = log;
        _topic = configuration.GetValue<string>("Kafka:Topic") ?? "orders.public.orders";
        _config = new ConsumerConfig
        {
            BootstrapServers = configuration.GetValue<string>("Kafka:BootstrapServers") ?? "localhost:9092",
            GroupId = "signalr-bridge",
            AutoOffsetReset = AutoOffsetReset.Latest,
            EnableAutoCommit = true
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var consumer = new ConsumerBuilder<string, string>(_config).Build();
                consumer.Subscribe(_topic);
                _log.LogInformation("Subscribed to {Topic}", _topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(stoppingToken);
                    if (result?.Message?.Value is null) continue;

                    var json = result.Message.Value;
                    _log.LogInformation("Received event from {Topic}", _topic);

                    await _hub.Clients.Group(_topic).SendAsync("ReceiveEvent", json, stoppingToken);
                    await _sseChannel.Writer.WriteAsync(json, stoppingToken);
                }

                consumer.Close();
            }
            catch (OperationCanceledException) { break; }
            catch (ConsumeException ex)
            {
                _log.LogWarning(ex, "Kafka consume failed, retrying in 5s");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
