# Como aplicar o pacote no projeto AssistenciaTecnicaAppNovo

Este pacote foi ajustado para o namespace:

```text
AssistenciaTecnicaAppNovo
```

## 1. Pare a execução do app

No Visual Studio, pare a execução antes de copiar os arquivos.

## 2. Copie as pastas para dentro do projeto

Copie/substitua estas pastas dentro da pasta onde está o arquivo `.csproj` do projeto:

```text
Controllers
Database
DTOs
Helpers
Models
Services
Views
Resources/Styles
```

Atenção: os arquivos `.xaml` redesenhados já estão dentro de `Views/`.

## 3. Copie também estes arquivos da raiz

Substitua no projeto:

```text
App.xaml
App.xaml.cs
MauiProgram.cs
```

O `App.xaml` já registra o arquivo:

```text
Resources/Styles/AppStyles.xaml
```

## 4. Pacotes NuGet necessários

Confirme se estão instalados no projeto:

```text
Dapper
MySqlConnector
MongoDB.Driver
sqlite-net-pcl
Microsoft.Extensions.Logging.Debug
```

## 5. Configure o MySQL

Abra:

```text
Database/DatabaseConfig.cs
```

Ajuste:

```csharp
public string MySqlServer { get; set; } = "localhost";
public string MySqlDatabase { get; set; } = "assistencia_tecnica";
public string MySqlUser { get; set; } = "root";
public string MySqlPassword { get; set; } = "SUA_SENHA_AQUI";
public uint MySqlPort { get; set; } = 3306;
```

## 6. Usuário de teste

No MySQL Workbench, rode:

```sql
USE assistencia_tecnica;

INSERT INTO tecnico (nome, email, senha_hash, ativo)
VALUES ('Administrador', 'admin@teste.com', SHA2('123456', 256), 1);
```

Login no app:

```text
E-mail: admin@teste.com
Senha: 123456
```

## 7. Recompile

No Visual Studio:

```text
Compilação > Limpar Solução
Compilação > Recompilar Solução
```

Depois execute o app.
