using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Persistence.Contexts
{
    /// <summary>
    /// 介面
    /// </summary>
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
        IDbConnection GetConnection_Test();
    }
}
