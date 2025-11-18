using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Shipments")]
    public class Shipment
    {
        /// <summary>
        /// 主鍵 (PK)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ShipmentId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public long OrderId { get; set; }

        /// <summary>
        /// 物流名稱（郵局/黑貓/7-11 等）
        /// </summary>
        [Required, StringLength(100)]
        public string Carrier { get; set; } = string.Empty;

        /// <summary>
        /// 物流追蹤碼
        /// </summary>
        public string? TrackingNo { get; set; }

        /// <summary>
        /// 出貨狀態
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 出貨日期
        /// </summary>
        public DateTime? ShippedAt { get; set; }

        /// <summary>
        /// 到貨日期
        /// </summary>
        public DateTime? DeliveredAt { get; set; }
    }
}
