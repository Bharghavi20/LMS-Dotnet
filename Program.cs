using LMS.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Register Oracle DbContext
builder.Services.AddDbContext<LMSDbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("OracleDb")
    ));

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
