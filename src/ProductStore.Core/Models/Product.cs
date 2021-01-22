using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Core.Models
{
    [Table("product")]
    public class Product : BaseEntity, IEquatable<Product>
    {
        [Column("price")]
        public decimal Price { get; set; }

        [Column("deliveryprice")]
        public decimal DeliveryPrice { get; set; }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id
                   && Name == other.Name
                   && Description == other.Description
                   && Price == other.Price
                   && DeliveryPrice == other.DeliveryPrice;
        }
    }
}