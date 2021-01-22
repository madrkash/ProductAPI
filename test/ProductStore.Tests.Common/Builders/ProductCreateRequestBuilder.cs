using System;
using ProductStore.API.ApiModels;

namespace ProductStore.Tests.Common.Builders
{
    public class ProductCreateRequestBuilder
    {
        private ProductCreateRequest _productCreateRequest = new ProductCreateRequest();

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
            _productCreateRequest = new ProductCreateRequest
            {
                Name = $"Test Name {DateTime.Now.Ticks}",
                Description = $"Test Description {DateTime.Now.Ticks}",
                Price = 100,
                DeliveryPrice = 5
            };

            return this;
        }

        public ProductCreateRequest Build() => _productCreateRequest;
    }
}