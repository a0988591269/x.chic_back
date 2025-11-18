using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("ProductVariantImages")]
    public class ProductVariantImage
    {
        /// <summary>
        /// 主鍵 (PK)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductVariantImageId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public long ProductVariantId { get; set; }

        /// <summary>
        /// 圖片 URL
        /// </summary>
        [Required, StringLength(2000)]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 排序（0 最前）
        /// </summary>
        public int SortOrder { get; set; }

        public ProductVariant? ProductVariant { get; set; }
    }
}
