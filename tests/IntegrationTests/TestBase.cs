using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.DbClients;
using MongoDB.Driver;

namespace IntegrationTests
{
    public class TestBase
    {
        protected string DbName = "StarWarsTest";
        protected IMongoClient MongoClient = new MongoClient("mongodb://localhost:27017");
        protected readonly IDbConnection SqlConnection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=StarWarsTest;Trusted_Connection=True;");

        protected IMongoDatabase Db => MongoClient.GetDatabase(DbName);

        protected ISqlClient SqlClient => new SqlCLient(SqlConnection);

        internal async Task ClearCharactersTable()
        {
            await SqlClient.ExecuteAsync("truncate table [Characters].[StarWarsCharacters]", null, CommandType.Text);
        }
    }
}
