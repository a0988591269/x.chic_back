using MediatR;
using MyApp.Application.Commons.Results;

namespace MyApp.Application.Features.Products.Queries.GetProductBySlug
{
    public record GetProductBySlugQuery : IRequest<Result<IEnumerable<GetProductBySlugDto>>>
    {
        public string Slug { get; set; } = string.Empty; 
    }
}
