using DeskLink.Agent.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SystemCommandService>();

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

app.MapPost("/commands/lock", (SystemCommandService systemCommands) =>
{
    var success = systemCommands.LockWorkstation();

    if (!success)
    {
        return Results.Problem(
            title: "Failed to lock laptop",
            detail: "DeskLink could not execute the lock command."
        );
    }

    return Results.Ok(new
    {
        success = true,
        command = "lock",
        message = "Laptop locked successfully"
    });
});

app.Run();