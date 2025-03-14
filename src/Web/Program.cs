using Application;
using Infrastructure;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInstructionServices(builder.Configuration);
builder.AddWebServiceCollection(builder.Configuration);

var app = builder.Build();
await app.AddWebAppServiceAsync().ConfigureAwait(false);
