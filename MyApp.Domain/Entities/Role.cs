using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Roles")]
    public class Role
    {
        /// <summary>
        /// 主鍵 (PK)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名稱（如 Admin）
        /// </summary>
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 角色描述 / 用途說明
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }

        // 讓 Role 知道它有哪些權限
        public ICollection<RolePermission>? RolePermissions { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
    }
}