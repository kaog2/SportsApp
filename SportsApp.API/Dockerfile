FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-stage
WORKDIR /src
COPY . .
RUN dotnet restore "SportsApp.API.csproj"
RUN dotnet build "SportsApp.API.csproj" -c Release -o /app/build

FROM build-stage AS publish-stage
RUN dotnet publish "SportsApp.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app

# Instalar herramientas EF Core
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Copiar binarios publicados
COPY --from=publish-stage /app/publish .

# Copiar el código fuente para EF CLI
COPY --from=build-stage /src .

# Entrypoint
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh \
    && sed -i 's/\r$//' /entrypoint.sh

ENTRYPOINT ["/entrypoint.sh"]
