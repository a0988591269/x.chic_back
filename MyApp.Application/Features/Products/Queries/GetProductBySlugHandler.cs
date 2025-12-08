using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Application.Services.Categories.Queries;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Application.Features.Products.Queries
{
    public class GetProductBySlugHandler : IRequestHandler<GetProductBySlugQuery, Result<GetProductBySlugDto>>
    {
        public async Task<GetProductBySlugDto> GetProductBySlugHandler(GetProductBySlugQuery request, CancellationToken cancellationToken)
        {
            var products = await request.GetBySlug(request.Slug);
            var product = products.FirstOrDefault();
            if (product == null)
            {
                return Result<GetProductBySlugDto>.Failure("Product not found", ResultStatus.NotFound);
            }
            return Result<GetProductBySlugDto>.Success(product);
        }
    }
}
