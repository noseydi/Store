using Application.Features.ProductTypes.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers
{
    
    public class ProductTypeController : BaseApiController
    {
        [HttpGet("ProductType")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAllProductType(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllProductTypeQuery() ,  cancellationToken));   
        }
    }
}
