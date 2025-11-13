using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    
    [Table("ProductOptions")]
    public class ProductOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductOptionId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty; // Color/Size

        public int SortOrder { get; set; } = 0;

        public Product? Product { get; set; }

        public ICollection<ProductOptionValue>? Values { get; set; }
    }
}
