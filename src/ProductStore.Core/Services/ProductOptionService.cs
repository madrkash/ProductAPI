using ProductStore.Core.Contracts;
using ProductStore.Core.Exceptions;
using ProductStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore.Core.Services
{
    public class ProductOptionService : IProductOptionService
    {
        private readonly IProductOptionRepository _productOptionRepository;

        public ProductOptionService(IProductOptionRepository productOptionRepository)
        {
            _productOptionRepository = productOptionRepository;
        }

        public async Task<ProductOption> GetByIdAsync(Guid id)
        {
            var productOption = await _productOptionRepository.GetByIdAsync(id);

            if (productOption == null)
            {
                throw new ProductOptionNotFoundException(id);
            }
            return productOption;
        }

        public async Task<IEnumerable<ProductOption>> GetAllAsync()
        {
            var productOptions = await _productOptionRepository.GetAllAsync();
            if (!productOptions.Any())
            {
                throw new EntityNotFoundException("No product options available in the store");
            }
            return productOptions;
        }

        public async Task<Guid> AddAsync(ProductOption productOption)
        {
            return await _productOptionRepository.AddAsync(productOption);
        }

        public async Task UpdateAsync(ProductOption productOption)
        {
            var updateResult = await _productOptionRepository.UpdateAsync(productOption);

            if (updateResult == 0)
            {
                throw new ProductOptionNotFoundException(productOption.Id);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var deleteResult = await _productOptionRepository.DeleteAsync(id);
            if (deleteResult == 0)
            {
                throw new ProductOptionNotFoundException(id);
            }
        }

        public async Task<IEnumerable<ProductOption>> GetAllOptionsByProductIdAsync(Guid id)
        {
            var productOptions = await _productOptionRepository.GetAllOptionsByProductIdAsync(id);
            if (productOptions == null || !productOptions.Any())
            {
                throw new ProductOptionNotFoundException($"No product options available for product with Id {id}");
            }
            return productOptions;
        }

        public Task<int> DeleteListAsync(Guid productId)
        {
            return  _productOptionRepository.DeleteListAsync(productId);
        }
    }
}