using LMS.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register Oracle DbContext
builder.Services.AddDbContext<LMSDbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("OracleDb")
    ));

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
