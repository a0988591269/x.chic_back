using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Features.Products.Queries.GetProductBySlug
{
    public class GetProductBySlugHandler : IRequestHandler<GetProductBySlugQuery, Result<IEnumerable<GetProductBySlugDto>>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductBySlugHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IEnumerable<GetProductBySlugDto>>> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductBySlug(request.Slug);

            if (products == null)
            {
                return Result<IEnumerable<GetProductBySlugDto>>.NotFound();
            }

            var product = products.Select(p => new GetProductBySlugDto(
                p.ProductId,
                p.ProductName,
                p.ShortDescription,
                p.LongDescription,
                p.CategoryId,
                p.IsActive,
                p.TotalSales,
                p.Rating,
                p.IsHot,
                p.IsNew,
                p.IsRecommended,
                p.ProductVariantId,
                p.Sku,
                p.Price,
                p.DiscountPrice,
                p.StockQty,
                p.ImageUrl
                ));
            return Result<IEnumerable<GetProductBySlugDto>>.Success(product);
        }
    }
}
