using Microsoft.Extensions.DependencyInjection;
using ProductStore.Core.Contracts;
using ProductStore.Infrastructure.Data;

namespace ProductStore.Infrastructure.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IProductOptionRepository, ProductOptionRepository>()
                .AddTransient<IProductRepository, ProductRepository>();
        }
    }
}