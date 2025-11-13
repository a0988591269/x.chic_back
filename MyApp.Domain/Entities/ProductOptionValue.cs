using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("ProductOptionValues")]
    public class ProductOptionValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductOptionValueId { get; set; }

        [Required]
        public int ProductOptionId { get; set; }

        [Required, StringLength(200)]
        public string Value { get; set; } = string.Empty;

        public string? ValueImageUrl { get; set; }

        public ProductOption? ProductOption { get; set; }

        public ICollection<ProductVariantOptionValue>? ProductVariantOptionValues { get; set; }
    }
}
