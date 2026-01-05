using Dapper;
using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Features.Products.Queries.GetProductBySlug
{
    public record GetProductBySlugQuery : IRequest<Result<IEnumerable<GetProductBySlugDto>>>
    {
        public string Slug { get; set; } = string.Empty; 
    }
}
