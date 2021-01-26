using System;

namespace ProductStore.API.Dtos
{
    public class ProductOptionCreateRequestDto
    {
        public string Name { get; set; }
        public Guid ProductId { get; set; }
        public string Description { get; set; }
    }
}