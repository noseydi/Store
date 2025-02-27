using Application.Contracts.Specification;
using Domain.Entities;
using Application.Wrappers;


namespace Application.Features.Products.Queries.GetAll
{
    public class GetProductSpec : BaseSpecification<Product>
    {
        public GetProductSpec(GetAllProductQuery request) : base (x=>
        !request.BrandId.HasValue || x.ProductBeandId == request.BrandId
        && (!request.TypeId.HasValue || x.ProductTypeId == request.TypeId))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x=> x.ProductType);
            if (request.TypeSort == TypeSort.Desc)
            {
                switch (request.Sort)
                {
                    case 1:
                        AddOrderByDesc(x => x.Title);
                        break;
                    case 2:
                        AddOrderByDesc(x => x.ProductType.Title);
                        break;
                    default:
                        AddOrderByDesc(x => x.Title);
                        break;
                }
            }
            else
            {
                switch (request.Sort)
                {
                    case 1:
                        AddOrderBy(x => x.Title);
                        break;
                    case 2:
                        AddOrderBy(x => x.ProductType.Title);
                        break;
                    default:
                        AddOrderBy(x => x.Title);
                        break;
                }
            }
        }
        public GetProductSpec(int Id): base (x => x.Id ==  Id) 
        {
        AddInclude (x => x.ProductBrand);
            AddInclude(x=> x.ProductType);
        }
    }
}
