using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("AuditLogs")]
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AuditId { get; set; }

        [Required, StringLength(100)]
        public string EntityType { get; set; } = string.Empty;

        public long? EntityId { get; set; }

        [Required, StringLength(50)]
        public string Operation { get; set; } = string.Empty;

        [StringLength(200)]
        public string PerformedBy { get; set; } = string.Empty;

        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

        public string? PayloadBefore { get; set; }

        public string? PayloadAfter { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
