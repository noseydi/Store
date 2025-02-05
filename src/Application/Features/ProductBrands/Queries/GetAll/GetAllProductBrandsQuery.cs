using Application.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductBrands.Queries.GetAll
{
    public class GetAllProductBrandsQuery : IRequest<IEnumerable<ProductBrand>>
    {

    }
}
