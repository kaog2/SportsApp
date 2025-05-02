#!/bin/bash
set -e

echo "Running migrations..."
dotnet ef database update --no-build --project ./SportsApp.API.csproj

echo "🚀 Starting API..."
exec dotnet SportsApp.API.dll