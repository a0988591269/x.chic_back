namespace MyApp.Application.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? CategoryEngName { get; set; }

        public string CategoryUrl { get; set; } = string.Empty;
    }
}
