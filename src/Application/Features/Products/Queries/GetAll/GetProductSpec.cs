using Application.Contracts.Specification;
using Domain.Entities;
using Application.Wrappers;


namespace Application.Features.Products.Queries.GetAll
{
    public class GetProductSpec : BaseSpecification<Product>
    {
        public GetProductSpec(GetAllProductQuery specparams) : base (x=>
        !specparams.BrandId.HasValue || x.ProductBeandId == specparams.BrandId
        && (!specparams.TypeId.HasValue || x.ProductTypeId == specparams.TypeId))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x=> x.ProductType);
            if (specparams.TypeSort == TypeSort.Desc)
            {
                switch (specparams.Sort)
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
                switch (specparams.Sort)
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
            ApplyPaging(specparams.PageSize * (specparams.PageIndex - 1), specparams.PageSize, true);
        }
        public GetProductSpec(int Id): base (x => x.Id ==  Id) 
        {
        AddInclude (x => x.ProductBrand);
            AddInclude(x=> x.ProductType);
        }
    }
}
