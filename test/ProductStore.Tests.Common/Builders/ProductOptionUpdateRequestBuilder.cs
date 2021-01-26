using ProductStore.API.Dtos;
using System;

namespace ProductStore.Tests.Common.Builders
{
    public class ProductOptionUpdateRequestBuilder
    {
        private ProductOptionUpdateRequestDto _productOptionUpdateRequest = new ProductOptionUpdateRequestDto();

        public ProductOptionUpdateRequestBuilder WithId(Guid value)
        {
            _productOptionUpdateRequest.Id = value;
            return this;
        }

        public ProductOptionUpdateRequestBuilder WithDescription(string value)
        {
            _productOptionUpdateRequest.Description = value;
            return this;
        }

        public ProductOptionUpdateRequestBuilder WithName(string value)
        {
            _productOptionUpdateRequest.Name = value;
            return this;
        }

        public ProductOptionUpdateRequestBuilder WithProductId(Guid value)
        {
            _productOptionUpdateRequest.ProductId = value;
            return this;
        }

        public ProductOptionUpdateRequestBuilder WithDefaultValues()
        {
            _productOptionUpdateRequest = new ProductOptionUpdateRequestDto
            {
                Id = Guid.NewGuid(),
                Name = $"Test Name {DateTime.Now.Ticks}",
                Description = $"Test Description {DateTime.Now.Ticks}",
                ProductId = Guid.NewGuid()
            };

            return this;
        }

        public ProductOptionUpdateRequestDto Build() => _productOptionUpdateRequest;
    }
}