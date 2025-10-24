FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/BookCatalog/BookCatalog.API/BookCatalog.API.csproj", "src/services/BookCatalog/BookCatalog.API/"]
COPY ["src/services/BookCatalog/BookCatalog.Application/BookCatalog.Application.csproj", "src/services/BookCatalog/BookCatalog.Application/"]
COPY ["src/shared/Shared.CQRS/Shared.CQRS.csproj", "src/shared/Shared.CQRS/"]
COPY ["src/shared/Shared/Shared.csproj", "src/shared/Shared/"]
COPY ["src/services/BookCatalog/BookCatalog.Domain/BookCatalog.Domain.csproj", "src/services/BookCatalog/BookCatalog.Domain/"]
COPY ["src/services/BookCatalog/BookCatalog.Infrastructure/BookCatalog.Infrastructure.csproj", "src/services/BookCatalog/BookCatalog.Infrastructure/"]
RUN dotnet restore "src/services/BookCatalog/BookCatalog.API/BookCatalog.API.csproj"
COPY . .
WORKDIR "/src/src/services/BookCatalog/BookCatalog.API"
RUN dotnet build "./BookCatalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookCatalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookCatalog.API.dll"]
