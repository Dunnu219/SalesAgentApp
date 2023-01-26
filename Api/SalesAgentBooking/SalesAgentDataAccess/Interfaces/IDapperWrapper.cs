using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAgentDataAccess.Interfaces
{
    public interface IDapperWrapper
    {
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, DynamicParameters dynamicParameters, CommandType commandType);
    }
}
