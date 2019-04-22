using System.Data.SqlClient;
using System.Threading.Tasks;
using Bussiness.Models;
using Domain.DbClients;
using Domain.Dtos;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    class Class1
    {
        [Test]
        public async Task Test()
        {
            var connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=StarWars;Trusted_Connection=True;");

            var client = new SqlCLient(connection);

            var result = await client.QueryAsync<CharacterDto>("[Characters].[GetCharacters]", null);
        }
    }
}
