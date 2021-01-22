using System;
using ProductStore.Core.Models;

namespace ProductStore.API.ApiModels
{
    public class ProductViewModel : IEquatable<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other)) return false;

            return Id == other.Id
                   && Name == other.Name
                   && Description == other.Description
                   && Price == other.Price
                   && DeliveryPrice == other.DeliveryPrice;
        }
    }
}