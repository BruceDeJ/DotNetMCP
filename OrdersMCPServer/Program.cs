using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrdersMCPServer.OrderSystem.Client;
using Serilog;

// Configure Serilog early so startup logs are captured. Use AppContext.BaseDirectory for a deterministic log folder
var logDir = Path.Combine(AppContext.BaseDirectory, "logs");
if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
var logPath = Path.Combine(logDir, "ordersmcp-.log");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14)
    .CreateLogger();

// Emit a short startup message on stderr so MCP inspector shows where logs are written
Console.Error.WriteLine($"Serilog writing to: {logPath}");

//When creating the ApplicationHostBuilder, ensure you use CreateEmptyApplicationBuilder instead of CreateDefaultBuilder.
//This ensures that the server does not write any additional messages to the console.
//This is only necessary for servers using STDIO transport.
var builder = Host.CreateEmptyApplicationBuilder(settings: null);

// Avoid the below in STDIO servers, because the MCP protocol uses stdout for data and stderr for logs.
// This can cause issues if the console output is redirected or captured by other tools.
// Instead, we log to a file and use stderr for Serilog logs, while keeping stdout clean for MCP communication.
//Console.WriteLine();

// Route console/system logs through Serilog (but keep console to stderr for MCP protocol separation)
builder.Logging.AddSerilog();
builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services.AddHttpClient<OrderServiceClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:52796/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Add the MCP services: the transport to use (stdio) and the tools to register.
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<OrderServiceAPITools>();

try
{
    var app = builder.Build();
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
