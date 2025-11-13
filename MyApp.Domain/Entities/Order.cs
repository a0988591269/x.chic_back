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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }

        public Guid OrderUuid { get; set; } = Guid.NewGuid();

        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;

        public long? UserId { get; set; }

        public byte Status { get; set; }

        public decimal TotalAmount { get; set; }

        [StringLength(3)]
        public string Currency { get; set; } = "TWD";

        public string? BillingAddress { get; set; }
        public string? ShippingAddress { get; set; }

        [Required]
        public string ItemsSnapshot { get; set; } = string.Empty;

        public string? PaymentSnapshot { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
