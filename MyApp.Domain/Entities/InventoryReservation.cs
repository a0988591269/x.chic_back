using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("InventoryReservations")]
    public class InventoryReservation
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ReservationId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public long ProductVariantId { get; set; }

        /// <summary>
        /// 保留數量
        /// </summary>
        [Required]
        public int ReservationQty { get; set; }

        /// <summary>
        /// 對應訂單（可為 NULL）
        /// </summary>
        public long? ReservedForOrderId { get; set; }

        /// <summary>
        /// 0=待使用, 1=已使用, 2=已過期
        /// </summary>
        public byte Status { get; set; } // 0=Reserved,1=Confirmed,2=Released,3=Expired

        /// <summary>
        /// 到期時間
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ProductVariant? ProductVariant { get; set; }
    }
}
