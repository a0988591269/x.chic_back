using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

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
