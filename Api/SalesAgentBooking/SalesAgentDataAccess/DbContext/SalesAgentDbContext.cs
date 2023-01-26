using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SalesAgentDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAgentDataAccess.DbContext
{
    public class SalesAgentDbContext : ISalesAgentDbContext
    {
        private readonly IConfiguration _configuration;

        public SalesAgentDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
    }
}
