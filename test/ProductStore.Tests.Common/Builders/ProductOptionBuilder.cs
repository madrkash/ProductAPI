using System;
using ProductStore.Core.Models;

namespace ProductStore.Tests.Common.Builders
{
    public class ProductOptionBuilder
    {
        private ProductOption _productOption = new ProductOption();

        public ProductOptionBuilder WithId(Guid value)
        {
            _productOption.Id = value;
            return this;
        }

        public ProductOptionBuilder WithDescription(string value)
        {
            _productOption.Description = value;
            return this;
        }

        public ProductOptionBuilder WithName(string value)
        {
            _productOption.Name = value;
            return this;
        }

        public ProductOptionBuilder WithProductId(Guid value)
        {
            _productOption.ProductId = value;
            return this;
        }

        public ProductOptionBuilder WithDefaultValues()
        {
            _productOption = new ProductOption
            {
                Id = Guid.NewGuid(),
                Name = $"Test Name {DateTime.Now.Ticks}",
                Description = $"Test Description {DateTime.Now.Ticks}",
                ProductId = Guid.NewGuid()
            };

            return this;
        }

        public ProductOption Build() => _productOption;
    }
}