using Microsoft.Extensions.DependencyInjection;
using ProductStore.Core.Contracts;
using ProductStore.Core.Services;

namespace ProductStore.Core.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IProductOptionService, ProductOptionService>()
                .AddTransient<IProductService, ProductService>();
        }
    }
}