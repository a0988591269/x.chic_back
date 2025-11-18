using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        [Required, StringLength(200)]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 商品簡短摘要
        /// </summary>
        [StringLength(500)]
        public string? ShortDescription { get; set; }

        /// <summary>
        /// 商品詳細說明
        /// </summary>
        public string? LongDescription { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// 是否上架中
        /// </summary>
        public bool IsActive { get; set; } = true;

        // navs
        public Category? Category { get; set; }

        public ICollection<ProductOption>? Options { get; set; }

        public ICollection<ProductVariant>? Variants { get; set; }
    }
}
