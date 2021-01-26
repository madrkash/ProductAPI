using System;

namespace ProductStore.Core.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Guid productId)
            : base($"No product found with id {productId}")
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }
    }
}