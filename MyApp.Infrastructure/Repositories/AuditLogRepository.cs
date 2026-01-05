using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

namespace MyApp.Infrastructure.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly IConnectionFactory _factory;

        public AuditLogRepository(IConnectionFactory factory) {
            _factory = factory;
        }
    }
}
