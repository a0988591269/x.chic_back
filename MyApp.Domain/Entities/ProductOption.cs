using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    
    [Table("ProductOptions")]
    public class ProductOption
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductOptionId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public long ProductId { get; set; }

        /// <summary>
        /// 規格名稱（Color / Size）
        /// </summary>
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty; // Color/Size

        /// <summary>
        /// 選項排序
        /// </summary>
        public int SortOrder { get; set; } = 0;

        public Product? Product { get; set; }

        public ICollection<ProductOptionValue>? Values { get; set; }
    }
}
