using Microsoft.AspNetCore.DataProtection;
using Sample.Hashed.Services;
using Sample.Hashed.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dataProtecionProvider = DataProtectionProvider.Create("Sample.DataProtection", );
builder.Services.AddSingleton<IDataProtector>(_ => dataProtecionProvider.CreateProtector("Encryption"));
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();