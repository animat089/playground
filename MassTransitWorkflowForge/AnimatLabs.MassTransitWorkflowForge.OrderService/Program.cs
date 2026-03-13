using AnimatLabs.MassTransitWorkflowForge.Contracts;
using AnimatLabs.MassTransitWorkflowForge.OrderService.Consumers;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddSimpleConsole(o =>
{
    o.SingleLine = true;
    o.TimestampFormat = "HH:mm:ss ";
});

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<OrderSubmittedConsumer>();

    // InMemory transport -- no RabbitMQ needed to run the demo
    cfg.UsingInMemory((context, inmem) =>
    {
        inmem.ConfigureEndpoints(context);
    });

    // To use RabbitMQ instead, uncomment below and comment out UsingInMemory:
    // cfg.UsingRabbitMq((context, rmq) =>
    // {
    //     rmq.Host("localhost", "/", h =>
    //     {
    //         h.Username("guest");
    //         h.Password("guest");
    //     });
    //     rmq.ConfigureEndpoints(context);
    // });
});

var host = builder.Build();

// Fire a test order after the bus starts
_ = Task.Run(async () =>
{
    await Task.Delay(2000);
    var bus = host.Services.GetRequiredService<IBus>();

    Console.WriteLine();
    Console.WriteLine("=== Submitting order (amount $99 -- will SUCCEED) ===");
    Console.WriteLine();

    await bus.Publish(new SubmitOrder(Guid.NewGuid(), "happy@example.com", 99.99m));
    await Task.Delay(5000);

    Console.WriteLine();
    Console.WriteLine("=== Submitting order (amount $999 -- will FAIL, triggering compensation) ===");
    Console.WriteLine();

    await bus.Publish(new SubmitOrder(Guid.NewGuid(), "sad@example.com", 999.99m));
});

await host.RunAsync();
