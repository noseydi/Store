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
    public class GetAllProductBrandsQueryHandler : IRequestHandler<GetAllProductBrandsQuery ,IEnumerable< ProductBrand>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllProductBrandsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<ProductBrand>> Handle(GetAllProductBrandsQuery request , CancellationToken cancellationToken)
            {
            return await _uow.Repository<ProductBrand >().GetAllAsync(cancellationToken);
            }
    }
}
