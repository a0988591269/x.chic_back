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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ShipmentId { get; set; }

        [Required]
        public long OrderId { get; set; }

        [Required, StringLength(100)]
        public string Carrier { get; set; } = string.Empty;

        public string? TrackingNo { get; set; }

        public byte Status { get; set; }

        public DateTime? ShippedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }
    }
}
