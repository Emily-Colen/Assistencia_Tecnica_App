using MongoDB.Driver;

namespace AssistenciaTecnicaAppNovo.Database;

public class MongoConnectionFactory
{
    private readonly DatabaseConfig _config;

    public MongoConnectionFactory(DatabaseConfig config)
    {
        _config = config;
    }

    public IMongoDatabase GetDatabase()
    {
        var client = new MongoClient(_config.MongoConnectionString);
        return client.GetDatabase(_config.MongoDatabaseName);
    }
}
