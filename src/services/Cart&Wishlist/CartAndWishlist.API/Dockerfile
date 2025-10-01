FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/Cart&Wishlist/CartAndWishlist.API/CartAndWishlist.API.csproj", "src/services/Cart&Wishlist/CartAndWishlist.API/"]
COPY ["src/services/Cart&Wishlist/CartAndWishlist.BLL/CartAndWishlist.BLL.csproj", "src/services/Cart&Wishlist/CartAndWishlist.BLL/"]
COPY ["src/services/Cart&Wishlist/CartAndWishlist.DAL/CartAndWishlist.DAL.csproj", "src/services/Cart&Wishlist/CartAndWishlist.DAL/"]
COPY ["src/services/Cart&Wishlist/CartAndWishlist.Domain/CartAndWishlist.Domain.csproj", "src/services/Cart&Wishlist/CartAndWishlist.Domain/"]
COPY ["src/shared/Shared/Shared.csproj", "src/shared/Shared/"]
RUN dotnet restore "src/services/Cart&Wishlist/CartAndWishlist.API/CartAndWishlist.API.csproj"
COPY . .
WORKDIR "/src/src/services/Cart&Wishlist/CartAndWishlist.API"
RUN dotnet build "./CartAndWishlist.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CartAndWishlist.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CartAndWishlist.API.dll"]
