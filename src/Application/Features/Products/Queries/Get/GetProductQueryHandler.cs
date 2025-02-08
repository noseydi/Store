using Application.Contracts;
using Application.Dtos.Products;
using Application.Features.ProductBrands.Queries.GetAll;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries.Get
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper; 
        public GetProductQueryHandler(IUnitOfWork uow , IMapper mapper )
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductSpec(request.Id);
            var result = await _uow.Repository<Product>().GetEntitywithSpec(spec , cancellationToken);
            return _mapper.Map<ProductDto>(result);
           //ar entity = await _uow.Repository<Product>().GetByIdAsync(request.Id, cancellationToken);
          //if (entity == null) throw new Exception("error message");
          //  return entity;
        }
    }
}