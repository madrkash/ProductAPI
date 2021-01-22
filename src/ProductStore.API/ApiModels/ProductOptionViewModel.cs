using System;
using ProductStore.Core.Models;

namespace ProductStore.API.ApiModels
{
    public class ProductOptionViewModel : IEquatable<ProductOption>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProductId { get; set; }
        public string Description { get; set; }

        public bool Equals(ProductOption other)
        {
            if (ReferenceEquals(null, other)) return false;

            return Id == other.Id
                   && Name == other.Name
                   && Description == other.Description;
        }
    }
}