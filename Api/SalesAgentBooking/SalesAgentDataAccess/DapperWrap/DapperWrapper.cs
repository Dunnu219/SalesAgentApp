using Dapper;
using SalesAgentDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAgentDataAccess.DapperWrap
{
    public class DapperWrapper : IDapperWrapper
    {
        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, DynamicParameters dynamicParameters, CommandType commandType)
        {
            return connection.QueryAsync<T>(sql, dynamicParameters, commandType: commandType);
        }
    }
}
