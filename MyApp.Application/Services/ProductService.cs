using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }
    }
}
