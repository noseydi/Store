using Application;
using Infrastructure;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.SeedData;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Web;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInstructionServices(builder.Configuration);
builder.AddWebServiceCollection(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<MiddlwareExceptionHandler>();
app.UseStaticFiles();
await app.AddWebAppServiceAsync().ConfigureAwait(false);
