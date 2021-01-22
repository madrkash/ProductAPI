FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app 
EXPOSE 5000
EXPOSE 80
 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
COPY . .
RUN dotnet restore "test/ProductStore.IntegrationTests/ProductStore.IntegrationTests.csproj"
ENTRYPOINT ["dotnet", "test", "test/ProductStore.IntegrationTests/ProductStore.IntegrationTests.csproj"]