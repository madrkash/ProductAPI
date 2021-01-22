using ProductStore.Core.Models;

namespace ProductStore.Core.Contracts
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
    }
}