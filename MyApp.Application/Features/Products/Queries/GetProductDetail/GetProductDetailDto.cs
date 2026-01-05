using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Products.Queries.GetProductDetail
{
    public class GetProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int Sales { get; set; }
        public double Rating { get; set; }

        // 巢狀結構
        public List<ProductOptionDto> Options { get; set; } = new List<ProductOptionDto>();
        public List<VariantDto> Variants { get; set; } = new List<VariantDto>();
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();

        // 模擬資料 (可視需求從 DB 撈或寫死)
        public List<string> Features { get; set; } = new List<string>();
        public List<string> Policy { get; set; } = new List<string>();
        public Dictionary<string, string> Specs { get; set; } = new Dictionary<string, string>();
    }

    public class ProductOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<OptionValueDto> Values { get; set; } = new List<OptionValueDto>();
    }

    public class OptionValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
    }

    public class VariantDto
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQty { get; set; }
        public List<string> Images { get; set; } = new List<string>();

        // 關鍵：這會對應前端的 optionValueIds: number[]
        public List<int> OptionValueIds { get; set; } = new List<int>();
    }

    public class ReviewDto
    {
        public string User { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
    }
}
