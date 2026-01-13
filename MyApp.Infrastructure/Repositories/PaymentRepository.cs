using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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