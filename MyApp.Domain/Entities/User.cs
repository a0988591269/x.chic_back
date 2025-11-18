using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        /// <summary>
        /// 主鍵 (PK)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        /// <summary>
        /// 對外識別用（公開 API 使用，不暴露 UserId）
        /// </summary>
        public Guid UserUuid { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 登入識別，具唯一索引
        /// </summary>
        [Required, EmailAddress, StringLength(320)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 顯示名稱
        /// </summary>
        [StringLength(200)]
        public string? Name { get; set; }

        /// <summary>
        /// 存放雜湊後密碼（不要存明碼）
        /// </summary>
        [StringLength(200)]
        public string? HashedPassword { get; set; }

        /// <summary>
        /// 會員等級（業務定義：0=一般、1=VIP 等）
        /// </summary>
        public byte Tier { get; set; } = 0;

        /// <summary>
        /// 帳號狀態（0=Inactive/Disabled,1=Active）
        /// </summary>
        public byte Status { get; set; } = 1;

        // nav
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}