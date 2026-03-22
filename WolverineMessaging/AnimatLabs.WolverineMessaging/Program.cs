using AnimatLabs.WolverineMessaging.Messages;
using Microsoft.AspNetCore.Builder;
using Wolverine;
using Wolverine.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine();

var app = builder.Build();

app.MapWolverineEndpoints();

app.MapPost("/api/orders", (CreateOrder command, IMessageBus bus) =>
    bus.InvokeAsync<OrderCreated>(command));

app.Run();
