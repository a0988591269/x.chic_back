using MyApp.Application.Services.ProductVariants.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces
{
    public interface IProductVariantsService
    {
        Task<IEnumerable<ProductVariantDto>> GetByProductId(long productId);
    }
}
