using ProductStore.Core.Models;
using System;
using ProductStore.API.ApiModels;

namespace ProductStore.UnitTests.Builders
{
    public class ProductOptionUpdateRequestBuilder
    {
        private ProductOptionUpdateRequest _productOptionUpdateRequest = new ProductOptionUpdateRequest();

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
            _productOptionUpdateRequest = new ProductOptionUpdateRequest
            {
                Id = Guid.NewGuid(),
                Name = $"Test Name {DateTime.Now.Ticks}",
                Description = $"Test Description {DateTime.Now.Ticks}",
                ProductId = Guid.NewGuid()
            };

            return this;
        }

        public ProductOptionUpdateRequest Build() => _productOptionUpdateRequest;
    }
}