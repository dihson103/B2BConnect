#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Web-backend/src/WebApi/WebApi.csproj", "Web-backend/src/WebApi/"]
COPY ["Web-backend/src/Application/Application.csproj", "Web-backend/src/Application/"]
COPY ["Web-backend/src/Contract/Contract.csproj", "Web-backendsrc/Contract/"]
COPY ["Web-backend/src/Domain/Domain.csproj", "Web-backend/src/Domain/"]
COPY ["Web-backend/src/Infrastructure/Infrastructure.csproj", "Web-backend/src/Infrastructure/"]
COPY ["Web-backend/src/Persistence/Persistence.csproj", "Web-backend/src/Persistence/"]
RUN dotnet restore "./Web-backend/src/WebApi/WebApi.csproj"

COPY . .
WORKDIR "/src/Web-backend/src/WebApi"
RUN dotnet build "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebApi.dll"]

