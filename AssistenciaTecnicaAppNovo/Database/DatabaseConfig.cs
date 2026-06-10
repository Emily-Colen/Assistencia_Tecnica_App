namespace AssistenciaTecnicaAppNovo.Database;

public class DatabaseConfig
{
    //acesso ao MySLQ
    public string MySqlServer { get; set; } = "localhost";
    public string MySqlDatabase { get; set; } = "assistencia_tecnica";
    public string MySqlUser { get; set; } = "root";
    public string MySqlPassword { get; set; } = "";
    public uint MySqlPort { get; set; } = 3306;

    // SQLite local do app.
    public string SQLiteDatabasePath => Path.Combine(FileSystem.AppDataDirectory, "assistencia_local.db3");

    // MongoDB para anexos/documentos.
    public string MongoConnectionString { get; set; } = "mongodb://localhost:27017";
    public string MongoDatabaseName { get; set; } = "assistencia_tecnica_docs";
}
