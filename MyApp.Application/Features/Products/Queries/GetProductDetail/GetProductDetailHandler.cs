using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Features.Products.Queries.GetProductDetail
{
    public class GetProductDetailHandler : IRequestHandler<GetProductDetailQuery, Result<GetProductDetailDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductDetailHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<GetProductDetailDto>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            var data = await _productRepository.GetProductById(request.Id);
            if (data == null)
            {
                return Result<GetProductDetailDto>.NotFound();
            }

            var product = new GetProductDetailDto(data.Id, data.Name, data.Summary, data.Description, data.Price, data.DiscountPrice,
                data.Sales, data.Rating, data.Options, data.Variants, data.Reviews, data.Features, data.Policy, data.Specs);

            return Result<GetProductDetailDto>.Success(product);
        }
    }
}
