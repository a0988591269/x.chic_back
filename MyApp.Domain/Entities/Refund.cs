using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Refunds")]
    public class Refund
    {
        /// <summary>
        /// 主鍵 (PK)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RefundId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public long OrderId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        public long? PaymentId { get; set; }

        /// <summary>
        /// 退款金額
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 退款理由
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// 退款狀態（申請/成功/失敗）
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 建立時間（已自動 Default）
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
