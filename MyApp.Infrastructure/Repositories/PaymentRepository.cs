using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IConnectionFactory _factory;

        public PaymentRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}