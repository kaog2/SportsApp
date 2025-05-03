#!/bin/bash
set -e

echo "⏳ Waiting for database..."
until dotnet ef database update --project SportsApp.API.csproj; do
  echo "❗ Retry in 5s..."
  sleep 5
done

echo "🚀 Starting API..."
exec dotnet SportsApp.API.dll
