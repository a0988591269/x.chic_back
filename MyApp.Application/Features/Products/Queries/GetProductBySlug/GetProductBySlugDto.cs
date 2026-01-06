using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyApp.Domain.Entities;

namespace MyApp.Application.Features.Products.Queries.GetProductBySlug
{
    public record GetProductBySlugDto(long ProductId, string ProductName, string? ShortDescription, string? LongDescription,
        int CategoryId, bool IsActive, int TotalSales, float Rating, bool IsHot, bool IsNew, bool IsRecommended, long ProductVariantId,
        string Sku, decimal Price, decimal? DiscountPrice, int StockQty, string ImageUrl)
    { }
}
