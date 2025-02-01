using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace Web
{
    public  static class ConfigureService
    {
        public static IServiceCollection AddWebServiceCollection (this WebApplicationBuilder builder )
        {

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            return builder.Services;
        }
        public static async Task<IApplicationBuilder> AddWebAppServiceAsync(this WebApplication app)
        {
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

            await app.RunAsync();
            return app;
        }
    }
}
