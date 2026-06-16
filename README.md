# Projeto 2° Bim - Programação III

## Aplicativo multiplataforma C# com .NET MAUI - Gestão para Assistência Técnica

O projeto visa aplicar conceitos de **arquitetura de software**, **persistência de dados** e utilização de **múltiplos bancos de dados** em uma aplicação multiplataforma desenvolvida com .NET MAUI.

**Tema:** Sistema de gerenciamento para Assistência Técnica 🛠️

- **Linguagem:** C#
- **Framework:** .NET MAUI
- **Banco relacional:** MySQL
- **Banco local:** SQLite
- **Banco NoSQL:** MongoDB
- **Arquitetura:** MVC com camada Service

---

## 📌 Sobre o projeto

O **Assistência Técnica** foi criado para organizar o fluxo básico de uma assistência técnica, permitindo o controle de clientes, equipamentos, técnicos, ordens de serviço, peças, estoque, anexos e backup de dados.

O projeto foi estruturado com foco acadêmico, utilizando separação de responsabilidades por camadas, regras de negócio centralizadas em Services e acesso a dados separado em DAOs.

---

## 🎯 Objetivo

O objetivo do sistema é facilitar o gerenciamento de uma assistência técnica, oferecendo uma aplicação com:

- Cadastro e gerenciamento de clientes;
- Controle de peças e estoque;
- Abertura e acompanhamento de ordens de serviço;
- Controle de técnicos e status de atendimento;
- Backup dos dados em JSON;
- Uso de MySQL, MongoDB e SQLite;
- Indicador gráfico simples na tela principal.

---

## 🧱 Arquitetura do projeto

O projeto segue uma organização baseada em **MVC**, com camadas adicionais para melhorar a manutenção do código.

```text
AssistenciaTecnicaAppNovo/
│
├── Controllers/        # Controladores da aplicação
├── DAO/                # Acesso aos bancos de dados
├── Database/           # Configurações e factories de conexão
├── DTOs/               # Objetos de transferência de dados
├── Helpers/            # Classes auxiliares de validação e senha
├── Interfaces/         # Contratos das camadas
├── Models/             # Entidades do sistema
├── Services/           # Regras de negócio
├── Views/              # Telas XAML do app
├── MauiProgram.cs      # Injeção de dependência e configuração do app
└── App.xaml.cs         # Inicialização da aplicação
```

---

## 🖥️ Telas principais

O sistema possui telas para:

- **Login** de técnico;
- **Menu principal**;
- **Clientes**;
- **Peças / Estoque**;
- **Ordens de Serviço**;
- **Exportação e importação JSON**.

A tela principal também apresenta um gráfico simples com a quantidade de ordens de serviço agrupadas por status.

---

## 📊 Gráfico na tela principal

Foi implementado um gráfico simples de barras no dashboard principal para visualizar as ordens de serviço por status.

Exemplo de indicador:

```text
Ordens por status

Aberta         ██████████       2
Em andamento  ██████████████    3
Finalizada     ███████          1
```

Esse gráfico utiliza componentes nativos do MAUI, como:

- `Border`;
- `Grid`;
- `Label`;
- `Button`;
- `BoxView`.

Assim, não é necessário instalar biblioteca externa de gráficos.

---

## 🗄️ Banco de dados MySQL

O **MySQL** é o banco principal do sistema e armazena os dados relacionais da aplicação.

Principais entidades:

- `cliente`;
- `tecnico`;
- `equipamento`;
- `ordem_servico`;
- `status_ordem_servico`;
- `peca`;
- `movimentacao_estoque`;
- `garantia`;
- `tecnico_has_especialidade`;
- `peca_has_ordem_servico`.


## 🍃 MongoDB

O **MongoDB** é utilizado para armazenar anexos e documentos vinculados às ordens de serviço.

A coleção utilizada é:

```text
anexos_ordem_servico
```

A estrutura permite associar documentos a uma OS sem sobrecarregar o banco relacional.

Campos principais do anexo:

- `Id`;
- `OrdemServicoId`;
- `TecnicoId`;
- `NomeArquivo`;
- `TipoArquivo`;
- `CaminhoArquivo`;
- `Descricao`;
- `DataUpload`.

