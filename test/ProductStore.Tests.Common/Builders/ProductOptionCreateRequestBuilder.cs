using System;
using ProductStore.API.Dtos;

namespace ProductStore.Tests.Common.Builders
{
    public class ProductOptionCreateRequestBuilder
    {
        private ProductOptionCreateRequestDto _productOptionCreateRequest = new ProductOptionCreateRequestDto();

        public ProductOptionCreateRequestBuilder WithDescription(string value)
        {
            _productOptionCreateRequest.Description = value;
            return this;
        }

        public ProductOptionCreateRequestBuilder WithName(string value)
        {
            _productOptionCreateRequest.Name = value;
            return this;
        }

        public ProductOptionCreateRequestBuilder WithEmptyName()
        {
            _productOptionCreateRequest.Name = string.Empty;
            return this;
        }

        public ProductOptionCreateRequestBuilder WithProductId(Guid value)
        {
            _productOptionCreateRequest.ProductId = value;
            return this;
        }

        public ProductOptionCreateRequestBuilder WithDefaultValues()
        {
            _productOptionCreateRequest = new ProductOptionCreateRequestDto
            {
                Name = $"Test Name {DateTime.Now.Ticks}",
                Description = $"Test Description {DateTime.Now.Ticks}",
                ProductId = Guid.NewGuid()
            };

            return this;
        }

        public ProductOptionCreateRequestDto Build() => _productOptionCreateRequest;
    }
}