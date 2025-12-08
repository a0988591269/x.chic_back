using Dapper;
using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Application.Features.Products.Queries.GetProductBySlug
{
    public record GetProductBySlugQuery : IRequest<Result<IEnumerable<GetProductBySlugDto>>>
    {
        public string Slug { get; set; } = string.Empty; 
    }
}