---

## 💾 SQLite

O **SQLite** é utilizado como banco local do aplicativo.

Ele cria o arquivo:

```text
assistencia_local.db3
```

O cache local armazena informações simplificadas de peças, como:

- ID da peça;
- Nome;
- Estoque atual.

Essa estrutura é controlada pelas classes:

```text
SQLiteConnectionFactory
LocalCacheDao
LocalCacheService
```

---

## 📤 Exportação e importação JSON

O sistema possui exportação de backup em formato **JSON**.

Na tela principal, o usuário pode clicar em:

- **Exportar JSON**: gera um arquivo de backup com os dados principais do sistema;
- **Importar JSON**: permite selecionar um arquivo `.json` e validar sua estrutura.

O arquivo exportado segue o padrão:

```text
backup_assistencia_yyyyMMdd_HHmmss.json
```

A lógica está centralizada em:

```text
JsonBackupService
BackupDao
BackupJson
```

---

## ⚙️ Tecnologias utilizadas

- **C#**;
- **.NET MAUI**;
- **XAML**;
- **MySQL**;
- **MongoDB**;
- **SQLite**;
- **Dapper**;
- **MySqlConnector**;
- **MongoDB.Driver**;
- **sqlite-net-pcl**;
- **System.Text.Json**;
- **Injeção de dependência com Microsoft.Extensions.DependencyInjection**.

---

## 📦 Pacotes NuGet principais

```xml
<PackageReference Include="Dapper" Version="2.1.79" />
<PackageReference Include="MongoDB.Driver" Version="3.9.0" />
<PackageReference Include="MySqlConnector" Version="2.5.0" />
<PackageReference Include="sqlite-net-pcl" Version="1.9.172" />
<PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="10.0.0" />
```

---

## ▶️ Como executar o projeto

1. Clone o repositório:

```bash
git clone https://github.com/SEU_USUARIO/AssistenciaTecnicaAppNovo.git
```

2. Abra o projeto no **Visual Studio**.

3. Restaure os pacotes NuGet.

4. Configure o banco em:

```text
Database/DatabaseConfig.cs
```

5. Verifique se o MySQL está ativo e se o banco `assistencia_tecnica` foi criado.

6. Se for utilizar anexos, verifique se o MongoDB está em execução.

7. Execute usando:

```text
Windows Machine
```

---

## 🔐 Login

O sistema utiliza login por técnico.

A autenticação valida:

- E-mail informado;
- Senha informada;
- Técnico existente;
- Usuário ativo;
- Hash da senha.

A lógica de login está em:

```text
AuthController
LoginService
TecnicoDao
PasswordHelper
```

---

## ✅ Funcionalidades implementadas

- Login de técnico;
- Cadastro de clientes;
- Cadastro e controle de peças;
- Controle de ordens de serviço;
- Controle de estoque;
- Uso de Services para regras de negócio;
- Uso de DAOs para acesso a dados;
- Banco principal em MySQL;
- MongoDB para anexos;
- SQLite para cache local;
- Exportação e validação de JSON;
- Gráfico simples na tela principal;
- Estrutura com Models, Views, Controllers, Services, DAO e Interfaces.

---

## 🧠 Organização das regras de negócio

As regras de negócio ficam concentradas na camada `Services`.

Exemplos:

- `ClienteService`: valida nome, CPF/CNPJ, e-mail, telefone e endereço;
- `PecaService`: valida nome, estoque e valores;
- `OrdemServicoService`: valida equipamento, status, defeito relatado e preço;
- `EstoqueService`: valida entrada e saída de peças;
- `JsonBackupService`: valida estrutura do arquivo JSON;
- `LoginService`: valida autenticação do técnico.

---

## 👨‍💻 Autor

Projeto desenvolvido para fins acadêmicos na disciplina de Programação III.

**Aluna:** Emily Colen Honorio
**Instituição:** UNIVAP — Faculdade de Engenharia, Arquitetura e Urbanismo  
**Curso:** Engenharia da Computação

