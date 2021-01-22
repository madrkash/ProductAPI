FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
COPY . . 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

RUN dotnet restore
ENTRYPOINT ["dotnet", "test"]
