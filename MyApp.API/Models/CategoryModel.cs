namespace MyApp.API.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string CategoryEngName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Slug { get; set; } = string.Empty;
    }
}
