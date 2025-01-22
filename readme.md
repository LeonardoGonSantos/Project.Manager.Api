# Como Rodar

## Executando com Docker Compose

Para rodar a aplicação utilizando Docker Compose, siga os passos abaixo:

1. Certifique-se de ter o Docker e o Docker Compose instalados na sua máquina.
2. Navegue até o diretório onde o arquivo `docker-compose.yml` está localizado.
3. Execute o comando abaixo para iniciar os serviços definidos no `docker-compose.yml`:

```sh
docker-compose up
```

4. Para rodar os serviços em segundo plano (modo detached), utilize o comando:

```sh
docker-compose up -d
```

5. Para parar os serviços, utilize o comando:

```sh
docker-compose down
```

Esses comandos irão facilitar a execução e gerenciamento dos serviços da aplicação em contêineres Docker.

## Testando a API com Swagger

Após iniciar os serviços com Docker Compose, abra o navegador e acesse a página [http://localhost:5741/swagger](http://localhost:5741/swagger). A documentação do Swagger será exibida, permitindo que você teste os endpoints da API de forma interativa.

# Melhorias no Projeto

## Pontos de Melhoria

1. **Código Limpo e Manutenível**:
  - Refatorar o código para seguir os princípios do SOLID.
  - Implementar padrões de design como Factory, Singleton, e Strategy onde aplicável.

3. **Documentação**:
  - Melhorar a documentação do código e criar um guia de contribuição.
  - Adicionar comentários explicativos em partes complexas do código.

4. **Desempenho**:
  - Otimizar consultas ao banco de dados e melhorar a eficiência dos algoritmos.
  - Implementar caching para reduzir a carga no servidor.

5. **Segurança**:
  - Revisar e melhorar a segurança da aplicação, incluindo a validação de entradas e proteção contra ataques comuns como SQL Injection e XSS.

6. **Dívida Técnica**:
  - Separar a camada de aplicação da camada de repositório quando o projeto crescer.
  - Utilizar Dapper para acesso ao banco de dados caso o projeto seja muito requisitado.
  - Não utilizei SQL Server por fins de simplicidade.

## Visão do Projeto sobre Arquitetura/Cloud

- **Arquitetura de Microserviços**: ?

- **Cloud**: Migrar a aplicação para uma plataforma de nuvem como Azure ou AWS para aproveitar a escalabilidade, disponibilidade e outros serviços oferecidos pela nuvem.

- **CI/CD**: Implementar pipelines de integração contínua e entrega contínua para automatizar o processo de build, testes e deploy.

## Pipeline no GitHub

Foi criada uma pipeline no GitHub para buildar e testar a aplicação:

```yaml
name: Build and Test .NET 8 Project

on:
  pull_request:
  branches:
    - main

jobs:
  # Job 1: Build
  build:
  runs-on: ubuntu-latest
  steps:
    # Checkout the repository
    - name: Checkout code
    uses: actions/checkout@v3

    # Setup .NET 8 SDK
    - name: Setup .NET 8
    uses: actions/setup-dotnet@v3
    with:
      dotnet-version: 8.0.x

    # Restore dependencies
    - name: Restore dependencies
    run: dotnet restore ./Project.Manager.Api/Project.Manager.Api.csproj

    # Build the project
    - name: Build the project
    run: dotnet build ./Project.Manager.Api/Project.Manager.Api.csproj --configuration Release --no-restore

  # Job 2: Test
  test:
  runs-on: ubuntu-latest
  needs: build # Dependência do Job de Build
  steps:
    # Checkout the repository
    - name: Checkout code
    uses: actions/checkout@v3

    # Setup .NET 8 SDK
    - name: Setup .NET 8
    uses: actions/setup-dotnet@v3
    with:
      dotnet-version: 8.0.x

    # Run tests
    - name: Run tests
    run: dotnet test ./Project.Manager.Api/Project.Manager.Api.csproj --configuration Release --no-build --verbosity normal
```

## Criando Migrations

Para criar novas migrations, utilize o comando abaixo:

```sh
dotnet ef migrations add InitialCreateDb --project Project.Manager.Infra.Data --startup-project Project.Manager.Api
```

Essas melhorias ajudarão a tornar o projeto mais robusto, escalável e fácil de manter.

