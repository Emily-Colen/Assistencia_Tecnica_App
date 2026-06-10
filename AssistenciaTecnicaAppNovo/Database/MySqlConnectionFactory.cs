using MySqlConnector;
using System.Data;

namespace AssistenciaTecnicaAppNovo.Database;

public class MySqlConnectionFactory
{
    private readonly DatabaseConfig _config;

    public MySqlConnectionFactory(DatabaseConfig config)
    {
        _config = config;
    }

    public IDbConnection CreateConnection()
    {
        var builder = new MySqlConnectionStringBuilder
        {
            Server = _config.MySqlServer,
            Database = _config.MySqlDatabase,
            UserID = _config.MySqlUser,
            Password = _config.MySqlPassword,
            Port = _config.MySqlPort,
            SslMode = MySqlSslMode.Disabled,
            AllowUserVariables = true
        };

        return new MySqlConnection(builder.ConnectionString);
    }
}
