using Application.Contracts;
using Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Application.Common.BahavioursPipe
{
    public class CachedQueryBehaviour<TRequest , TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheQuery , IRequest<TResponse>
        
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public CachedQueryBehaviour(IDistributedCache cache , IHttpContextAccessor httpcontextAccessor)
        {
            _cache = cache;
            _httpcontextAccessor = httpcontextAccessor;
            
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;
            var cachedResponse = await _cache.GetAsync(GenerateKey(), cancellationToken);
            if (cachedResponse != null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
            }
            else 
            {
                response = await next();
                var serialized = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));
                await CreateNewCache(request, serialized , cancellationToken);
            }
            return response;
        }

        private async Task<Task> CreateNewCache(TRequest request, byte[] serialized, CancellationToken cancellationToken)
        {
            return _cache.SetAsync(GenerateKey(), serialized,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeToLive(request)
                },
                cancellationToken);
                
        }

        private static TimeSpan TimeToLive(TRequest request)
        {
            return new TimeSpan( 0 ,0,0, request.HoursafeData);
        }

        private string GenerateKey()
        {
            return IDGenerator.GenerateCacheKeyFromRequest(_httpcontextAccessor.HttpContext.Request);    
        }
    }
}
