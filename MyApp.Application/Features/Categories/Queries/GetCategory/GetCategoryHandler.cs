using Azure.Core;
using Dapper;
using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Products.Queries.GetProductBySlug;
using MyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, Result<IEnumerable<GetCategoryDto>>>
    {
        private readonly IConnectionFactory _factory;

        public GetCategoryHandler(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<Result<IEnumerable<GetCategoryDto>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            using var conn = _factory.GetConnection();

            var sql = @" SELECT * FROM Categories ";
            var category = await conn.QueryAsync<GetCategoryDto>(sql);

            if (category == null)
            {
                return Result<IEnumerable<GetCategoryDto>>.NotFound();
            }

            return Result<IEnumerable<GetCategoryDto>>.Success(category);
        }
    }
}
