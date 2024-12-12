using MyContacts.Domain.Core.Models;
using MyContacts.Domain.Core.Specifications;

namespace MyContacts.Domain.Core.Repositories
{
    public interface IBaseRepositoryAsync<T> where T : BaseEntity
    {
        Task<bool> IsExistEntity(IBaseSpecification<T> spec);
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetEntityAsync(IBaseSpecification<T> spec);
        Task<IList<T>> ListAsync(IBaseSpecification<T> spec);
        Task AddAsync(T entity);
        Task AddListAsync(IList<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteRangeAsync(T entity);
    }
}
