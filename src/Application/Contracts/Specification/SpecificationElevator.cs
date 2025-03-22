using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Specification
{
    public class SpecificationElevator<T> where T : BaseEntity
    {
        public SpecificationElevator() { }
        public static IQueryable<T> GetQuery(IQueryable<T> inputquery, ISpecification<T> specification)
        {
            var query = inputquery.AsQueryable();
            if (specification.Predicate != null)
            {
                query = query.Where(specification.Predicate);
            }
            if (specification.OrderBy!=null)
            {
                query= query.OrderBy(specification.OrderBy);
            }
            if (specification.OrderByDesc != null)
            {
                query=query.OrderByDescending(specification.OrderByDesc);
            }
            if (specification.Includes.Any())
            {
                query = specification.Includes.Aggregate(query, (current, value) => current.Include(value));
            }
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }
            return query;
        }
    }
}
