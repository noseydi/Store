using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace Web
{
    public  static class ConfigureService
    {
        public static IServiceCollection AddWebServiceCollection (this WebApplicationBuilder builer )
        {
            builer.Services.AddDbContext<ApplicationDbContext>( option =>
            {
                option.UseSqlServer(builer.Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            return builer.Services ;
        }
    }
}
