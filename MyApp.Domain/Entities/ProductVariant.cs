using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    [Table("ProductVariants")]
    // 加入索引
    [Index(nameof(ProductId), nameof(Price))]
    [Index(nameof(ProductId), nameof(DiscountPrice))]
    public class ProductVariant : BaseEntity
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductVariantId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public long ProductId { get; set; }

        /// <summary>
        /// SKU 編碼，唯一
        /// </summary>
        [Required, StringLength(100)]
        public string Sku { get; set; } = string.Empty;

        /// <summary>
        /// 銷售價格
        /// </summary>
        // Price precision handled in FluentConfig
        public decimal Price { get; set; }

        /// <summary>
        /// 折扣價格
        /// </summary>
        public decimal? DiscountPrice { get; set; }   // 如果有折扣

        /// <summary>
        /// 庫存數量
        /// </summary>
        public int StockQty { get; set; } = 0;

        /// <summary>
        /// UPC / EAN
        /// </summary>
        [StringLength(100)]
        public string? Barcode { get; set; }

        /// <summary>
        /// 是否可銷售
        /// </summary>
        public bool IsActive { get; set; } = true;

        public Product? Product { get; set; }

        public ICollection<ProductVariantOptionValue>? OptionValues { get; set; }

        public ICollection<ProductVariantImage>? Images { get; set; }
    }
}
