using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required, StringLength(100)]
        public string CategoryEngName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
