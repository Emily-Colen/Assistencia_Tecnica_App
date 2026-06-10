using AssistenciaTecnicaAppNovo.Database;
using AssistenciaTecnicaAppNovo.DTOs;
using AssistenciaTecnicaAppNovo.Models;
using Dapper;
using System.Text.Json;

namespace AssistenciaTecnicaAppNovo.Services;

public class JsonBackupService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public JsonBackupService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<string> ExportarAsync(string caminhoArquivo)
    {
        using var conn = _connectionFactory.CreateConnection();

        var backup = new BackupJson
        {
            Estados = (await conn.QueryAsync<Estado>("SELECT idestado AS IdEstado, nome AS Nome, sigla AS Sigla FROM estado;")).ToList(),
            Cidades = (await conn.QueryAsync<Cidade>("SELECT idcidade AS IdCidade, nome AS Nome, estado_idestado AS EstadoIdEstado FROM cidade;")).ToList(),
            Bairros = (await conn.QueryAsync<Bairro>("SELECT idbairro AS IdBairro, nome AS Nome, cidade_idcidade AS CidadeIdCidade FROM bairro;")).ToList(),
            Ruas = (await conn.QueryAsync<Rua>("SELECT idrua AS IdRua, nome AS Nome, bairro_idbairro AS BairroIdBairro FROM rua;")).ToList(),
            Clientes = (await conn.QueryAsync<Cliente>(@"
                SELECT idcliente AS IdCliente, nome AS Nome, cpf_cnpj AS CpfCnpj, email AS Email, telefone AS Telefone,
                       rua_idrua AS RuaIdRua, numero_endereco AS NumeroEndereco, complemento AS Complemento
                FROM cliente;")).ToList(),
            Tecnicos = (await conn.QueryAsync<Tecnico>("SELECT idtecnico AS IdTecnico, nome AS Nome, email AS Email, senha_hash AS SenhaHash, ativo AS Ativo FROM tecnico;")).ToList(),
            Especialidades = (await conn.QueryAsync<Especialidade>("SELECT idespecialidade AS IdEspecialidade, nome AS Nome, descricao AS Descricao FROM especialidade;")).ToList(),
            TecnicoEspecialidades = (await conn.QueryAsync<TecnicoEspecialidade>("SELECT tecnico_idtecnico AS TecnicoIdTecnico, especialidade_idespecialidade AS EspecialidadeIdEspecialidade FROM tecnico_has_especialidade;")).ToList(),
            Equipamentos = (await conn.QueryAsync<Equipamento>(@"
                SELECT idequipamento AS IdEquipamento, tipo AS Tipo, marca AS Marca, modelo AS Modelo, numero_serie AS NumeroSerie,
                       descricao AS Descricao, cliente_idcliente AS ClienteIdCliente
                FROM equipamento;")).ToList(),
            StatusOrdensServico = (await conn.QueryAsync<StatusOrdemServico>("SELECT idstatus AS IdStatus, nome AS Nome, descricao AS Descricao, finalizado AS Finalizado FROM status_ordem_servico;")).ToList(),
            OrdensServico = (await conn.QueryAsync<OrdemServico>(@"
                SELECT idordem_servico AS IdOrdemServico, defeito_relatado AS DefeitoRelatado, preco AS Preco,
                       data_abertura AS DataAbertura, data_fechamento AS DataFechamento, data_prevista AS DataPrevista,
                       descricao_manutencao AS DescricaoManutencao, status_idstatus AS StatusIdStatus,
                       tecnico_idtecnico AS TecnicoIdTecnico, equipamento_idequipamento AS EquipamentoIdEquipamento
                FROM ordem_servico;")).ToList(),
            Pecas = (await conn.QueryAsync<Peca>(@"
                SELECT idpeca AS IdPeca, codigo AS Codigo, nome AS Nome, descricao AS Descricao, unidade AS Unidade,
                       estoque_atual AS EstoqueAtual, estoque_minimo AS EstoqueMinimo, valor_custo AS ValorCusto, valor_venda AS ValorVenda
                FROM peca;")).ToList(),
            PecasOrdensServico = (await conn.QueryAsync<PecaOrdemServico>(@"
                SELECT peca_idpeca AS PecaIdPeca, ordem_servico_idordem_servico AS OrdemServicoIdOrdemServico,
                       quantidade AS Quantidade, subtotal AS Subtotal, valor_unitario AS ValorUnitario
                FROM peca_has_ordem_servico;")).ToList(),
            MovimentacoesEstoque = (await conn.QueryAsync<MovimentacaoEstoque>(@"
                SELECT idmovimentacao_estoque AS IdMovimentacaoEstoque, tipo_movimentacao AS TipoMovimentacao,
                       quantidade AS Quantidade, valor_unitario AS ValorUnitario, data_movimentacao AS DataMovimentacao,
                       observacao AS Observacao, peca_idpeca AS PecaIdPeca,
                       ordem_servico_idordem_servico AS OrdemServicoIdOrdemServico, tecnico_idtecnico AS TecnicoIdTecnico
                FROM movimentacao_estoque;")).ToList(),
            Garantias = (await conn.QueryAsync<Garantia>(@"
                SELECT idgarantia AS IdGarantia, data_inicio AS DataInicio, data_fim AS DataFim, termo AS Termo,
                       ativo AS Ativo, ordem_servico_idordem_servico AS OrdemServicoIdOrdemServico
                FROM garantia;")).ToList()
        };

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(backup, options);
        await File.WriteAllTextAsync(caminhoArquivo, json);
        return caminhoArquivo;
    }

    public async Task<BackupJson> LerArquivoAsync(string caminhoArquivo)
    {
        if (!File.Exists(caminhoArquivo))
            throw new FileNotFoundException("Arquivo JSON não encontrado.", caminhoArquivo);

        if (!caminhoArquivo.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("O arquivo selecionado precisa ser .json.");

        string json = await File.ReadAllTextAsync(caminhoArquivo);
        var backup = JsonSerializer.Deserialize<BackupJson>(json);

        if (backup == null)
            throw new InvalidOperationException("Arquivo JSON inválido.");

        ValidarBackup(backup);
        return backup;
    }

    public void ValidarBackup(BackupJson backup)
    {
        foreach (var cliente in backup.Clientes)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
                throw new InvalidOperationException("Existe cliente sem nome no JSON.");
            if (string.IsNullOrWhiteSpace(cliente.CpfCnpj))
                throw new InvalidOperationException("Existe cliente sem CPF/CNPJ no JSON.");
        }

        foreach (var peca in backup.Pecas)
        {
            if (string.IsNullOrWhiteSpace(peca.Nome))
                throw new InvalidOperationException("Existe peça sem nome no JSON.");
            if (peca.EstoqueAtual < 0)
                throw new InvalidOperationException("Existe peça com estoque negativo no JSON.");
        }

        foreach (var os in backup.OrdensServico)
        {
            if (string.IsNullOrWhiteSpace(os.DefeitoRelatado))
                throw new InvalidOperationException("Existe ordem de serviço sem defeito relatado no JSON.");
            if (os.Preco < 0)
                throw new InvalidOperationException("Existe ordem de serviço com preço negativo no JSON.");
        }
    }
}
