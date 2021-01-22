using System;

namespace ProductStore.API.ApiModels
{
    public class ProductOptionCreateRequest
    {
        public string Name { get; set; }
        public Guid ProductId { get; set; }
        public string Description { get; set; }
    }
}