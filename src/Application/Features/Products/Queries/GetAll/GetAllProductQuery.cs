using Application.Contracts;
using Application.Dtos.Products;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductQuery : RequestParametersBasic , IRequest<PaginationResponse<ProductDto>> , ICacheQuery
    {
        //public int Id { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        //[NotMapped]
        public int HoursafeData => 1 ;
    }
}
