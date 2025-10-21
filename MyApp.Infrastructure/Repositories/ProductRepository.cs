using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IDapperConnectionFactory factory) : base(factory, DatabaseKey.Default) { }
    }
}
