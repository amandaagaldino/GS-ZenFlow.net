# ZenFlow - Diário de Bem-Estar

## ⚠️ O Desafio

O futuro do trabalho impõe grande pressão sobre a saúde mental dos colaboradores. Empresas não conseguem monitorar o estresse e prevenir o burnout de forma eficaz e ética, pois não há um canal anônimo e seguro para os trabalhadores expressarem seu estado emocional.

## ✨ A Solução

O ZenFlow é uma plataforma que usa tecnologia para monitorar o bem-estar coletivo da organização. Fornece dados empáticos para a gestão tomar decisões baseadas em evidências, criando um ambiente de trabalho mais inclusivo e sustentável.

A solução ZenFlow está diretamente alinhada com os Objetivos de Desenvolvimento Sustentável (ODS) da ONU, especialmente:

### 🎯 ODS 8: Trabalho Decente e Crescimento Econômico

Ao focar no bem-estar e na saúde mental, a solução contribui para a promoção de ambientes de trabalho seguros e saudáveis, garantindo um trabalho digno para todos. O monitoramento contínuo do estresse permite que as organizações identifiquem e resolvam problemas antes que impactem significativamente a qualidade de vida dos colaboradores.

### 📚 ODS 4: Educação de Qualidade

A partir dos dados coletados, a empresa pode identificar tendências e investir em programas de capacitação e workshops focados em gestão de estresse e inteligência emocional, promovendo o aprendizado ao longo da vida. O ZenFlow fornece insights valiosos que orientam o desenvolvimento de programas educacionais personalizados.

### ⚖️ ODS 10: Redução das Desigualdades

O anonimato garante que todos os colaboradores, independentemente do cargo ou vulnerabilidade, possam expressar suas preocupações de forma segura, criando um sistema de apoio mais inclusivo. Isso elimina barreiras hierárquicas e promove equidade no acesso a recursos de bem-estar.

## 📖 Sobre o Projeto

O ZenFlow é composto por duas aplicações complementares:

1. **API REST (gs-ZenFlow)**: Fornece endpoints RESTful para integração com outros sistemas, com documentação Swagger completa e tratamento padronizado de erros.

2. **Aplicação Web MVC (Web-gs-ZenFlow)**: Interface web amigável para gerenciamento de usuários e registros de estresse, utilizando Razor Pages e Bootstrap.

Ambas as aplicações compartilham a mesma arquitetura em camadas, banco de dados Oracle e lógica de negócio, garantindo consistência e reutilização de código.

---

