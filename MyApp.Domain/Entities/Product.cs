using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }

        [Required, StringLength(200)]
        public string ProductName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool IsActive { get; set; } = true;

        // navs
        public Category? Category { get; set; }

        public ICollection<ProductOption>? Options { get; set; }

        public ICollection<ProductVariant>? Variants { get; set; }
    }
}
