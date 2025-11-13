using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("OutboxEvents")]
    public class OutboxEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OutboxId { get; set; }

        [Required, StringLength(100)]
        public string AggregateType { get; set; } = string.Empty;

        public long AggregateId { get; set; }

        [Required, StringLength(100)]
        public string EventType { get; set; } = string.Empty;

        [Required]
        public string Payload { get; set; } = string.Empty;

        public DateTime? ProcessedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
