using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class RefundRepository : IRefundRepository
    {
        private readonly IConnectionFactory _factory;

        public RefundRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}