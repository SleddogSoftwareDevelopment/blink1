Remove-Item -Force -ErrorAction SilentlyContinue .\Build\*.nupkg

New-Item -ItemType Directory Build

.\Nuget.exe pack -OutputDirectory .\Build .\src\Sleddog.Blink1\Sleddog.Blink1.csproj -Prop Configuration=Release

#.\src\.nuget\Nuget.exe push .\Build\*.nupkg
