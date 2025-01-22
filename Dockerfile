# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore as distinct layers
COPY ["Project.Manager.Api/Project.Manager.Api.csproj", "./"]
COPY ["Project.Manager.Application/Project.Manager.Application.csproj", "./"]
COPY ["Project.Manager.Domain/Project.Manager.Domain.csproj", "./"]
COPY ["Project.Manager.Infra.Data/Project.Manager.Infra.Data.csproj", "./"]
COPY ["Project.Manager.Test/Project.Manager.Test.csproj", "./"]
RUN dotnet restore

# Copy everything else and build the app
COPY . ./
RUN dotnet publish Project.Manager.Api.csproj -c Release -o out

# Use the official ASP.NET Core runtime image for the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port 80
EXPOSE 80

# Run the app
ENTRYPOINT ["dotnet", "Project.Manager.Api.dll"]