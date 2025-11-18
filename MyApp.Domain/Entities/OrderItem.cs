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
        /// <summary>
        /// 主鍵 (PK)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderItemId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public long OrderId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public long ProductVariantId { get; set; }

        /// <summary>
        /// SKU 字串（快照）
        /// </summary>
        [StringLength(100)]
        public string? Sku { get; set; }

        /// <summary>
        /// 商品名稱快照
        /// </summary>
        [StringLength(200)]
        public string? Title { get; set; }

        /// <summary>
        /// 下單時單價
        /// </summary>
        public decimal PriceAtPurchase { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 小計
        /// </summary>
        public decimal Subtotal { get; set; }
    }
}
