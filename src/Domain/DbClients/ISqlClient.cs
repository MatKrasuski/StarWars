using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Domain.DbClients
{
    public interface ISqlClient
    {
        Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null, CommandType commandType = CommandType.StoredProcedure);
        Task<int> ExecuteAsync(string query, object param = null, CommandType commandType = CommandType.StoredProcedure);
    }
}