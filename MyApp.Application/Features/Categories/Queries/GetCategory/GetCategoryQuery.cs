using MediatR;
using MyApp.Application.Commons.Results;

namespace MyApp.Application.Features.Categories.Queries.GetCategory
{
    public record GetCategoryQuery : IRequest<Result<IEnumerable<GetCategoryDto>>>
    {

    }
}
