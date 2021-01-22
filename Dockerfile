FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app 
EXPOSE 5000
EXPOSE 80
 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["src/ProductStore.Core/ProductStore.Core.csproj", "ProductStore.Core/"]
COPY ["src/ProductStore.Core/", "ProductStore.Core/"]
COPY ["src/ProductStore.Infrastructure/ProductStore.Infrastructure.csproj", "ProductStore.Infrastructure/"]
COPY ["src/ProductStore.Infrastructure/", "ProductStore.Infrastructure/"]
COPY ["src/ProductStore.API/ProductStore.API.csproj", "ProductStore.API/"]
COPY ["src/ProductStore.API/", "ProductStore.API/"]

RUN dotnet restore "ProductStore.API/ProductStore.API.csproj"

WORKDIR "/src/ProductStore.API"
RUN dotnet build "ProductStore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductStore.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductStore.API.dll"]