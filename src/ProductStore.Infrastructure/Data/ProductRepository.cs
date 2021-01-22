using Microsoft.Extensions.Configuration;
using ProductStore.Core.Contracts;
using ProductStore.Core.Models;
using ProductStore.Infrastructure.Configs;

namespace ProductStore.Infrastructure.Data
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DatabaseConfig configuration) : base(configuration)
        {
        }
    }
}