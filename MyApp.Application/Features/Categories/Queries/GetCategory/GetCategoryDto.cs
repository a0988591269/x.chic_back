using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyApp.Application.Commons.Results;

namespace MyApp.Application.Features.Categories.Queries.GetCategory
{
    public record GetCategoryDto(int CategoryId, string CategoryName, string CategoryEngName, string? Description, string Slug) { }
}
