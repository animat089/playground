using Core;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add SoapCore
builder.Services.AddSoapCore();
builder.Services.AddSingleton<ISampleServiceExtended, SampleService>();

// Add CoreWCF
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// Register SoapCore
((IApplicationBuilder)app).UseSoapEndpoint<ISampleServiceExtended>("/SampleSoapCoreService", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);

// Register CoreWcf
app.UseServiceModel(builder =>
{
    builder.AddService<SampleService>(serviceOptions => { })
    // Add BasicHttpBinding endpoint
    .AddServiceEndpoint<SampleService, ISampleServiceExtended>(new BasicHttpBinding(CoreWCF.Channels.BasicHttpSecurityMode.Transport), "/SampleCoreWcfService");

    var mb = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    mb.HttpsGetEnabled = true;
});

app.Run();