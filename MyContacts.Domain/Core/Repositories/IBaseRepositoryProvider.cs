using Microsoft.EntityFrameworkCore;
using MyContacts.Domain.Core.Models;

namespace MyContacts.Domain.Core.Repositories
{
    public interface IBaseRepositoryProvider
    {
        IBaseRepositoryAsync<T> GetRepository<T>() where T : BaseEntity;
    }
}
