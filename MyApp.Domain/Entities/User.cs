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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        public Guid UserUuid { get; set; } = Guid.NewGuid();

        [Required, EmailAddress, StringLength(320)]
        public string Email { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Name { get; set; }

        [StringLength(200)]
        public string? HashedPassword { get; set; }

        /// <summary>
        /// 等級 0: 一般會員, 1: VIP, 2: SVIP
        /// </summary>
        public byte Tier { get; set; } = 0;

        public byte Status { get; set; } = 1;

        // nav
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}