using ProductStore.API.Dtos;
using System;

namespace ProductStore.Tests.Common.Builders
{
    public class ProductCreateRequestBuilder
    {
        private ProductCreateRequestDto _productCreateRequest = new ProductCreateRequestDto();

        public ProductCreateRequestBuilder WithDescription(string value)
        {
            _productCreateRequest.Description = value;
            return this;
        }

        public ProductCreateRequestBuilder WithName(string value)
        {
            _productCreateRequest.Name = value;
            return this;
        }

        public ProductCreateRequestBuilder WithEmptyName()
        {
            _productCreateRequest.Name = string.Empty;
            return this;
        }

        public ProductCreateRequestBuilder WithPrice(decimal value)
        {
            _productCreateRequest.Price = value;
            return this;
        }

        public ProductCreateRequestBuilder WithDeliveryPrice(decimal value)
        {
            _productCreateRequest.DeliveryPrice = value;
            return this;
        }

        public ProductCreateRequestBuilder WithDefaultValues()
        {
            _productCreateRequest = new ProductCreateRequestDto
            {
                Name = $"Test Name {DateTime.Now.Ticks}",
                Description = $"Test Description {DateTime.Now.Ticks}",
                Price = 100,
                DeliveryPrice = 5
            };

            return this;
        }

        public ProductCreateRequestDto Build() => _productCreateRequest;
    }
}