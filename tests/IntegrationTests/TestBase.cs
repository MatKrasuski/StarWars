using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.DbClients;

namespace IntegrationTests
{
    public class TestBase
    {
        protected readonly IDbConnection SqlConnection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=StarWarsTest;Trusted_Connection=True;");

        protected ISqlClient SqlClient => new SqlCLient(SqlConnection);

        internal async Task ClearCharactersTable()
        {
            await SqlClient.ExecuteAsync("truncate table [Characters].[StarWarsCharacters]", null, CommandType.Text);
        }
    }
}
