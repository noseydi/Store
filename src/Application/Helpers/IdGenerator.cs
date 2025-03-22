using Microsoft.AspNetCore.Http;
using System.Text;

namespace Application.Helpers
{
    public class IDGenerator
    {
        public static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append($"{request.Path}");//save path
            foreach (var (key , value ) in request.Query.OrderBy(x => x.Value))
            {
                KeyBuilder.Append($"|{key}-{value}");
            }
            return KeyBuilder.ToString();
        }
    }
}
