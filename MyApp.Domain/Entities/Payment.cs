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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PaymentId { get; set; }

        [Required]
        public long OrderId { get; set; }

        [Required, StringLength(100)]
        public string Gateway { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string GatewayPaymentId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public byte Status { get; set; }

        public string? MethodJson { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
