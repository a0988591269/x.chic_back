using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("RolePermissions")]
    public class RolePermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolePermissionId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required, StringLength(200)]
        public string Permission { get; set; } = string.Empty;

        public Role? Role { get; set; }
    }
}
