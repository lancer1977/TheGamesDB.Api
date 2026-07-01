#!/usr/bin/env bash
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root"

dotnet restore GamesDB.RestAsync.Test/GamesDB.RestAsync.Test.csproj
dotnet build GamesDB.RestAsync.Test/GamesDB.RestAsync.Test.csproj --configuration Release --no-restore
dotnet test GamesDB.RestAsync.Test/GamesDB.RestAsync.Test.csproj --configuration Release --no-restore --no-build --verbosity normal
