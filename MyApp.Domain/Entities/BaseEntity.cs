using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 樂觀鎖
        /// </summary>
        public byte[]? RowVersion { get; set; }
    }
}
