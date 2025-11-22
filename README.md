# ZenFlow - Diário de Bem-Estar

## ⚠️ O Desafio

O futuro do trabalho impõe grande pressão sobre a saúde mental dos colaboradores. Empresas não conseguem monitorar o estresse e prevenir o burnout de forma eficaz e ética, pois não há um canal anônimo e seguro para os trabalhadores expressarem seu estado emocional.

## ✨ A Solução

O ZenFlow é uma plataforma que usa tecnologia para monitorar o bem-estar coletivo da organização. Fornece dados empáticos para a gestão tomar decisões baseadas em evidências, criando um ambiente de trabalho mais inclusivo e sustentável.

---

## 📋 Índice

- [Tecnologias](#-tecnologias)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Pré-requisitos](#-pré-requisitos)
- [Configuração](#-configuração)
- [Executando o Projeto](#-executando-o-projeto)
- [Migrations](#-migrations)
- [API Endpoints](#-api-endpoints)
- [Documentação Swagger](#-documentação-swagger)
- [Arquitetura](#-arquitetura)

---

## 🛠️ Tecnologias

- **.NET 8.0** - Framework principal
- **Entity Framework Core 8.0.22** - ORM para acesso a dados
- **Oracle Database** - Banco de dados relacional
- **Oracle.EntityFrameworkCore 8.23.26000** - Provider Oracle para EF Core
- **Swagger/OpenAPI** - Documentação da API
- **AutoMapper 12.0.1** - Mapeamento de objetos
- **FluentValidation 12.1.0** - Validação de dados

---

## 📁 Estrutura do Projeto

```
gs-ZenFlow/
├── Application/              # Camada de Aplicação
│   ├── DTOs/                 # Data Transfer Objects
│   │   ├── Registro/
│   │   └── Usuario/
│   └── UseCase/              # Casos de uso da aplicação
│
├── Controllers/              # Controllers da API
│   ├── RegistroController.cs
│   └── UsuarioController.cs
│
├── Domain/                   # Camada de Domínio
│   ├── Entities/             # Entidades do domínio
│   │   ├── Registro.cs
│   │   └── Usuario.cs
│   └── Repositories/         # Interfaces dos repositórios
│
├── Infrastructure/           # Camada de Infraestrutura
│   ├── Data/                 # DbContext
│   ├── Mappings/             # Configurações do EF Core
│   ├── Migrations/           # Migrations do banco de dados
│   └── Repositories/         # Implementações dos repositórios
│
├── Properties/               # Configurações do projeto
├── Program.cs                # Ponto de entrada da aplicação
└── appsettings.json          # Configurações da aplicação
```

---

## 📦 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Oracle Database](https://www.oracle.com/database/) (ou acesso a um servidor Oracle)
- Uma IDE de sua preferência (Visual Studio, Rider, VS Code)

---

## ⚙️ Configuração

### 1. Clone o repositório

```bash
git clone <url-do-repositorio>
cd gs-ZenFlow
```

### 2. Configure a String de Conexão

Edite o arquivo `gs-ZenFlow/appsettings.json` e configure a string de conexão do Oracle:

```json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=seu_usuario;Password=sua_senha;Data Source=servidor:porta/servico;"
  }
}
```

**Exemplo:**
```json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=rm560066;Password=100605;Data Source=oracle.fiap.com.br:1521/orcl;"
  }
}
```

### 3. Restaure as Dependências

```bash
cd gs-ZenFlow
dotnet restore
```

---

## 🚀 Executando o Projeto

### 1. Aplique as Migrations

Primeiro, certifique-se de que o banco de dados está configurado e acessível. Em seguida, execute:

```bash
dotnet ef database update --project gs-ZenFlow/gs-ZenFlow.csproj
```

Ou, se estiver na pasta do projeto:

```bash
cd gs-ZenFlow
dotnet ef database update
```

### 2. Execute a Aplicação

```bash
dotnet run --project gs-ZenFlow/gs-ZenFlow.csproj
```

Ou, se estiver na pasta do projeto:

```bash
cd gs-ZenFlow
dotnet run
```

A aplicação estará disponível em:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`
- **Swagger**: `http://localhost:5000` (em ambiente de desenvolvimento)

---

## 🔄 Migrations

### Criar uma Nova Migration

```bash
dotnet ef migrations add NomeDaMigration --project gs-ZenFlow/gs-ZenFlow.csproj
```

### Aplicar Migrations ao Banco de Dados

```bash
dotnet ef database update --project gs-ZenFlow/gs-ZenFlow.csproj
```

### Remover a Última Migration (antes de aplicar)

```bash
dotnet ef migrations remove --project gs-ZenFlow/gs-ZenFlow.csproj
```

### Verificar Status das Migrations

```bash
dotnet ef migrations list --project gs-ZenFlow/gs-ZenFlow.csproj
```

---

## 📡 API Endpoints

### 👤 Usuários (`/api/Usuario`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `POST` | `/api/Usuario` | Criar novo usuário |
| `POST` | `/api/Usuario/login` | Autenticar usuário (login) |
| `GET` | `/api/Usuario` | Listar todos os usuários ativos |
| `GET` | `/api/Usuario/{id}` | Buscar usuário por ID |
| `PATCH` | `/api/Usuario/{id}/email` | Alterar email do usuário |
| `PATCH` | `/api/Usuario/{id}/senha` | Alterar senha do usuário |
| `DELETE` | `/api/Usuario/{id}` | Desativar usuário (remoção lógica) |

### 📝 Registros de Estresse (`/api/Registro`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `POST` | `/api/Registro/usuario/{usuarioId}` | Criar novo registro de estresse |
| `GET` | `/api/Registro` | Listar todos os registros ativos |
| `GET` | `/api/Registro/{id}` | Buscar registro por ID |
| `GET` | `/api/Registro/usuario/{usuarioId}` | Listar registros de um usuário |
| `DELETE` | `/api/Registro/{id}/usuario/{usuarioId}` | Desativar registro (remoção lógica) |

### 📋 Exemplos de Requisições

#### Criar Usuário
```http
POST /api/Usuario
Content-Type: application/json

{
  "nomeCompleto": "João Silva",
  "email": "joao.silva@email.com",
  "senha": "senha123",
  "dataNascimento": "1990-01-15",
  "cpf": "12345678901",
  "isGestor": false
}
```

#### Login
```http
POST /api/Usuario/login
Content-Type: application/json

{
  "email": "joao.silva@email.com",
  "senha": "senha123"
}
```

#### Criar Registro de Estresse
```http
POST /api/Registro/usuario/1
Content-Type: application/json

{
  "nivelEstresse": 3,
  "observacoes": "Dia muito corrido, muitas reuniões"
}
```

---

## 📚 Documentação Swagger

A documentação interativa da API está disponível através do Swagger quando a aplicação está rodando em ambiente de desenvolvimento.

Acesse: `http://localhost:5000` (ou a porta configurada)

O Swagger permite:
- Visualizar todos os endpoints disponíveis
- Testar as requisições diretamente no navegador
- Ver os modelos de dados (DTOs)
- Verificar os códigos de resposta esperados

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Arquitetura em Camadas (Layered Architecture)** e **Clean Architecture**:

### Camadas

1. **Domain** - Entidades e interfaces de repositórios
   - Contém as regras de negócio puras
   - Não depende de outras camadas

2. **Application** - Casos de uso e DTOs
   - Orquestra a lógica de negócio
   - Define contratos de entrada e saída

3. **Infrastructure** - Implementações técnicas
   - Acesso a dados (Entity Framework)
   - Implementações de repositórios
   - Configurações de mapeamento

4. **Controllers** - Camada de apresentação
   - Endpoints HTTP
   - Validação de entrada
   - Tratamento de erros

### Padrões Utilizados

- **Repository Pattern** - Abstração do acesso a dados
- **Use Case Pattern** - Encapsulamento da lógica de negócio
- **DTO Pattern** - Transferência de dados entre camadas
- **Dependency Injection** - Inversão de controle

---

## 🗄️ Banco de Dados

### Tabelas

#### `T_GS_USUARIO`
Armazena informações dos usuários do sistema.

**Campos principais:**
- `Id` (PK, Identity)
- `NomeCompleto` (NVARCHAR2(100))
- `Email` (NVARCHAR2(100), Unique)
- `Senha` (NVARCHAR2(50))
- `Cpf` (NVARCHAR2(11), Unique)
- `DataNascimento` (TIMESTAMP)
- `IsGestor` (NUMBER - 0 ou 1)
- `DataCriacao` (TIMESTAMP)
- `DataAtualizacao` (TIMESTAMP, nullable)
- `Ativo` (NUMBER - 0 ou 1)

#### `T_GS_REGISTRO`
Armazena os registros de estresse dos usuários.

**Campos principais:**
- `Id` (PK, Identity)
- `UsuarioId` (FK para T_GS_USUARIO)
- `NivelEstresse` (NUMBER - 1 a 5)
- `Observacoes` (NVARCHAR2(500), nullable)
- `Data` (TIMESTAMP)
- `DataCriacao` (TIMESTAMP)
- `DataAtualizacao` (TIMESTAMP, nullable)
- `Ativo` (NUMBER - 0 ou 1)

### Relacionamentos

- Um `Usuario` pode ter muitos `Registro`
- Um `Registro` pertence a um `Usuario`
- Relacionamento com `DeleteBehavior.Restrict` (não permite deletar usuário com registros)

---

## 👤 Desenvolvedor

**Amanda Galdino**
- Email: RM560066@fiap.com.br
- RM: 560066

---

## 📝 Licença

Este projeto foi desenvolvido como parte do Global Solution da FIAP.

---

## 🔍 Observações Importantes

- ⚠️ **Remoção Lógica**: Tanto usuários quanto registros são desativados (remoção lógica), não deletados fisicamente do banco
- 🔐 **Segurança**: As senhas devem ser tratadas com hash em produção (implementação futura)
- 📊 **Nível de Estresse**: Valores aceitos de 1 a 5 (1 = muito baixo, 5 = muito alto)
- 🔑 **Índices Únicos**: Email e CPF são únicos no sistema
- 🎯 **Soft Delete**: O campo `Ativo` controla a visibilidade dos registros

---

## 🐛 Troubleshooting

### Erro ao conectar ao Oracle
- Verifique se a string de conexão está correta
- Confirme se o servidor Oracle está acessível
- Verifique as credenciais de acesso

### Erro ao executar migrations
- Certifique-se de que o banco de dados existe
- Verifique se o usuário tem permissões para criar tabelas
- Confirme que o Oracle.EntityFrameworkCore está instalado

### Swagger não aparece
- Verifique se está em ambiente de desenvolvimento
- Confirme que `app.Environment.IsDevelopment()` retorna `true`

---

**Desenvolvido com ❤️ para melhorar o bem-estar no ambiente de trabalho**

