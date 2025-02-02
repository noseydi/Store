using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Common
{
    [ApiController]
    [Route(template:"api/controller")]
    public class BaseApiController : Controller
    {
        private  ISender _mediator = null;
        protected ISender Mediator => _mediator   ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
