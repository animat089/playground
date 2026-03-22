using Microsoft.AspNetCore.SignalR;

namespace AnimatLabs.KafkaSignalR.Hubs;

public sealed class EventHub : Hub
{
    public async Task JoinTopic(string topic)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, topic);
    }

    public async Task LeaveTopic(string topic)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, topic);
    }
}
