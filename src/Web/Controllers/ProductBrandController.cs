using Application.Features.ProductBrands.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers
{
   
    public class ProductBrandController : BaseApiController
    {
        [HttpGet("Get All Product Brand")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllProductBrand(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send ( new GetAllProductBrandsQuery() ,  cancellationToken));
        }

        
    }
}
