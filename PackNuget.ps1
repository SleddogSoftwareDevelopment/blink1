rm -Force .\Build\*.nupkg

.\src\.nuget\Nuget.exe pack -OutputDirectory .\Build .\src\Sleddog.Blink1\Sleddog.Blink1.csproj

.\src\.nuget\Nuget.exe push .\Build\*.nupkg