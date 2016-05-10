Remove-Item -Force -Recurse -ErrorAction SilentlyContinue .\Build

New-Item -ItemType Directory Build

.\Nuget.exe pack -OutputDirectory .\Build .\src\Sleddog.Blink1\Sleddog.Blink1.csproj -Prop Configuration=Release

.\Nuget.exe push -Source https://www.nuget.org/api/v2/package .\Build\*.nupkg
