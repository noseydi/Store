using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Common
{
    [ApiController]
    [Route(template:"api")]
    public class BaseApiController : ControllerBase
    {
        private  ISender _mediator = null!;
        protected ISender Mediator => _mediator   ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
