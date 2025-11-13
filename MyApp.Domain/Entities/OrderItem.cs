using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderItemId { get; set; }

        [Required]
        public long OrderId { get; set; }

        [Required]
        public long ProductVariantId { get; set; }

        [StringLength(100)]
        public string? Sku { get; set; }

        [StringLength(200)]
        public string? Title { get; set; }

        public decimal PriceAtPurchase { get; set; }

        public int Qty { get; set; }

        public decimal Subtotal { get; set; }
    }
}
