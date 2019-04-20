using MongoDB.Driver;

namespace IntegrationTests
{
    public class TestBase
    {
        protected string DbName = "StarWarsTest";
        protected IMongoClient MongoClient = new MongoClient("mongodb://localhost:27017");

        protected IMongoDatabase Db => MongoClient.GetDatabase(DbName);
    }
}
