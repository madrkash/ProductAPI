using System;

namespace ProductStore.API.Exceptions
{
    public class ProductOptionNotFoundException : Exception
    {
        public ProductOptionNotFoundException(Guid productOptionId)
            : base($"No product option found with id {productOptionId}")
        {

        }

        public ProductOptionNotFoundException(string message) : base(message)
        {
        }
    }
}