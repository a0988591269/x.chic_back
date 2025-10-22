using MyApp.Application.DTOs;

namespace MyApp.API.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Intro { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Notice { get; set; }

        public decimal Price { get; set; }

        public decimal? Discount { get; set; }

        public int Stock { get; set; }

        public int SalesVolume { get; set; }

        public int CategoryId { get; set; }

        public int? ImageId { get; set; }
    }
}
