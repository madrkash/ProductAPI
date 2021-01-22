using ProductStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStore.Core.Contracts
{
    public interface IProductOptionRepository : IAsyncRepository<ProductOption>
    {
        Task<IEnumerable<ProductOption>> GetAllOptionsByProductIdAsync(Guid id);

        Task<int> DeleteListAsync(Guid productId);
    }
}