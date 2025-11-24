using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        /// <summary>
        /// 主鍵 (PK)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        /// <summary>
        /// 顯示用名稱（中文或其他）
        /// </summary>
        [Required, StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// 顯示用名稱（英文或其他）
        /// </summary>
        [Required, StringLength(100)]
        public string CategoryEngName { get; set; } = string.Empty;

        /// <summary>
        /// 分類描述 / SEO 用
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Slug，用於 URL
        /// </summary>
        [Required, StringLength(100)]
        public string Slug { get; set; } = string.Empty;

        public ICollection<Product>? Products { get; set; }
    }
}
