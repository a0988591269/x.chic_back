namespace MyApp.Application.DTOs
{
    public class ProductDto
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

        // Navigation Property
        public CategoryDto? Category { get; set; }

        public int? ImageId { get; set; }
    }
}
