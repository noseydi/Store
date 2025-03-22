using Domain.Exceptions;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.SeedData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using Web.Middleware;

namespace Web
{
    public  static class ConfigureService
    {
        public static IServiceCollection AddWebServiceCollection (this WebApplicationBuilder builder  , IConfiguration configuration)
        {

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.Configure<ApiBehaviorOptions>( options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count> 0 )
                .SelectMany(v => v.Value.Errors)
                .Select(c => c.ErrorMessage).ToArray( );
                    return new BadRequestObjectResult(new ApiToReturn(400 , errors.ToList()));
                };
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy => policy.WithOrigins("http://localhost:4200").
                AllowAnyMethod().AllowAnyHeader()
              );
            });
          

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDistributedMemoryCache ();
            return builder.Services;
        }
        public static async Task<IApplicationBuilder> AddWebAppServiceAsync(this WebApplication app)
        {
            app.UseMiddleware<MiddlwareExceptionHandler>();
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
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
            return app;
        }
    }
}
