using Application.Dtos.Products;
using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web
{
    public class ProductsController : BaseApiController
    {
        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] GetAllProductQuery request ,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request , cancellationToken));
        }
        
        [HttpGet("product/{id:int}")]
        public async Task<ActionResult<ProductDto>> Get([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetProductQuery(id), cancellationToken ));
        }
        
    }
}
