using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Core.Models
{
    [Table("productoption")]
    public class ProductOption : BaseEntity
    {
        [Column("productid")]
        public Guid ProductId { get; set; }
    }
}