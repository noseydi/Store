using Application.Features.Products.Queries.GetAll;

using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers
{
    public class ProductsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllProductQuery(), cancellationToken));
        }
        /*
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> Get([FromBody] int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(GetProductQuery(id), cancellationToken ));
        }
        */
    }
}
