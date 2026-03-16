var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Content("""
    <!DOCTYPE html>
    <html>
    <head><title>Web Service</title></head>
    <body>
        <h1>Web Service</h1>
        <p>This is the web frontend, served via <code>web.localhost</code>.</p>
        <p>The API lives at <a href="http://api.localhost">api.localhost</a>.</p>
    </body>
    </html>
    """, "text/html"));

app.MapGet("/health", () => Results.Ok("healthy"));

app.Run();
