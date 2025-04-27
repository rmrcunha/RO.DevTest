# RO.DevTest

Teste técnico realizado com .NET 8, PostgreSQL, Entity Framework Core no padrão CQRS e Repository.

## Funcionalidades desenvolvidas
CRUD completo implementando o padrão CQRS

- Autenticação de usuários com JWT
- Produtos
- Vendas
- Usuários

## 🗂️ Estrutura de Projeto

| Camada                    | Responsabilidade |
|--------------------------|------------------|
| `Domain`                 | Entidades, Enums, Exceptions |
| `Application`            | CQRS (Commands, Queries, Handlers, Validators) |
| `Persistence`            | Contexto EF Core, Migrations, Repositórios |
| `Infrastructure`         | Serviços auxiliares, abstrações |
| `WebApi`                 | Controllers, Endpoints, Injeção de Dependência |
| `Tests`                  | Testes automatizados |

## Tecnologias utilizadas
- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- FluentValidation
- MediatR
- Docker
- Swagger
- JWT Bearer Authentication
- xUnit + Moq
- Swagger / OpenAPI

## Execução dos testes

	dotnet test

Testes cobrem a maioria das funcionalidades implementadas, incluindo:
- Validações de entrada
- Repositórios
- Handlers
- Exceções
- Autenticação

## Como executar o projeto

1. **Pré-requisitos**
	- Instalar o Docker
	- Instalar o .NET 8 SDK
	- Instalar o PostgreSQL

2. **Clonar o repositório**
	```bash
		git clone https://github.com/rmrcunha/RO.DevTest.git
	```

3. **Iniciar o Docker**
	- Criar o banco de dados PostgreSQL
	
	```
	docker-compose up -d
	 ```

4. **Aplicar migrations**

	``` 
	dotnet ef database update --project RO.DevTest.Persistence
	```

5. **Executar o projeto**
	``` 
	dotnet run --project RO.DevTest.WebApi
	```

6. **Acessar a API**
	- Acesse `http://localhost:7014/swagger` para visualizar a documentação da API gerada pelo Swagger.


### Autenticação e Autorização

- O projeto utiliza JWT Bearer Authentication para autenticação e autorização.
- Admin é iniciado automaticamente

```json
{
	"email":"admin@rota.com",
	"password": "Acesso123!"
}
```