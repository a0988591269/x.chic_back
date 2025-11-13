using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("ProductVariantImages")]
    public class ProductVariantImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductVariantImageId { get; set; }

        [Required]
        public long ProductVariantId { get; set; }

        [Required, StringLength(2000)]
        public string ImageUrl { get; set; } = string.Empty;

        public int SortOrder { get; set; }

        public ProductVariant? ProductVariant { get; set; }
    }
}
