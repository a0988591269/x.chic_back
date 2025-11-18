using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }

        /// <summary>
        /// 公開查詢使用的安全 UUID
        /// </summary>
        public Guid OrderUuid { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 人類可讀訂單編號（唯一）
        /// </summary>
        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// 外部主鍵 (FK) - 使用者
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 訂單狀態
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 訂單總金額
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 幣別，預設 TWD
        /// </summary>
        [StringLength(3)]
        public string Currency { get; set; } = "TWD";

        /// <summary>
        /// 帳單地址 JSON
        /// </summary>
        public string? BillingAddress { get; set; }

        /// <summary>
        /// 收件地址 JSON
        /// </summary>
        public string? ShippingAddress { get; set; }

        /// <summary>
        /// 購物明細快照（防止後改價格）
        /// </summary>
        [Required]
        public string ItemsSnapshot { get; set; } = string.Empty;

        /// <summary>
        /// 支付回報快照
        /// </summary>
        public string? PaymentSnapshot { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
