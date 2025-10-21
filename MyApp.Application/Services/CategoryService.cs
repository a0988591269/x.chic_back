using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Repositories;

namespace MyApp.Application.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _repo;

        public CategoryService(CategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
            => await _repo.GetAllAsync();
    }
}
