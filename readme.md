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

Essas melhorias ajudarão a tornar o projeto mais robusto, escalável e fácil de manter.