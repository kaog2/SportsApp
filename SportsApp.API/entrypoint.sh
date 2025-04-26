#!/bin/bash
set -e

echo "📦 Ejecutando migraciones..."
dotnet ef database update --no-build --project ./SportsApp.API.csproj

echo "🚀 Iniciando la aplicación..."
exec dotnet SportsApp.API.dll
