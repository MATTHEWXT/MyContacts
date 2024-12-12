using Microsoft.EntityFrameworkCore;
using MyContacts.Domain.Core.Models;
using MyContacts.Domain.Core.Repositories;
using MyContacts.Domain.Core.Specifications;

namespace MyContacts.Infrastructure.Data.Repositories
{
    public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public BaseRepositoryAsync(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddListAsync(IList<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            _dbContext.SaveChanges();
        }

        public async Task DeleteRangeAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id) ?? throw new InvalidOperationException("Entity not found by id");
        }

        public async Task<T> GetEntityAsync(IBaseSpecification<T> spec)
        {
            T entity = await ApplySpecification(spec).FirstAsync();

            if (entity == null)
                throw new InvalidOperationException("Entity not found by id");

            return entity;
        }

        public async Task<bool> IsExistEntity(IBaseSpecification<T> spec)
        {
            var entity = await ApplySpecification(spec).ToListAsync();
            
            if(entity.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IList<T>> ListAsync(IBaseSpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(IBaseSpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }
    }
}
