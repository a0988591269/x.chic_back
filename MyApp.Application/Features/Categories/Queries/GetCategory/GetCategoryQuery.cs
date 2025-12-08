using Dapper;
using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Application.Features.Categories.Queries.GetCategory
{
    public record GetCategoryQuery : IRequest<Result<IEnumerable<GetCategoryDto>>>
    {

    }
}
