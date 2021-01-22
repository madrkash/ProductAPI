using ProductStore.Core.Models;
using System;
using ProductStore.API.ApiModels;

namespace ProductStore.UnitTests.Builders
{
    public class ProductOptionCreateRequestBuilder
    {
        private ProductOptionCreateRequest _productOptionCreateRequest = new ProductOptionCreateRequest();

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
            _productOptionCreateRequest = new ProductOptionCreateRequest
            {
                Name = $"Test Name {DateTime.Now.Ticks}",
                Description = $"Test Description {DateTime.Now.Ticks}",
                ProductId = Guid.NewGuid()
            };

            return this;
        }

        public ProductOptionCreateRequest Build() => _productOptionCreateRequest;
    }
}