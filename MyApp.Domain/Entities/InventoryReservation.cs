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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ReservationId { get; set; }

        [Required]
        public long ProductVariantId { get; set; }

        [Required]
        public int ReservationQty { get; set; }

        public long? ReservedForOrderId { get; set; }

        public byte Status { get; set; } // 0=Reserved,1=Confirmed,2=Released,3=Expired

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ProductVariant? ProductVariant { get; set; }
    }
}
