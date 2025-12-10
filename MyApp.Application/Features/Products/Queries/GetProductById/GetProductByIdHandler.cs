using Dapper;
using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Products.Queries.GetProductBySlug;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdDto>>
    {
        private readonly IConnectionFactory _factory;

        public GetProductByIdHandler(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<Result<GetProductByIdDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var conn = _factory.GetConnection();
            var sql = string.Empty;

            sql = @"SELECT * FROM Products WHERE ProductId = @ProductId";
            var product = await conn.QueryFirstAsync<GetProductByIdDto>(sql, new { ProductId = request.Id });

            sql = @"SELECT * FROM ProductVariants WHERE ProductId = @ProductId";
            var productVariant = await conn.QueryFirstAsync<GetProductByIdDto>(sql, new { ProductId = request.Id });

            sql = @"SELECT * FROM ProductVariantImages WHERE ProductId = @ProductId";
            var ProductVariantImage = await conn.QueryFirstAsync<GetProductByIdDto>(sql, new { ProductId = request.Id });

            if (product == null)
            {
                return Result<GetProductByIdDto>.NotFound();
            }

            return Result<GetProductByIdDto>.Success(product);
        }
    }
}
