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
        /// <summary>
        /// 主鍵 (PK)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolePermissionId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        [Required]
        public int RoleId { get; set; }

        /// <summary>
        /// 權限名稱（ex: Product.Read）
        /// </summary>
        [Required, StringLength(200)]
        public string Permission { get; set; } = string.Empty;

        public Role? Role { get; set; }
    }
}
