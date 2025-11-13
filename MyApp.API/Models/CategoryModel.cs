namespace MyApp.API.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? CategoryEngName { get; set; }

        public string CategoryUrl { get; set; } = string.Empty;
    }
}
