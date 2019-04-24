using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace IntegrationTests
{
    public class TestBase
    {
        protected readonly IDbConnection SqlConnection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=StarWars;Trusted_Connection=True;");

        internal async Task ClearCharactersTable()
        {
            await SqlConnection.ExecuteAsync("truncate table [Characters].[StarWarsCharacters]", commandType: CommandType.Text);
        }
    }
}
