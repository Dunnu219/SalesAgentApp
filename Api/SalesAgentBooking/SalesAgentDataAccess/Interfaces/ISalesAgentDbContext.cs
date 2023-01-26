using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAgentDataAccess.Interfaces
{
    public interface ISalesAgentDbContext
    {
        public IDbConnection CreateConnection();
    }
}
