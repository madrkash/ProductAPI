using System;

namespace ProductStore.API.Dtos
{
    public class ProductOptionUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProductId { get; set; }
        public string Description { get; set; }
    }
}