using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaShopping.Models
{
    public class Product
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int QuantityPerUnit { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        [Required]
        public string ProductImage { get; set; }

        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}
