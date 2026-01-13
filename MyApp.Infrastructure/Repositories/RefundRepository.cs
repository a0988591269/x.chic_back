using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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