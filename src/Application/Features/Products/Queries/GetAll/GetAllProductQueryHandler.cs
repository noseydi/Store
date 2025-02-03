using Application.Contracts;
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
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery,IEnumerable< Product>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllProductQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable< Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Repository<Product>().GetAllAsync(cancellationToken);
           // if (entity == null) throw new "erro exception";

        }
    }
}
