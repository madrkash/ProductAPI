using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStore.Core.Contracts
{
    public interface IService<T>
    {
        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<Guid> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(Guid id);
    }
}