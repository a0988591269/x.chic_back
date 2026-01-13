namespace MyApp.Domain.Models.Products
{
    public class GetProductDetailModel
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
        public List<ProductOptionModel> Options { get; set; } = new List<ProductOptionModel>();
        public List<VariantModel> Variants { get; set; } = new List<VariantModel>();
        public List<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();

        // 模擬資料 (可視需求從 DB 撈或寫死)
        public List<string> Features { get; set; } = new List<string>();
        public List<string> Policy { get; set; } = new List<string>();
        public Dictionary<string, string> Specs { get; set; } = new Dictionary<string, string>();
    }

    public class ProductOptionModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<OptionValueModel> Values { get; set; } = new List<OptionValueModel>();
    }

    public class OptionValueModel
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
    }

    public class VariantModel
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

    public class ReviewModel
    {
        public string User { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
    }
}
