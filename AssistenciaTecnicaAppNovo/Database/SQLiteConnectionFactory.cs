using SQLite;

namespace AssistenciaTecnicaAppNovo.Database;

public class SQLiteConnectionFactory
{
    private readonly DatabaseConfig _config;

    public SQLiteConnectionFactory(DatabaseConfig config)
    {
        _config = config;
    }

    public SQLiteAsyncConnection CreateConnection()
    {
        return new SQLiteAsyncConnection(_config.SQLiteDatabasePath);
    }
}
