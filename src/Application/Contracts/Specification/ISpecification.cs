using Domain.Entities.Base;
using System.Linq.Expressions;

namespace Application.Contracts.Specification
{
    public interface ISpecification<T>  where T : BaseEntity
    {
        Expression<Func<T , bool >> Criteria { get;  }
        List<Expression<Func<T , object >>> Includes { get;  }
    }
}
