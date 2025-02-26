using Domain.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Exception = System.Exception;
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
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var result = JsonSerializer.Serialize(new ApiToReturn(500, ex.Message), options);

               // context.Response.ContentType = "application/json";
              //  context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = HandleResult(context, ex, result, options);
              await context.Response.WriteAsync(result);

            }
        }

        private string HandleResult(HttpContext context, Exception exception, string result, JsonSerializerOptions options)
        {
            switch (exception)
            {
                case NotFoundException notFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new ApiToReturn(404, notFoundException.Message , notFoundException.Messages , exception.Message));
                        break;
                case BadRequestException badRequestException :
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new ApiToReturn(400, badRequestException.Message, badRequestException.Messages, exception.Message));
                    break;
                case ValidationEntityException  validationEntityException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new ApiToReturn(400, validationEntityException.Message, validationEntityException.Messages, exception.Message));
                    break;
            }
            return result;
        }
    }
}
