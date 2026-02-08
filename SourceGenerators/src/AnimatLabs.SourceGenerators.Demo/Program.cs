using System;
using AnimatLabs.SourceGenerators.Demo.Mappers;
using AnimatLabs.SourceGenerators.Demo.Models;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder().Build();

var settings = DatabaseSettings.Bind(configuration);
Console.WriteLine($"Settings: {settings.ConnectionString}, Timeout={settings.Timeout}, Retry={settings.Retry.MaxAttempts}");

var mapper = new UserMapper();
var user = new User
{
    Id = 101,
    FirstName = "Ada",
    LastName = "Lovelace",
    Email = "ada@animatlabs.com"
};

var dto = mapper.ToDto(user);
Console.WriteLine($"DTO: {dto.FirstName} {dto.LastName} ({dto.Email})");

var person = new Person { FirstName = "Linus", LastName = "Torvalds", Age = 54 };
Console.WriteLine(person);

var status = OrderStatus.Processing;
Console.WriteLine($"Status: {status.ToDisplayName()}");

if (OrderStatusExtensions.TryParse("Pending Approval", out var parsed))
{
    Console.WriteLine($"Parsed: {parsed}");
}
