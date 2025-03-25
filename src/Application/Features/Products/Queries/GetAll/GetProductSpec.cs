using Application.Contracts.Specification;
using Domain.Entities;
using Application.Wrappers;
using Application.Features.Products.Queries.GetAll;
using System.Linq.Expressions;


namespace Application.Features.Products.Queries.GetAll
{
    public class GetProductSpec : BaseSpecification<Product>
    {
        public GetProductSpec(GetAllProductQuery specparams) : base(Expression.ExpressionSpec(specparams))

        {

            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            
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
        public GetProductSpec(int id) : base(x => x.Id ==id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }

    public class ProductCountSpec : BaseSpecification<Product>
    {
        public ProductCountSpec(GetAllProductQuery specParams) : base (Expression.ExpressionSpec(specParams)) 
        {
            IsPagingEnabled = false;
        }
    }
    public static class Expression
    {
        public static Expression<Func<Product, bool>> ExpressionSpec(GetAllProductQuery specparams)
        {
            return x =>
                (string.IsNullOrEmpty(specparams.Search) || x.Title.ToLower().Contains(specparams.Search))
                &&
                (!specparams.BrandId.HasValue || x.ProductBrandId == specparams.BrandId)
                &&
                (!specparams.TypeId.HasValue || x.ProductTypeId == specparams.TypeId);
            }

    }

}
