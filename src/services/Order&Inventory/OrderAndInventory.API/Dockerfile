FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/Order&Inventory/OrderAndInventory.API/OrderAndInventory.API.csproj", "src/services/Order&Inventory/OrderAndInventory.API/"]
COPY ["src/services/Order&Inventory/OrderAndInventory.BLL/OrderAndInventory.BLL.csproj", "src/services/Order&Inventory/OrderAndInventory.BLL/"]
COPY ["src/services/Order&Inventory/OrderAndInventory.DAL/OrderAndInventory.DAL.csproj", "src/services/Order&Inventory/OrderAndInventory.DAL/"]
COPY ["src/shared/Shared/Shared.csproj", "src/shared/Shared/"]
RUN dotnet restore "src/services/Order&Inventory/OrderAndInventory.API/OrderAndInventory.API.csproj"
COPY . .
WORKDIR "/src/src/services/Order&Inventory/OrderAndInventory.API"
RUN dotnet build "./OrderAndInventory.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderAndInventory.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderAndInventory.API.dll"]
