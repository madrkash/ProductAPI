using ProductStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStore.Core.Contracts
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<Guid> AddAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(Guid id);
    }
}