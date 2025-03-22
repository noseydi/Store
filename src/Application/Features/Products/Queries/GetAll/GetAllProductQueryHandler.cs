using Application.Contracts;
using Application.Dtos.Products;
using Application.Features.ProductBrands.Queries.GetAll;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery,PaginationResponse< ProductDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllProductQueryHandler(IUnitOfWork uow , IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PaginationResponse< ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductSpec(request);
          var result =  await _uow.Repository<Product>().ListAsyncSpec(spec , cancellationToken);
            var count = await _uow.Repository<Product>().CountAsyncSpec(new  ProductCountSpec(request),cancellationToken);

            var model = _mapper.Map<IEnumerable<ProductDto>>(result);
            //
            return new PaginationResponse<ProductDto>(request.PageIndex , request.PageSize , count , model);
             // return await _uow.Repository<Product>().GetAllAsync(cancellationToken);
           // if (entity == null) throw new "erro exception";

        }
    }
}
