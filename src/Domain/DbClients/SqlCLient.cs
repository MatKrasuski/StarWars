using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Domain.DbClients
{
    public class SqlCLient : ISqlClient
    {
        private readonly IDbConnection _dbConnection;

        public SqlCLient(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null, CommandType commandType = CommandType.StoredProcedure)
        {
            return (await _dbConnection.QueryAsync<T>(query, param, null, null, commandType)).ToList();
        }

        public async Task<int> ExecuteAsync(string query, object param, CommandType commandType = CommandType.StoredProcedure)
        {
            return await _dbConnection.ExecuteAsync(query, param, null, null, commandType);
        }
    }

}
