using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Payments")]
    public class Payment
    {
        /// <summary>
        /// 主鍵 (PK)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PaymentId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public long OrderId { get; set; }

        /// <summary>
        /// 金流平台名稱
        /// </summary>
        [Required, StringLength(100)]
        public string Gateway { get; set; } = string.Empty;

        /// <summary>
        /// 金流方回傳的交易編號
        /// </summary>
        [Required, StringLength(200)]
        public string GatewayPaymentId { get; set; } = string.Empty;

        /// <summary>
        /// 付款金額
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 交易狀態（成功、失敗、待確認等）
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 支付方式詳細資料（如卡號後四碼）
        /// </summary>
        public string? MethodJson { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
