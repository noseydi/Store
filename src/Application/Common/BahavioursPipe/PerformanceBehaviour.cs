using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Common.BahavioursPipe
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<IRequest> _logger;
        private readonly Stopwatch _timer; 
        public PerformanceBehaviour(ILogger<IRequest> logger) 
        {
            _logger = logger;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Performance (3. for command) (4. for query)");
            _timer.Start();
            var response = await next();
            _timer.Stop();
            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (elapsedMilliseconds <= 500) return response;
            var requestname = typeof(TRequest).Name;
            _logger.LogWarning("CleanArchitecture Long Running Request:{Name}({elapsedMilliseconds} milliseconds) {@UserId}",
                requestname, elapsedMilliseconds, request);
            return response;
        }

    }
}
