using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("ProductVariants")]
    public class ProductVariant : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductVariantId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required, StringLength(100)]
        public string Sku { get; set; } = string.Empty;

        // Price precision handled in FluentConfig
        public decimal Price { get; set; }

        public int StockQty { get; set; } = 0;

        [StringLength(100)]
        public string? Barcode { get; set; }

        public bool IsActive { get; set; } = true;

        public Product? Product { get; set; }

        public ICollection<ProductVariantOptionValue>? OptionValues { get; set; }

        public ICollection<ProductVariantImage>? Images { get; set; }
    }
}
