using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssistenciaTecnicaAppNovo.DTOs;

public class AnexoMongo
{
    [BsonId]
    public ObjectId Id { get; set; }

    public int OrdemServicoId { get; set; }
    public int TecnicoId { get; set; }
    public string NomeArquivo { get; set; } = string.Empty;
    public string TipoArquivo { get; set; } = string.Empty;
    public string CaminhoArquivo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime DataUpload { get; set; } = DateTime.Now;
}
