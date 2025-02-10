using Application;
using Infrastructure;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInstructionServices(builder.Configuration);
builder.AddWebServiceCollection();

var app = builder.Build();
app.UseStaticFiles();
await app.AddWebAppServiceAsync().ConfigureAwait(false);
