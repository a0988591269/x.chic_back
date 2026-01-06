using MyApp.Domain.Models.Products;

namespace MyApp.Application.Features.Products.Queries.GetProductDetail
{
    public record GetProductDetailDto(int Id, string Name, string Summary, string Description, decimal Price, decimal? DiscountPrice, int Sales, double Rating,
        List<ProductOptionModel> Options, List<VariantModel> Variants, List<ReviewModel> Reviews, List<string> Features, List<string> Policy, Dictionary<string, string> Specs)
    { }
}
