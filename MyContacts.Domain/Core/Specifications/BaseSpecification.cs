using MyContacts.Domain.Core.Models;
using System.Linq.Expressions;

namespace MyContacts.Domain.Core.Specifications
{
    public class BaseSpecification<T> : IBaseSpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
            Includes = new List<Expression<Func<T, object>>>();
        }

        public virtual void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
    }
}
