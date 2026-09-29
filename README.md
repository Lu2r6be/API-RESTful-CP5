# Biblioteca API

API RESTful em ASP.NET Core (.NET 10) com Entity Framework Core para controlar o acervo de uma biblioteca comunitária. Projeto do CP5 de C# Software Development, FIAP.

## Integrantes

| Nome | RM | Instituição |
| ---- | -- | ----------- |
| Luiz Henrique Dos Reis Grabe | RM556001 | FIAP |

## Contexto do projeto

Biblioteca de bairro ou de escola quase sempre controla o acervo em caderno ou planilha. Aí fica difícil saber quais livros existem e se estão na estante ou emprestados.

A Biblioteca API guarda essas informações em um banco de dados e libera o acesso por HTTP. Um site ou um app (Web ou Mobile) pode usar a API para cadastrar, consultar, atualizar e remover livros, e marcar se cada um está disponível. Ela foi pensada para bibliotecários e voluntários que cuidam do acervo.

**Fluxo da aplicação:** Cliente (Web/Mobile) → ASP.NET Core Web API → Entity Framework Core → SQLite → Resposta HTTP

## Banco de dados

**SQLite**, acessado pelo Entity Framework Core. O arquivo `biblioteca.db` é criado sozinho na primeira execução, com a tabela `Livros`.

## Como rodar o projeto

Precisa do [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) instalado (confira com `dotnet --version`).

1. Abra um terminal na pasta `FIAP.Biblioteca.API` (a que tem o arquivo `.csproj`).
2. Rode o projeto:

```bash
dotnet run
```

3. Abra `http://localhost:5080/swagger` no navegador.

Na primeira vez, a Migration cria o `biblioteca.db` com a tabela e 3 livros de exemplo. Para começar do zero, pare a API, apague o `biblioteca.db` e rode de novo.

No Visual Studio 2026, abra o `FIAP.Biblioteca.API.slnx` e aperte **F5**.

## Migration (EF Core)

A Migration se chama `InitialCreate` e está na pasta `Migrations/`. Ela cria a tabela `Livros` e insere os 3 livros de exemplo.

```bash
# instalar a ferramenta (só uma vez)
dotnet tool install --global dotnet-ef

# comando usado para criar a Migration
dotnet ef migrations add InitialCreate

# aplicar no banco (a API já faz isso sozinha ao iniciar, com Database.Migrate())
dotnet ef database update
```

## Endpoints

A versão da API vai na rota (`/api/v1/...`).

| Método | Endpoint | Descrição | Status |
| ------ | -------- | --------- | ------ |
| `GET` | `/api/v1/livros` | Lista os livros | 200 |
| `GET` | `/api/v1/livros/{id}` | Busca um livro pelo Id | 200, 404 |
| `POST` | `/api/v1/livros` | Cadastra um livro com validação dos dados | 201, 400 |
| `PUT` | `/api/v1/livros/{id}` | Atualiza os dados de um livro | 204, 400, 404 |
| `DELETE` | `/api/v1/livros/{id}` | Remove um livro | 204, 404 |

Exemplo de corpo para `POST` e `PUT`:

```json
{
  "titulo": "O Hobbit",
  "autor": "J.R.R. Tolkien",
  "anoPublicacao": 1937,
  "disponivel": true
}
```

Título e autor são obrigatórios, e o ano precisa estar entre 1000 e o ano atual. Se o banco falhar ao salvar, a API responde `500`.

## Evidências de teste

Prints do Swagger com cada endpoint funcionando:

| # | Endpoint | Cenário | Status | Print |
|---|----------|---------|--------|-------|
| 01 | `GET /api/v1/livros` | Lista os livros | 200 | [01-listar-livros.png](docs/evidencias/01-listar-livros.png) |
| 02 | `GET /api/v1/livros/1` | Busca livro por Id | 200 | [02-buscar-livro-por-id.png](docs/evidencias/02-buscar-livro-por-id.png) |
| 03 | `GET /api/v1/livros/99` | Livro que não existe | 404 | [03-buscar-livro-inexistente.png](docs/evidencias/03-buscar-livro-inexistente.png) |
| 04 | `POST /api/v1/livros` | Cadastra um livro | 201 | [04-cadastrar-livro01.png](docs/evidencias/04-cadastrar-livro01.png), [04-cadastrar-livro02.png](docs/evidencias/04-cadastrar-livro02.png) |
| 05 | `POST /api/v1/livros` | Título e autor vazios | 400 | [05-cadastrar-livro-invalido.png](docs/evidencias/05-cadastrar-livro-invalido.png) |
| 06 | `PUT /api/v1/livros/4` | Atualiza o livro cadastrado | 204 | [06-atualizar-livro.png](docs/evidencias/06-atualizar-livro.png) |
| 07 | `DELETE /api/v1/livros/4` | Remove o livro | 204 | [07-excluir-livro.png](docs/evidencias/07-excluir-livro.png) |
