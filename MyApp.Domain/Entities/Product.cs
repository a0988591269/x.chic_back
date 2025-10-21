using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("商品編號")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        [Description("商品名稱")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [Description("簡介|保固")]
        public string Intro { get; set; } = string.Empty;

        [StringLength(500)]
        [Description("商品描述")]
        public string? Description { get; set; }

        [StringLength(500)]
        [Description("注意事項")]
        public string? Notice { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Description("商品價格")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Description("商品折扣價")]
        public decimal? Discount { get; set; }

        [Description("庫存量")]
        public int Stock { get; set; }

        [Description("銷售量")]
        public int SalesVolume { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        // Navigation Property
        public Category? Category { get; set; }

        public int? ImageId { get; set; }
    }
}
