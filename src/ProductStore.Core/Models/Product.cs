using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Core.Models
{
    [Table("product")]
    public class Product : BaseEntity
    {
        [Column("price")]
        public decimal Price { get; set; }

        [Column("deliveryprice")]
        public decimal DeliveryPrice { get; set; }
    }
}