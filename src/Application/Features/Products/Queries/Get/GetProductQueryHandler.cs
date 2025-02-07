using Application.Contracts;
using Application.Features.ProductBrands.Queries.GetAll;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries.Get
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly IUnitOfWork _uow;
        public GetProductQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductSpec(request.Id);
            return await _uow.Repository<Product>().GetEntitywithSpec(spec , cancellationToken);
           //ar entity = await _uow.Repository<Product>().GetByIdAsync(request.Id, cancellationToken);
          //if (entity == null) throw new Exception("error message");
          //  return entity;
        }
    }
}