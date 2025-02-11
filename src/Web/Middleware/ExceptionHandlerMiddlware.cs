using Microsoft.IdentityModel.Tokens;

namespace Web.Middleware
{
    public class MiddlwareExceptionHandler
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILoggerFactory _logger;
        private readonly RequestDelegate _next;

        public MiddlwareExceptionHandler(IWebHostEnvironment env, ILoggerFactory logger, RequestDelegate next)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

    }
}
