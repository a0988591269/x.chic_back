using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("UserRoles")]
    public class UserRole
    {
        /// <summary>
        /// 主鍵 (PK)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserRoleId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public int RoleId { get; set; }

        // navs (nullable by Q4)
        public User? User { get; set; }

        public Role? Role { get; set; }
    }
}