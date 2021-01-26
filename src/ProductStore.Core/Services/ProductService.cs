using ProductStore.Core.Contracts;
using ProductStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductStore.Core.Exceptions;

namespace ProductStore.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionService _productOptionService;

        public ProductService(IProductRepository productRepository, IProductOptionService productOptionService)
        {
            _productRepository = productRepository;
            _productOptionService = productOptionService;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            if (!products.Any())
            {
                throw new EntityNotFoundException("No products available in the store");
            }

            return products;
        }

        public Task<Guid> AddAsync(Product product)
        {
            return _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            var updateResult = await _productRepository.UpdateAsync(product);

            if (updateResult == 0)
            {
                throw new ProductNotFoundException(product.Id);
            }
        }

        /// <summary>
        /// Cascade Delete - Delete the Product and its Options
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _productOptionService.DeleteListAsync(id);
            var deleteResult = await _productRepository.DeleteAsync(id);

            if (deleteResult == 0)
            {
                throw new ProductNotFoundException(id);
            }
        }
    }
}