using MediatR;
using MyApp.Application.Commons.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Products.Queries.GetProductById
{
    public record GetProductDetailQuery : IRequest<Result<GetProductDetailDto>>
    {
        public long Id { get; set; }
    }
}