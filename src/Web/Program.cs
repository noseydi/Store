using Infrastructure;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInstructionServices(builder.Configuration);
builder.AddWebServiceCollection();

var app = builder.Build();
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var logerfactory = services.GetRequiredService<ILoggerFactory>();
var context = services.GetRequiredService<ApplicationDbContext>();

try
{
    await context.Database.MigrateAsync();
    await GenerateFakeData.SeedDataAsync(context, logerfactory);

}
catch (Exception e)
{
    var logger = logerfactory.CreateLogger<Program>();
    logger.LogError(e, "error exception for migration");
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
