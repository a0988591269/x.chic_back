using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Contexts
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
