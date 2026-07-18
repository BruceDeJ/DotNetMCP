using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    // Prevent JSON cycles from navigation properties (Order -> Items -> Order ...)
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opts.JsonSerializerOptions.MaxDepth = 64;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
string dbPath = System.IO.Path.Join(path, "orders.db");
// Use an absolute path for the SQLite file to avoid different working-directory semantics
var connectionString = $"Data Source={dbPath}";
builder.Services.AddDbContext<OrderContext>(options => options.UseSqlite(connectionString));

var app = builder.Build();

// Ensure database and apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;
    var db = sp.GetRequiredService<OrderContext>();
    var logger = sp.GetRequiredService<ILogger<Program>>();
    try
    {
        
        db.Database.Migrate();
        logger.LogInformation("Database migrations applied.");
    }
    catch (Exception ex)
    {
        // Fall back to EnsureCreated if migrations fail for any reason (helps local/dev scenarios)
        
        try
        {
            db.Database.EnsureCreated();
            logger.LogInformation("Database ensured created.");
        }
        catch (Exception inner)
        {
            logger.LogError(inner, "EnsureCreated also failed.\n");
            throw; // rethrow to make startup failure visible
        }
    }
}


app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
