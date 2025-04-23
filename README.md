# RO.DevTest

Teste técnico realizado com .NET 8, PostgreSQL, Entity Framework Core no padrão CQRS e Repository.

## Funcionalidades desenvolvidas
CRUD completo implementando o padrão CQRS

Produtos

- CreateProductCommand
- GetProductByIdQuery
- GetAllProductsQuery
- UpdateProductCommand
- DeleteProductCommand

Vendas
- CreateSaleCommand
- GetSaleByIdQuery
- GetSalesByPeriodQuery
- UpdateSaleCommand
- DeleteSaleCommand

## 🗂️ Estrutura de Projeto

| Camada                    | Responsabilidade |
|--------------------------|------------------|
| `Domain`                 | Entidades, Enums, Exceptions |
| `Application`            | CQRS (Commands, Queries, Handlers, Validators) |
| `Persistence`            | Contexto EF Core, Migrations, Repositórios |
| `Infrastructure`         | Serviços auxiliares, abstrações |
| `WebApi`                 | Controllers, Endpoints, Injeção de Dependência |

## Tecnologias utilizadas
- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- FluentValidation
- MediatR
- Docker
- Swagger

## Como executar o projeto

1. **Iniciar o Docker**
	- Criar o banco de dados PostgreSQL
	- docker-compose up -d

2. **Aplicar migrations**
	- dotnet ef database update --project RO.DevTest.Persistence

3. **Executar o projeto**
	- dotnet run --project RO.DevTest.WebApi

4 **Acessar a API**
	- Acesse `http://localhost:7014/swagger` para visualizar a documentação da API gerada pelo Swagger.