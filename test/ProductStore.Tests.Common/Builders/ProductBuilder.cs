using System;
using ProductStore.Core.Models;

namespace ProductStore.Tests.Common.Builders
{
    public class ProductBuilder
    {
        private Product _product = new Product();

        public ProductBuilder WithId(Guid value)
        {
            _product.Id = value;
            return this;
        }

        public ProductBuilder WithDescription(string value)
        {
            _product.Description = value;
            return this;
        }

        public ProductBuilder WithName(string value)
        {
            _product.Name = value;
            return this;
        }

        public ProductBuilder WithPrice(decimal value)
        {
            _product.Price = value;
            return this;
        }

        public ProductBuilder WithDeliveryPrice(decimal value)
        {
            _product.DeliveryPrice = value;
            return this;
        }

        public ProductBuilder WithDefaultValues()
        {
            _product = new Product
            {
                Id = Guid.NewGuid(),
                Name = $"Test Name {DateTime.Now.Ticks}",
                Description = $"Test Description {DateTime.Now.Ticks}",
                Price = 100,
                DeliveryPrice = 5
            };

            return this;
        }

        public Product Build() => _product;
    }
}