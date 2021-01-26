using System;
using ProductStore.API.Dtos;

namespace ProductStore.Tests.Common.Builders
{
    public class ProductUpdateRequestBuilder
    {
        private ProductUpdateRequestDto _productUpdateRequest = new ProductUpdateRequestDto();

        public ProductUpdateRequestBuilder WithId(Guid value)
        {
            _productUpdateRequest.Id = value;
            return this;
        }

        public ProductUpdateRequestBuilder WithDescription(string value)
        {
            _productUpdateRequest.Description = value;
            return this;
        }

        public ProductUpdateRequestBuilder WithName(string value)
        {
            _productUpdateRequest.Name = value;
            return this;
        }

        public ProductUpdateRequestBuilder WithPrice(decimal value)
        {
            _productUpdateRequest.Price = value;
            return this;
        }

        public ProductUpdateRequestBuilder WithDeliveryPrice(decimal value)
        {
            _productUpdateRequest.DeliveryPrice = value;
            return this;
        }

        public ProductUpdateRequestBuilder WithDefaultValues()
        {
            _productUpdateRequest = new ProductUpdateRequestDto
            {
                Id = Guid.NewGuid(),
                Name = $"Test Name {DateTime.Now.Ticks}",
                Description = $"Test Description {DateTime.Now.Ticks}",
                Price = 100,
                DeliveryPrice = 5
            };

            return this;
        }

        public ProductUpdateRequestDto Build() => _productUpdateRequest;
    }
}