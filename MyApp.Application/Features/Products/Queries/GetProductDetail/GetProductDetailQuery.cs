using MediatR;
using MyApp.Application.Commons.Results;

namespace MyApp.Application.Features.Products.Queries.GetProductDetail
{
    public record GetProductDetailQuery : IRequest<Result<GetProductDetailDto>>
    {
        public long Id { get; set; }
    }
}