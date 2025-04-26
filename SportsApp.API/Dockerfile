# Etapa base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa para construir la app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SportsApp.API/SportsApp.API.csproj", "SportsApp.API/"]
RUN dotnet restore "SportsApp.API/SportsApp.API.csproj"
COPY . .
WORKDIR "/src/SportsApp"
RUN dotnet build "SportsApp.API.csproj" -c Release -o /app/build

# Publicar
FROM build AS publish
RUN dotnet publish "SportsApp.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final: runtime + migraciones
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app

# Instala herramientas para EF Core
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Copiar publicación y entrada
COPY --from=publish /app/publish .

# Copiar entrypoint para ejecutar migraciones
COPY SportsApp.API/entrypoint.sh ./entrypoint.sh
RUN chmod +x ./entrypoint.sh

# Ejecutar EF y app
ENTRYPOINT ["./entrypoint.sh"]
