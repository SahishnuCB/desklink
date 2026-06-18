using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "DeskLink Agent is running");

app.MapGet("/status", () =>
{
    var machineName = Environment.MachineName;
    var userName = Environment.UserName;
    var osVersion = Environment.OSVersion.ToString();

    var totalMemory = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;

    return Results.Ok(new
    {
        app = "DeskLink.Agent",
        status = "online",
        device = machineName,
        user = userName,
        os = osVersion,
        time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        memory = new
        {
            totalAvailableBytes = totalMemory
        }
    });
});

app.Run();