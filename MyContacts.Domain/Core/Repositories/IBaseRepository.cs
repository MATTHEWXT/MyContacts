using MyContacts.Domain.Core.Models;
using MyContacts.Domain.Core.Specifications;

namespace MyContacts.Domain.Core.Repositories
{
    public interface IBaseRepositoryAsync<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IList<T>> ListAsync(IBaseSpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteRangeAsync(T entity);
    }
}
