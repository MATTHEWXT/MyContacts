
using MyContacts.Domain.Core.Models;
using System.Linq.Expressions;

namespace MyContacts.Domain.Core.Specifications
{
    public interface IBaseSpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>>? Includes { get; }
    }
}
