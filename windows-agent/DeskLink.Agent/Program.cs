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
        time = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
        memory = new
        {
            totalAvailableBytes = totalMemory
        }
    });
});

app.MapPost("/commands/lock", () =>
{
    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "rundll32.exe",
            Arguments = "user32.dll,LockWorkStation",
            CreateNoWindow = true,
            UseShellExecute = false
        });

        return Results.Ok(new
        {
            success = true,
            command = "lock",
            message = "Laptop locked successfully"
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            title: "Failed to lock laptop",
            detail: ex.Message
        );
    }
});

app.Run();