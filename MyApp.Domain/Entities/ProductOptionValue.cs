using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("ProductOptionValues")]
    public class ProductOptionValue
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductOptionValueId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public int ProductOptionId { get; set; }

        /// <summary>
        /// 規格值名稱
        /// </summary>
        [Required, StringLength(200)]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// 用於色票、樣式圖展示
        /// </summary>
        public string? ValueImageUrl { get; set; }

        public ProductOption? ProductOption { get; set; }

        public ICollection<ProductVariantOptionValue>? ProductVariantOptionValues { get; set; }
    }
}
