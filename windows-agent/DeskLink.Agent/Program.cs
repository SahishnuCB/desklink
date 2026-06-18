var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "DeskLink Agent is running");

app.MapGet("/status", () =>
{
    return Results.Ok(new
    {
        app = "DeskLink.Agent",
        status = "online",
        device = Environment.MachineName,
        time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    });
});

app.Run();