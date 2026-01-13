namespace MyApp.Application.Features.Categories.Queries.GetCategory
{
    public record GetCategoryDto(int CategoryId, string CategoryName, string CategoryEngName, string? Description, string Slug) { }
}
