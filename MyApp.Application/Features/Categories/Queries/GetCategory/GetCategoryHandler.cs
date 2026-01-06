using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, Result<IEnumerable<GetCategoryDto>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<IEnumerable<GetCategoryDto>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {

            var categories = await _categoryRepository.GetAll();

            if (categories == null)
            {
                return Result<IEnumerable<GetCategoryDto>>.NotFound();
            }

            // 2. Mapping (Entity -> DTO)
            // 這裡可以手動 Map，也可以用 AutoMapper / Mapster
            var category = categories.Select(c => new GetCategoryDto(
                c.CategoryId,
                c.CategoryName,
                c.CategoryEngName,
                c.Description,
                c.Slug
            )).ToList();

            return Result<IEnumerable<GetCategoryDto>>.Success(category);
        }
    }
}
