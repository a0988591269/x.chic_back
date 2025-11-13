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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RefundId { get; set; }

        [Required]
        public long OrderId { get; set; }

        public long? PaymentId { get; set; }

        public decimal Amount { get; set; }

        public string? Reason { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
