using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("ProductVariantOptionValue")]
    public class ProductVariantOptionValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductVariantOptionValueId { get; set; }

        [Required]
        public long ProductVariantId { get; set; }

        [Required]
        public int ProductOptionValueId { get; set; }

        public ProductVariant? ProductVariant { get; set; }

        public ProductOptionValue? ProductOptionValue { get; set; }
    }
}