## 📋 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Tecnologias](#-tecnologias)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Pré-requisitos](#-pré-requisitos)
- [Configuração](#-configuração)
- [Executando o Projeto](#-executando-o-projeto)
- [Migrations](#-migrations)
- [API Endpoints](#-api-endpoints)
- [Aplicação Web MVC](#-aplicação-web-mvc)
- [Documentação Swagger](#-documentação-swagger)
- [Arquitetura](#-arquitetura)
- [Banco de Dados](#-banco-de-dados)
- [Desenvolvedor](#-desenvolvedor)
- [Licença](#-licença)
- [Observações Importantes](#-observações-importantes)
- [Troubleshooting](#-troubleshooting)

---

## 🛠️ Tecnologias

### Framework e Linguagem
- **.NET 8.0** - Framework principal
- **C#** - Linguagem de programação

### Banco de Dados
- **Oracle Database** - Banco de dados relacional
- **Entity Framework Core 8.0.22** - ORM para acesso a dados
- **Oracle.EntityFrameworkCore 8.23.26000** - Provider Oracle para EF Core
- **Oracle.ManagedDataAccess.Core 23.26.0** - Driver Oracle gerenciado

### API (gs-ZenFlow)
- **Swagger/OpenAPI 6.8.1** - Documentação interativa da API
- **Swashbuckle.AspNetCore** - Geração de documentação Swagger
- **Swashbuckle.AspNetCore.Annotations** - Anotações para Swagger
- **Microsoft.AspNetCore.OpenApi 8.0.22** - Suporte OpenAPI

### Aplicação Web (Web-gs-ZenFlow)
- **ASP.NET Core MVC** - Framework web MVC
- **Razor Pages** - Engine de views
- **Bootstrap** - Framework CSS
- **jQuery** - Biblioteca JavaScript
- **jQuery Validation** - Validação client-side

### Bibliotecas Compartilhadas
- **AutoMapper 12.0.1** - Mapeamento de objetos
- **AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.1** - Integração AutoMapper com DI
- **FluentValidation 12.1.0** - Validação de dados
- **FluentValidation.AspNetCore 11.3.1** - Integração FluentValidation com ASP.NET Core

---

## 📁 Estrutura do Projeto

O projeto é composto por duas aplicações que compartilham a mesma arquitetura e banco de dados:

### 1. **gs-ZenFlow** - API Web API
Aplicação REST API com documentação Swagger.

### 2. **Web-gs-ZenFlow** - Aplicação Web MVC
Aplicação web com interface Razor Pages para gerenciamento de usuários e registros.

```
gs-ZenFlow/
├── gs-ZenFlow/                    # Projeto API Web API
│   ├── Application/              # Camada de Aplicação
│   │   ├── DTOs/                   # Data Transfer Objects
│   │   │   ├── Registro/
│   │   │   └── Usuario/
│   │   └── UseCase/                # Casos de uso da aplicação
│   │
│   ├── Controllers/                # Controllers da API
│   │   ├── RegistroController.cs
│   │   └── UsuarioController.cs
│   │
│   ├── Domain/                     # Camada de Domínio
│   │   ├── Entities/               # Entidades do domínio
│   │   │   ├── Registro.cs
│   │   │   └── Usuario.cs
│   │   └── Repositories/           # Interfaces dos repositórios
│   │
│   ├── Infrastructure/             # Camada de Infraestrutura
│   │   ├── Data/                   # DbContext
│   │   ├── Mappings/               # Configurações do EF Core
│   │   ├── Migrations/             # Migrations do banco de dados
│   │   └── Repositories/           # Implementações dos repositórios
│   │
│   ├── Utils/                      # Utilitários (Swagger)
│   ├── Program.cs                  # Ponto de entrada da API
│   └── appsettings.json            # Configurações da API
│
└── Web-gs-ZenFlow/                 # Projeto Web MVC
    ├── Application/                # Camada de Aplicação
    │   ├── DTOs/                   # Data Transfer Objects
    │   └── UseCase/                # Casos de uso da aplicação
    │
    ├── Controllers/                # Controllers MVC
    │   ├── HomeController.cs
    │   ├── RegistroController.cs
    │   └── UsuarioController.cs
    │
    ├── Domain/                     # Camada de Domínio (compartilhada)
    ├── Infrastructure/             # Camada de Infraestrutura (compartilhada)
    ├── Models/                     # ViewModels
    ├── Views/                      # Views Razor
    │   ├── Home/
    │   ├── Registro/
    │   ├── Usuario/
    │   └── Shared/
    ├── wwwroot/                    # Arquivos estáticos (CSS, JS, etc.)
    ├── Program.cs                  # Ponto de entrada da aplicação web
    └── appsettings.json            # Configurações da aplicação web
```

---

## 📦 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Oracle Database](https://www.oracle.com/database/) (ou acesso a um servidor Oracle)
- Uma IDE de sua preferência (Visual Studio, Rider, VS Code)

---

## 🚀 Executando o Projeto

### 1. Clone o repositório

```bash
git clone <url-do-repositorio>
cd gs-ZenFlow
```

### 2. Configure a String de Conexão

Edite os arquivos `appsettings.json` de ambos os projetos e configure a string de conexão do Oracle:

**gs-ZenFlow/appsettings.json** (API):
```json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=seu_usuario;Password=sua_senha;Data Source=servidor:porta/servico;"
  }
}
```

**Web-gs-ZenFlow/appsettings.json** (Web MVC):
```json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=seu_usuario;Password=sua_senha;Data Source=servidor:porta/servico;"
  }
}
```

### 3. Execute a Aplicação

#### Executando a API (gs-ZenFlow)

```bash
dotnet run --project gs-ZenFlow/gs-ZenFlow.csproj
```

A API estará disponível em:
- **HTTP**: `http://localhost:5000` (ou porta configurada)
- **HTTPS**: `https://localhost:5001` (se configurado)
- **Swagger**: `http://localhost:5000` (em ambiente de desenvolvimento)

#### Executando a Aplicação Web (Web-gs-ZenFlow)

```bash
dotnet run --project Web-gs-ZenFlow/Web-gs-ZenFlow.csproj
```

A aplicação web estará disponível em:
- **HTTP**: `http://localhost:5000` (ou porta configurada)
- **HTTPS**: `https://localhost:5001` (se configurado)

---

## 🔄 Migrations

As migrations estão localizadas em `gs-ZenFlow/Infrastructure/Migrations/` e podem ser gerenciadas a partir de qualquer um dos projetos.

### Criar uma Nova Migration

```bash
# Usando o projeto API
dotnet ef migrations add NomeDaMigration --project gs-ZenFlow/gs-ZenFlow.csproj

# Ou usando o projeto Web
dotnet ef migrations add NomeDaMigration --project Web-gs-ZenFlow/Web-gs-ZenFlow.csproj
```

### Aplicar Migrations ao Banco de Dados

```bash
# Usando o projeto API
dotnet ef database update --project gs-ZenFlow/gs-ZenFlow.csproj

# Ou usando o projeto Web
dotnet ef database update --project Web-gs-ZenFlow/Web-gs-ZenFlow.csproj
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

## 🌐 Aplicação Web MVC

A aplicação web (`Web-gs-ZenFlow`) oferece uma interface gráfica para gerenciar usuários e registros através de páginas Razor.

### Rotas Disponíveis

#### Home
- `/` - Página inicial
- `/Home/Privacy` - Página de privacidade

#### Usuários
- `/Usuario` - Lista de usuários
- `/Usuario/Create` - Criar novo usuário
- `/Usuario/Details/{id}` - Detalhes do usuário
- `/Usuario/Delete/{id}` - Deletar usuário
- `/Usuario/Login` - Login de usuário
- `/Usuario/AlterarEmail/{id}` - Alterar email
- `/Usuario/AlterarSenha/{id}` - Alterar senha

#### Registros
- `/Registro` - Lista de registros
- `/Registro/Create` - Criar novo registro
- `/Registro/Details/{id}` - Detalhes do registro
- `/Registro/Delete/{id}` - Deletar registro
- `/Registro/ByUsuario/{usuarioId}` - Registros por usuário

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

A documentação interativa da API está disponível através do Swagger quando a aplicação **gs-ZenFlow** está rodando em ambiente de desenvolvimento.

Acesse: `http://localhost:5000` (ou a porta configurada no `launchSettings.json`)

O Swagger permite:
- Visualizar todos os endpoints disponíveis
- Testar as requisições diretamente no navegador
- Ver os modelos de dados (DTOs)
- Verificar os códigos de resposta esperados
- Testar autenticação e autorização (quando implementado)

**Recursos do Swagger:**
- Configuração personalizada em `appsettings.json`
- Suporte a múltiplos servidores (desenvolvimento e stage)
- Anotações habilitadas para melhor documentação
- Tratamento de erros com ProblemDetails (RFC 7807)

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Arquitetura em Camadas (Layered Architecture)** e **Clean Architecture**. Ambos os projetos (API e Web MVC) compartilham a mesma estrutura de camadas.

### Camadas

1. **Domain** - Entidades e interfaces de repositórios
   - Contém as regras de negócio puras
   - Não depende de outras camadas
   - Entidades: `Usuario`, `Registro`
   - Interfaces: `IUsuarioRepository`, `IRegistroRepository`

2. **Application** - Casos de uso e DTOs
   - Orquestra a lógica de negócio
   - Define contratos de entrada e saída
   - Use Cases: `UsuarioUseCase`, `RegistroUseCase`
   - DTOs para transferência de dados

3. **Infrastructure** - Implementações técnicas
   - Acesso a dados (Entity Framework Core)
   - Implementações de repositórios
   - Configurações de mapeamento (Fluent API)
   - Migrations do banco de dados

4. **Apresentação**
   - **API (gs-ZenFlow)**: Controllers REST com Swagger
   - **Web MVC (Web-gs-ZenFlow)**: Controllers MVC com Views Razor

### Padrões Utilizados

- **Repository Pattern** - Abstração do acesso a dados
- **Use Case Pattern** - Encapsulamento da lógica de negócio
- **DTO Pattern** - Transferência de dados entre camadas
- **Dependency Injection** - Inversão de controle
- **ProblemDetails (RFC 7807)** - Padronização de respostas de erro na API
- **CORS** - Configurado para permitir requisições cross-origin

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

Este projeto foi desenvolvido para a Global Solution da FIAP.
