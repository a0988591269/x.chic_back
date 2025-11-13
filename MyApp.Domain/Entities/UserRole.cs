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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserRoleId { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        // navs (nullable by Q4)
        public User? User { get; set; }

        public Role? Role { get; set; }
    }
}