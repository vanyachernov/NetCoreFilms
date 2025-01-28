FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Films.API/Films.API.csproj", "src/Films.API/"]
COPY ["src/Films.Application/Films.Application.csproj", "src/Films.Application/"]
COPY ["src/Films.Core/Films.Core.csproj", "src/Films.Core/"]
COPY ["src/Films.Infrastructure/Films.Infrastructure.csproj", "src/Films.Infrastructure/"]
RUN dotnet restore "src/Films.API/Films.API.csproj"
COPY . .
WORKDIR "/src/src/Films.API"
RUN dotnet build "Films.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Films.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Films.API.dll"]
