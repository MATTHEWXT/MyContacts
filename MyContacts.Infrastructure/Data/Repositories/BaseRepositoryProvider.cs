using MyContacts.Domain.Core.Models;
using MyContacts.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContacts.Infrastructure.Data.Repositories
{
    public class BaseRepositoryProvider : IBaseRepositoryProvider
    {
        private readonly AppDbContext _dbContext;

        public BaseRepositoryProvider(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IBaseRepositoryAsync<T> GetRepository<T>() where T : BaseEntity
        {
            return new BaseRepositoryAsync<T>(_dbContext);
        }
    }
}
