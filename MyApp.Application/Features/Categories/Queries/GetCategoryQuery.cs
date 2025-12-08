using Dapper;
using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Categories.Queries;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Application.Services.Categories.Queries
{
    public record GetCategoryQuery : IRequest<Result<IEnumerable<GetCategoryDto>>>
    {

    }
}
