using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Core.Models
{
    [Table("productoption")]
    public class ProductOption : BaseEntity, IEquatable<ProductOption>
    {
        [Column("productid")]
        public Guid ProductId { get; set; }

        public bool Equals(ProductOption other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id
                   && Name == other.Name
                   && Description == other.Description
                   && ProductId == other.ProductId;
        }
    }
}