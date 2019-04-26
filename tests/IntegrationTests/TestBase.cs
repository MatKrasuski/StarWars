using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace IntegrationTests
{
    public class TestBase
    {
        protected readonly IDbConnection DbConnection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=StarWars;Trusted_Connection=True;");

        internal async Task ClearCharactersTable()
        {
            await DbConnection.ExecuteAsync("truncate table [Characters].[StarWarsCharacters]", commandType: CommandType.Text);
            await DbConnection.ExecuteAsync("truncate table [Characters].[Friends]", commandType: CommandType.Text);
            await DbConnection.ExecuteAsync("truncate table [Characters].[Episodes]", commandType: CommandType.Text);
        }
    }
}
