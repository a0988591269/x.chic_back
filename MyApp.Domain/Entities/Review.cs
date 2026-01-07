using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Reviews")]
    [Index(nameof(ProductId))]
    [Index(nameof(ProductId), nameof(Rating))]
    [Index(nameof(OrderItemId))]
    public class Review : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ReviewId { get; set; }

        public long ProductId { get; set; }

        public long ProductVariantId { get; set; }

        public long UserId { get; set; }

        public long OrderItemId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // ⭐ 1~5 星

        [MinLength(20), MaxLength(2000)]
        public string? Comment { get; set; }

        // 🔗 Navigation
        public Product Product { get; set; } = new Product();

        public ProductVariant Variant { get; set; } = new ProductVariant();

        public User User { get; set; }

        public OrderItem OrderItem { get; set; } = new OrderItem();
    }
}
